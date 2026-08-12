using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AutoCheckIn
{
    /// <summary>
    /// Loads one calendar year's Taiwan government holiday data from the official
    /// data.gov.tw dataset (id 14718), caching the validated CSV beside the
    /// executable so a given year is only ever downloaded once. Any network,
    /// metadata, CSV, or filesystem failure degrades to
    /// <see cref="HolidayCalendarResult.Unavailable"/> rather than throwing --
    /// automatic scheduling must never stop because holiday data is unreachable.
    /// </summary>
    internal sealed class OfficialTaiwanHolidayProvider : ITaiwanHolidayProvider
    {
        private static readonly Uri MetadataUri = new Uri("https://data.gov.tw/api/v2/rest/dataset/14718");
        private const string UnavailableMessage = "Holiday data unavailable; using weekday schedule only.";

        private readonly string m_cacheDirectory;
        private readonly Func<Uri, CancellationToken, Task<string>> m_downloadTextAsync;
        private readonly HttpClient m_ownedClient;

        /// <summary>
        /// Deterministic constructor for tests: the caller supplies exactly how a
        /// URI is turned into text, with no real HTTP client involved.
        /// </summary>
        public OfficialTaiwanHolidayProvider(
            string cacheDirectory, Func<Uri, CancellationToken, Task<string>> downloadTextAsync)
        {
            m_cacheDirectory = cacheDirectory ?? throw new ArgumentNullException(nameof(cacheDirectory));
            m_downloadTextAsync = downloadTextAsync ?? throw new ArgumentNullException(nameof(downloadTextAsync));
        }

        private OfficialTaiwanHolidayProvider(string cacheDirectory, HttpClient ownedClient)
            : this(cacheDirectory, (uri, token) => DownloadWithClientAsync(ownedClient, uri, token))
        {
            m_ownedClient = ownedClient;
        }

        /// <summary>Runtime factory: owns one HttpClient for the lifetime of the provider.</summary>
        public static OfficialTaiwanHolidayProvider CreateDefault(string cacheDirectory)
        {
            return new OfficialTaiwanHolidayProvider(cacheDirectory, new HttpClient { Timeout = TimeSpan.FromSeconds(30) });
        }

        private static async Task<string> DownloadWithClientAsync(HttpClient client, Uri uri, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            using (var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async Task<HolidayCalendarResult> GetCalendarAsync(
            int year, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var cachePath = GetCachePath(year);
                if (File.Exists(cachePath))
                {
                    try
                    {
                        var cachedCsv = File.ReadAllText(cachePath, Encoding.UTF8);
                        return TaiwanHolidayCsvParser.Parse(cachedCsv, year);
                    }
                    catch (Exception ex)
                    {
                        // A corrupt/stale cache file must not block a fresh download.
                        ErrorLogger.Log("Taiwan holiday calendar cache read", ex);
                    }
                }

                var metadataJson = await m_downloadTextAsync(MetadataUri, cancellationToken).ConfigureAwait(false);
                var csvUri = ResolveCsvUri(metadataJson, year);
                if (csvUri == null)
                {
                    return HolidayCalendarResult.Unavailable(UnavailableMessage);
                }

                var csv = await m_downloadTextAsync(csvUri, cancellationToken).ConfigureAwait(false);
                var result = TaiwanHolidayCsvParser.Parse(csv, year);

                Directory.CreateDirectory(m_cacheDirectory);
                File.WriteAllText(cachePath, csv, Encoding.UTF8);

                return result;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Taiwan holiday calendar", ex);
                return HolidayCalendarResult.Unavailable(UnavailableMessage);
            }
        }

        private string GetCachePath(int year)
        {
            return Path.Combine(m_cacheDirectory, "holidays-" + year.ToString(CultureInfo.InvariantCulture) + ".csv");
        }

        /// <summary>
        /// Walks the data.gov.tw v2 dataset metadata (result.distribution[]) for
        /// the CSV resource matching this ROC year, preferring an exact
        /// description match, then falling back to a prefix match that excludes
        /// the Google-calendar (.ics) distractor entry, preferring UTF-8 encoding
        /// when more than one candidate remains.
        /// </summary>
        private static Uri ResolveCsvUri(string metadataJson, int year)
        {
            var serializer = new JavaScriptSerializer();
            var root = serializer.DeserializeObject(metadataJson) as Dictionary<string, object>;
            var result = GetDictionary(root, "result");
            var distribution = result != null ? GetList(result, "distribution") : null;
            if (distribution == null)
            {
                return null;
            }

            var expectedDescription = ExpectedDescription(year);
            var rocYearText = (year - 1911).ToString(CultureInfo.InvariantCulture);

            Dictionary<string, object> exactMatch = null;
            Dictionary<string, object> bestPrefixMatch = null;

            foreach (var entry in distribution)
            {
                var candidate = entry as Dictionary<string, object>;
                if (candidate == null)
                {
                    continue;
                }

                var description = GetString(candidate, "resourceDescription");
                var format = GetString(candidate, "resourceFormat");
                if (string.IsNullOrEmpty(description) ||
                    !string.Equals(format, "CSV", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (description.IndexOf("Google", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    continue;
                }

                if (string.Equals(description, expectedDescription, StringComparison.Ordinal))
                {
                    exactMatch = candidate;
                    break;
                }

                if (description.StartsWith(rocYearText, StringComparison.Ordinal))
                {
                    var encoding = GetString(candidate, "resourceCharacterEncoding");
                    var isUtf8 = string.Equals(encoding, "UTF-8", StringComparison.OrdinalIgnoreCase);
                    if (bestPrefixMatch == null || isUtf8)
                    {
                        bestPrefixMatch = candidate;
                    }
                }
            }

            var chosen = exactMatch ?? bestPrefixMatch;
            if (chosen == null)
            {
                return null;
            }

            var downloadUrl = GetString(chosen, "resourceDownloadUrl");
            Uri uri;
            if (string.IsNullOrEmpty(downloadUrl) ||
                !Uri.TryCreate(downloadUrl, UriKind.Absolute, out uri) ||
                uri.Scheme != Uri.UriSchemeHttps)
            {
                return null;
            }

            return uri;
        }

        private static string ExpectedDescription(int year)
        {
            return (year - 1911).ToString(CultureInfo.InvariantCulture) + "年中華民國政府行政機關辦公日曆表";
        }

        private static Dictionary<string, object> GetDictionary(Dictionary<string, object> source, string key)
        {
            object value;
            return source != null && source.TryGetValue(key, out value) ? value as Dictionary<string, object> : null;
        }

        private static IEnumerable GetList(Dictionary<string, object> source, string key)
        {
            // JavaScriptSerializer represents a JSON array as ArrayList or object[]
            // depending on the exact deserialization path -- accept either.
            object value;
            return source != null && source.TryGetValue(key, out value) ? value as IEnumerable : null;
        }

        private static string GetString(Dictionary<string, object> source, string key)
        {
            object value;
            return source != null && source.TryGetValue(key, out value) ? value as string : null;
        }

        public void Dispose()
        {
            m_ownedClient?.Dispose();
        }
    }
}
