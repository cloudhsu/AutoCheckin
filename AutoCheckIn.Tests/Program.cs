using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCheckIn.Tests
{
    /// <summary>
    /// Dependency-free regression test runner for AutoCheckIn. Each test is a
    /// named case (sync or async); Main runs them all, prints PASS/FAIL per
    /// case, and returns a non-zero exit code if any failed.
    /// </summary>
    internal static class Program
    {
        internal static readonly string Valid2026Csv = BuildCalendarCsv(2026);

        private static readonly List<Tuple<string, Func<Task>>> Tests = new List<Tuple<string, Func<Task>>>
        {
            AsSyncTest(nameof(ParserReadsOnlyFlagTwoDates), ParserReadsOnlyFlagTwoDates),
            AsSyncTest(nameof(ParserRejectsWrongYearAndMalformedFiles), ParserRejectsWrongYearAndMalformedFiles),
            Tuple.Create<string, Func<Task>>(nameof(ProviderUsesValidCacheWithoutNetworkAsync), ProviderUsesValidCacheWithoutNetworkAsync),
            Tuple.Create<string, Func<Task>>(nameof(ProviderDiscoversDownloadsAndCachesAsync), ProviderDiscoversDownloadsAndCachesAsync),
            Tuple.Create<string, Func<Task>>(nameof(ProviderFallsBackWhenUnavailableAsync), ProviderFallsBackWhenUnavailableAsync),
        };

        private static Tuple<string, Func<Task>> AsSyncTest(string name, Action action)
        {
            return Tuple.Create<string, Func<Task>>(name, () =>
            {
                action();
                return Task.CompletedTask;
            });
        }

        [STAThread]
        static int Main()
        {
            var failures = 0;
            foreach (var test in Tests)
            {
                try
                {
                    test.Item2().GetAwaiter().GetResult();
                    Console.WriteLine("PASS " + test.Item1);
                }
                catch (Exception ex)
                {
                    failures++;
                    Console.WriteLine("FAIL " + test.Item1);
                    Console.WriteLine(ex);
                }
            }

            Console.WriteLine(failures == 0
                ? "All tests passed."
                : failures + " test(s) failed.");
            return failures == 0 ? 0 : 1;
        }

        private static void ParserReadsOnlyFlagTwoDates()
        {
            string csv = BuildCalendarCsv(2026, new DateTime(2026, 1, 1));
            var result = TaiwanHolidayCsvParser.Parse(csv, 2026);
            AssertTrue(result.Available, "calendar should be available");
            AssertTrue(result.Holidays.Contains(new DateTime(2026, 1, 1)), "holiday missing");
            AssertFalse(result.Holidays.Contains(new DateTime(2026, 1, 2)), "workday included");
        }

        private static void ParserRejectsWrongYearAndMalformedFiles()
        {
            AssertThrows<FormatException>(() =>
                TaiwanHolidayCsvParser.Parse(BuildCalendarCsv(2025), 2026));
            AssertThrows<FormatException>(() =>
                TaiwanHolidayCsvParser.Parse("wrong,headers\r\nvalue,value\r\n", 2026));
        }

        private static async Task ProviderUsesValidCacheWithoutNetworkAsync()
        {
            var testDirectory = CreateTempDirectory();
            try
            {
                File.WriteAllText(Path.Combine(testDirectory, "holidays-2026.csv"), Valid2026Csv, Encoding.UTF8);
                var calls = 0;
                var provider = new OfficialTaiwanHolidayProvider(testDirectory, (uri, token) =>
                {
                    calls++;
                    throw new InvalidOperationException("network should not be called");
                });
                var result = await provider.GetCalendarAsync(2026, CancellationToken.None);
                AssertTrue(result.Available, "cache should load");
                AssertEqual(0, calls, "network call count");
            }
            finally
            {
                Directory.Delete(testDirectory, true);
            }
        }

        private static async Task ProviderDiscoversDownloadsAndCachesAsync()
        {
            var testDirectory = CreateTempDirectory();
            try
            {
                const string csvUrl = "https://data.gov.tw/fake/holidays-115.csv";
                var metadataJson = BuildMetadataJson(2026, csvUrl, "UTF-8");
                var calls = new List<Uri>();

                var provider = new OfficialTaiwanHolidayProvider(testDirectory, (uri, token) =>
                {
                    calls.Add(uri);
                    return Task.FromResult(calls.Count == 1 ? metadataJson : Valid2026Csv);
                });

                var result = await provider.GetCalendarAsync(2026, CancellationToken.None);

                AssertTrue(result.Available, "download should succeed");
                AssertEqual(2, calls.Count, "expected exactly two network calls (metadata, then CSV)");
                AssertEqual(csvUrl, calls[1].ToString(), "second call should fetch the resolved CSV URI");
                AssertTrue(
                    File.Exists(Path.Combine(testDirectory, "holidays-2026.csv")),
                    "cache file should be created after a successful download");
            }
            finally
            {
                Directory.Delete(testDirectory, true);
            }
        }

        private static async Task ProviderFallsBackWhenUnavailableAsync()
        {
            var testDirectory = CreateTempDirectory();
            try
            {
                var provider = new OfficialTaiwanHolidayProvider(testDirectory, (uri, token) =>
                {
                    throw new HttpRequestException("simulated network failure");
                });

                var result = await provider.GetCalendarAsync(2026, CancellationToken.None);

                AssertFalse(result.Available, "should fall back when the network is unavailable");
                AssertFalse(string.IsNullOrEmpty(result.Message), "fallback message should be present");
            }
            finally
            {
                Directory.Delete(testDirectory, true);
            }
        }

        /// <summary>
        /// Builds a fixture matching the data.gov.tw v2 dataset metadata shape:
        /// a "result.distribution" array of resource entries, each with
        /// resourceDescription/resourceFormat/resourceDownloadUrl (and optionally
        /// resourceCharacterEncoding). Includes a Google-calendar (.ics) distractor
        /// entry alongside the real CSV entry, matching the real dataset's shape.
        /// </summary>
        private static string BuildMetadataJson(int year, string csvUrl, string csvEncoding)
        {
            var rocYear = (year - 1911).ToString(CultureInfo.InvariantCulture);
            var description = rocYear + "年中華民國政府行政機關辦公日曆表";

            return "{\"result\":{\"distribution\":[" +
                "{\"resourceDescription\":\"" + description + "(Google日曆)\"," +
                "\"resourceFormat\":\"ICS\"," +
                "\"resourceDownloadUrl\":\"https://data.gov.tw/fake/holidays-" + rocYear + ".ics\"}," +
                "{\"resourceDescription\":\"" + description + "\"," +
                "\"resourceFormat\":\"CSV\"," +
                "\"resourceDownloadUrl\":\"" + csvUrl + "\"," +
                "\"resourceCharacterEncoding\":\"" + csvEncoding + "\"}" +
                "]}}";
        }

        private static string CreateTempDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "AutoCheckInTests_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Builds a full-year official-format CSV (BOM + header + one row per day
        /// of <paramref name="year"/>), flagging only <paramref name="holidays"/>
        /// with the official "day off" flag (2). Every other day is flagged 0.
        /// </summary>
        internal static string BuildCalendarCsv(int year, params DateTime[] holidays)
        {
            var holidaySet = new HashSet<DateTime>((holidays ?? new DateTime[0]).Select(d => d.Date));
            var sb = new StringBuilder();
            sb.Append('﻿'); // official files are UTF-8 with a BOM; the parser must strip it
            sb.AppendLine("西元日期,星期,是否放假,備註");

            var start = new DateTime(year, 1, 1);
            var end = new DateTime(year, 12, 31);
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                var isHoliday = holidaySet.Contains(date);
                sb.AppendLine(string.Join(",", new[]
                {
                    date.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
                    DayName(date.DayOfWeek),
                    isHoliday ? "2" : "0",
                    string.Empty
                }));
            }

            return sb.ToString();
        }

        private static string DayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday: return "日";
                case DayOfWeek.Monday: return "一";
                case DayOfWeek.Tuesday: return "二";
                case DayOfWeek.Wednesday: return "三";
                case DayOfWeek.Thursday: return "四";
                case DayOfWeek.Friday: return "五";
                default: return "六";
            }
        }

        internal static void AssertTrue(bool condition, string message)
        {
            if (!condition) throw new Exception("Assertion failed: " + message);
        }

        internal static void AssertFalse(bool condition, string message)
        {
            if (condition) throw new Exception("Assertion failed: " + message);
        }

        internal static void AssertEqual<T>(T expected, T actual, string message)
        {
            if (!Equals(expected, actual))
            {
                throw new Exception(string.Format(
                    "Assertion failed: {0} (expected {1}, got {2})", message, expected, actual));
            }
        }

        internal static void AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException)
            {
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(
                    "Expected {0} but got {1}: {2}", typeof(TException).Name, ex.GetType().Name, ex.Message));
            }

            throw new Exception(string.Format("Expected {0} but no exception was thrown", typeof(TException).Name));
        }
    }
}
