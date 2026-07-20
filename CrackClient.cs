using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace AutoCheckIn
{
    internal class CrackClient
    {
        public CrackClient() { }

        public enum CheckAction
        {
            CheckIn = 2,
            CheckOut = 4
        }

        public string StatusMessage { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }

        const string baseUrl = "http://192.168.13.212";
        const string loginUrl = "http://192.168.13.212/UOF/Login.aspx";
        const string authenticationUrl = "http://192.168.13.212/UOF/Login/Authentication.aspx";
        const string homePageUrl = "http://192.168.13.212/UOF/Homepage.aspx";

        public void checkInOut(CheckAction checkAction)
        {
            var handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.Deflate,
                AllowAutoRedirect = true
            };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0");

            var response = client.GetAsync(loginUrl).Result;
            var html = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("===========Get Login.aspx====================================");
            Console.WriteLine($"HTTP status: {response.StatusCode}");
            Console.WriteLine($"HTTP length: {html.Length}");
            //Console.WriteLine(html);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml( html );

            string viewState = doc.DocumentNode.SelectSingleNode("//input[@name='__VIEWSTATE']")?.GetAttributeValue("value", "") ?? "";
            string viewStateGen = doc.DocumentNode.SelectSingleNode("//input[@name='__VIEWSTATEGENERATOR']")?.GetAttributeValue("value", "") ?? "";

            var loginForm = new Dictionary<string, string>
            {
                ["ScriptManager1"] = "UpdatePanel1|btnSubmit",
                ["__VIEWSTATE"] = viewState,
                ["__VIEWSTATEGENERATOR"] = viewStateGen,
                ["__EVENTTARGET"] = "",
                ["__EVENTARGUMENT"] = "",
                ["__LASTFOCUS"] = "",
                ["ddlCulture"] = "zh-TW",

                ["txtAccount"] =LoginID,
                ["txtPwd"] = Password,
                ["cboKeepaccount"] = "on",

                ["txtAccount2"] = "",
                ["hdflag"] = "false",
                ["hfIsAdAuth"] = "false",
                ["hfUserGuid"] = "",
                ["hfIsDownloadComplete"] = "0",

                ["__ASYNCPOST"] = "true",
                ["btnSubmit"] = "登入"
            };

            var reqLogin = new HttpRequestMessage(HttpMethod.Post, loginUrl)
            {
                Content = new FormUrlEncodedContent(loginForm)
            };
            reqLogin.Headers.Referrer = new Uri(loginUrl);
            reqLogin.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
            reqLogin.Headers.TryAddWithoutValidation("X-MicrosoftAjax", "Delta=true");
            reqLogin.Headers.TryAddWithoutValidation("Origin", baseUrl);
            reqLogin.Headers.TryAddWithoutValidation("Accept-Language", "zh-TW,zh;q=0.9,en;q=0.8");

            var respLogin = client.SendAsync(reqLogin).GetAwaiter().GetResult();
            var bodyLogin = respLogin.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Console.WriteLine("===========Post Login.aspx====================================");
            Console.WriteLine($"Login status: {(int)respLogin.StatusCode} {respLogin.StatusCode}");
            //Console.WriteLine(bodyLogin.Length > 1000 ? bodyLogin.Substring(0, 1000) : bodyLogin);

            if (bodyLogin.IndexOf("Authentication.aspx") == -1)
            {
                Console.WriteLine("Login fail.");
                StatusMessage = "Login fail, please check ID/Password.";
                return;
            }

            var authenticationResponse = client.GetAsync(authenticationUrl).Result;
            var authenticationHtml = authenticationResponse.Content.ReadAsStringAsync().Result;

            Console.WriteLine("===========Get authentication.aspx============================");
            Console.WriteLine($"Authentication HTTP status: {authenticationResponse.StatusCode}");
            Console.WriteLine($"Authentication HTTP length: {authenticationHtml.Length}");
            //Console.WriteLine(authenticationHtml);

            var authenticationDoc = new HtmlAgilityPack.HtmlDocument();
            authenticationDoc.LoadHtml(authenticationHtml);
            
            var homePageResponse = client.GetAsync(homePageUrl).Result;
            var homePageHtml = homePageResponse.Content.ReadAsStringAsync().Result;

            Console.WriteLine("===========Get HomePage.aspx====================================");
            Console.WriteLine($"HomePage HTTP status: {homePageResponse.StatusCode}");
            Console.WriteLine($"HomePage HTTP length: {homePageHtml.Length}");
            //Console.WriteLine(homePageHtml);

            var checkForm = new Dictionary<string, string>();

            var homePageDoc = new HtmlAgilityPack.HtmlDocument();
            homePageDoc.LoadHtml(homePageHtml);
            var hiddenInputs = homePageDoc.DocumentNode.SelectNodes("//input[@type='hidden']");

            if (hiddenInputs != null)
            {
                foreach (var hiddenInput in hiddenInputs)
                {
                    var name = hiddenInput.GetAttributeValue("name", "");
                    var value = hiddenInput.GetAttributeValue("value", "");
                    //Console.WriteLine($"{name} = {value}");
                    if (!string.IsNullOrEmpty(name))
                    {
                        checkForm.Add(name, value);
                    }
                }
            }

            int actionValue = (int)checkAction;

            checkForm["__EVENTTARGET"] = "ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$RadToolBar1";
            checkForm["__EVENTARGUMENT"] = actionValue.ToString();
            checkForm["__ASYNCPOST"] = "true";

            checkForm["ctl00$ScriptManager1"] =
              "ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$UpdatePanel1|ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$RadToolBar1";

            checkForm["ctl00$ContentPlaceHolder1$RadDock1a3205a5960143ae8c7577df63d948c8$C$widget$RadToolBar1$i0$rdDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            checkForm["ctl00$ContentPlaceHolder1$RadDock1a3205a5960143ae8c7577df63d948c8$C$widget$RadToolBar1$i0$rdDate$dateInput"] = DateTime.Now.ToString("yyyy/MM/dd");
            checkForm["ctl00$ContentPlaceHolder1$RadDockd3e9d57ac1744d8ba17c7c2a3354e5de$C$widget$RadToolBar1$i0$ddlDate"] = DateTime.Now.ToString("yyyy/MM/dd");
            checkForm["ctl00$ContentPlaceHolder1$RadDock1a3205a5960143ae8c7577df63d948c8$C$widget$RadToolBar1$i2$ddlLocation"] = "ALL";
            
            var reqCheck = new HttpRequestMessage(HttpMethod.Post, homePageUrl)
            {
                Content = new FormUrlEncodedContent(checkForm)
            };
            reqCheck.Headers.Referrer = new Uri(homePageUrl);
            reqCheck.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
            reqCheck.Headers.TryAddWithoutValidation("X-MicrosoftAjax", "Delta=true");
            //reqCheck.Headers.TryAddWithoutValidation("Origin", baseUrl);
            //reqCheck.Headers.TryAddWithoutValidation("Accept-Language", "zh-TW,zh;q=0.9,en;q=0.8");

            var respCheck = client.SendAsync(reqCheck).GetAwaiter().GetResult();
            var bodyCheck = respCheck.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Console.WriteLine("===========Check In/Out====================================");
            Console.WriteLine("Check Action: " + checkAction.ToString());
            Console.WriteLine($"Check In/Out status: {(int)respCheck.StatusCode} {respCheck.StatusCode}");
            //Console.WriteLine(bodyCheck.Length > 1000 ? bodyCheck.Substring(0, 1000) : bodyCheck);

            StatusMessage = $"{checkAction} status: {respCheck.StatusCode} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
