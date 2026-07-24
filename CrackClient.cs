using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCheckIn
{
    /// <summary>
    /// Talks to the UOF Web Forms site. All site and transport failures are
    /// converted to CheckResult so callers never need to handle HTTP details.
    /// </summary>
    internal sealed class CrackClient : IUofClient
    {
        private const string BaseUrl = "http://192.168.13.212";
        private const string LoginUrl = BaseUrl + "/UOF/Login.aspx";
        private const string AuthenticationUrl = BaseUrl + "/UOF/Login/Authentication.aspx";
        private const string HomePageUrl = BaseUrl + "/UOF/Homepage.aspx";

        public async Task<CheckResult> CheckAsync(
            string loginId,
            string password,
            CheckAction action,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(loginId) || string.IsNullOrEmpty(password))
            {
                return CheckResult.Failure("ID and password are required.");
            }

            try
            {
                using (var handler = CreateHandler())
                using (var client = CreateClient(handler))
                {
                    var loginPage = await GetTextAsync(client, LoginUrl, cancellationToken).ConfigureAwait(false);
                    if (!loginPage.Succeeded)
                    {
                        return loginPage.Result;
                    }

                    var tokens = ReadLoginTokens(loginPage.Content);
                    if (tokens == null)
                    {
                        return CheckResult.Failure("UOF login page format is invalid or has changed.");
                    }

                    var loginResult = await LoginAsync(
                        client, loginId, password, tokens, cancellationToken).ConfigureAwait(false);
                    if (!loginResult.Succeeded)
                    {
                        return loginResult.Result;
                    }

                    if (loginResult.Content.IndexOf("Authentication.aspx", StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        return CheckResult.Failure("Login failed. Please check ID/password or UOF status.");
                    }

                    var authentication = await GetTextAsync(
                        client, AuthenticationUrl, cancellationToken).ConfigureAwait(false);
                    if (!authentication.Succeeded)
                    {
                        return authentication.Result;
                    }

                    var homePage = await GetTextAsync(client, HomePageUrl, cancellationToken).ConfigureAwait(false);
                    if (!homePage.Succeeded)
                    {
                        return homePage.Result;
                    }

                    var form = ReadHiddenFields(homePage.Content);
                    if (form == null)
                    {
                        return CheckResult.Failure("UOF home page format is invalid or has changed.");
                    }

                    PopulateCheckForm(form, action);
                    return await SubmitCheckAsync(client, form, action, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                return CheckResult.Failure("UOF operation was cancelled.");
            }
            catch (HttpRequestException ex)
            {
                return CheckResult.Failure("Cannot connect to UOF: " + ex.Message);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("UOF client", ex);
                return CheckResult.Failure("UOF operation failed: " + ex.Message);
            }
        }

        private static HttpClientHandler CreateHandler()
        {
            return new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                AllowAutoRedirect = true
            };
        }

        private static HttpClient CreateClient(HttpMessageHandler handler)
        {
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AutoCheckIn/1.0");
            return client;
        }

        private static async Task<HttpTextResult> GetTextAsync(
            HttpClient client, string url, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return HttpTextResult.Fail(HttpFailure(url, response.StatusCode));
                }

                return HttpTextResult.Ok(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        private static LoginTokens ReadLoginTokens(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var viewState = document.DocumentNode.SelectSingleNode("//input[@name='__VIEWSTATE']")
                ?.GetAttributeValue("value", null);
            var generator = document.DocumentNode.SelectSingleNode("//input[@name='__VIEWSTATEGENERATOR']")
                ?.GetAttributeValue("value", null);

            return string.IsNullOrEmpty(viewState) || string.IsNullOrEmpty(generator)
                ? null
                : new LoginTokens(viewState, generator);
        }

        private static async Task<HttpTextResult> LoginAsync(
            HttpClient client,
            string loginId,
            string password,
            LoginTokens tokens,
            CancellationToken cancellationToken)
        {
            var form = new Dictionary<string, string>
            {
                ["ScriptManager1"] = "UpdatePanel1|btnSubmit",
                ["__VIEWSTATE"] = tokens.ViewState,
                ["__VIEWSTATEGENERATOR"] = tokens.Generator,
                ["__EVENTTARGET"] = "",
                ["__EVENTARGUMENT"] = "",
                ["__LASTFOCUS"] = "",
                ["ddlCulture"] = "zh-TW",
                ["txtAccount"] = loginId,
                ["txtPwd"] = password,
                ["cboKeepaccount"] = "on",
                ["txtAccount2"] = "",
                ["hdflag"] = "false",
                ["hfIsAdAuth"] = "false",
                ["hfUserGuid"] = "",
                ["hfIsDownloadComplete"] = "0",
                ["__ASYNCPOST"] = "true",
                ["btnSubmit"] = "登入"
            };

            using (var request = new HttpRequestMessage(HttpMethod.Post, LoginUrl))
            {
                request.Content = new FormUrlEncodedContent(form);
                request.Headers.Referrer = new Uri(LoginUrl);
                request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                request.Headers.TryAddWithoutValidation("X-MicrosoftAjax", "Delta=true");
                request.Headers.TryAddWithoutValidation("Origin", BaseUrl);
                request.Headers.TryAddWithoutValidation("Accept-Language", "zh-TW,zh;q=0.9,en;q=0.8");

                using (var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return HttpTextResult.Fail(HttpFailure(LoginUrl, response.StatusCode));
                    }

                    return HttpTextResult.Ok(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                }
            }
        }

        private static Dictionary<string, string> ReadHiddenFields(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var inputs = document.DocumentNode.SelectNodes("//input[@type='hidden']");
            if (inputs == null)
            {
                return null;
            }

            var form = new Dictionary<string, string>();
            foreach (var input in inputs)
            {
                var name = input.GetAttributeValue("name", string.Empty);
                if (!string.IsNullOrEmpty(name))
                {
                    // Some UOF pages contain duplicate hidden inputs; the last value wins.
                    form[name] = input.GetAttributeValue("value", string.Empty);
                }
            }

            return form.Count == 0 ? null : form;
        }

        private static void PopulateCheckForm(Dictionary<string, string> form, CheckAction action)
        {
            form["__EVENTTARGET"] = "ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$RadToolBar1";
            form["__EVENTARGUMENT"] = ((int)action).ToString();
            form["__ASYNCPOST"] = "true";
            form["ctl00$ScriptManager1"] = "ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$UpdatePanel1|ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$RadToolBar1";
            form["ctl00$ContentPlaceHolder1$RadDock1a3205a5960143ae8c7577df63d948c8$C$widget$RadToolBar1$i0$rdDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            form["ctl00$ContentPlaceHolder1$RadDock1a3205a5960143ae8c7577df63d948c8$C$widget$RadToolBar1$i0$rdDate$dateInput"] = DateTime.Now.ToString("yyyy/MM/dd");
            form["ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$RadToolBar1$i0$ddlDate"] = DateTime.Now.ToString("yyyy/MM/dd");
            form["ctl00$ContentPlaceHolder1$RadDock1a3205a5960143ae8c7577df63d948c8$C$widget$RadToolBar1$i2$ddlLocation"] = "ALL";
        }

        private static async Task<CheckResult> SubmitCheckAsync(
            HttpClient client,
            Dictionary<string, string> form,
            CheckAction action,
            CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, HomePageUrl))
            {
                request.Content = new FormUrlEncodedContent(form);
                request.Headers.Referrer = new Uri(HomePageUrl);
                request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                request.Headers.TryAddWithoutValidation("X-MicrosoftAjax", "Delta=true");

                using (var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return CheckResult.Failure(HttpFailure(HomePageUrl, response.StatusCode));
                    }

                    return CheckResult.Success(
                        $"{action} status: {response.StatusCode} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
            }
        }

        private static string HttpFailure(string url, HttpStatusCode statusCode)
        {
            return $"UOF returned HTTP {(int)statusCode} ({statusCode}) for {new Uri(url).AbsolutePath}.";
        }

        private sealed class LoginTokens
        {
            public LoginTokens(string viewState, string generator)
            {
                ViewState = viewState;
                Generator = generator;
            }

            public string ViewState { get; }
            public string Generator { get; }
        }

        private sealed class HttpTextResult
        {
            private HttpTextResult(bool succeeded, string content, CheckResult result)
            {
                Succeeded = succeeded;
                Content = content;
                Result = result;
            }

            public bool Succeeded { get; }
            public string Content { get; }
            public CheckResult Result { get; }

            public static HttpTextResult Ok(string content) => new HttpTextResult(true, content, null);
            public static HttpTextResult Fail(string message) =>
                new HttpTextResult(false, null, CheckResult.Failure(message));
        }
    }
}
