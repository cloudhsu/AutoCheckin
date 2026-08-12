using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AutoCheckIn.Tests
{
    /// <summary>
    /// Dependency-free regression test runner for AutoCheckIn. Each test is a
    /// named zero-argument Action; Main runs them all, prints PASS/FAIL per
    /// case, and returns a non-zero exit code if any failed.
    /// </summary>
    internal static class Program
    {
        internal static readonly string Valid2026Csv = BuildCalendarCsv(2026);

        [STAThread]
        static int Main()
        {
            var tests = new List<Tuple<string, Action>>
            {
                Tuple.Create<string, Action>(nameof(ParserReadsOnlyFlagTwoDates), ParserReadsOnlyFlagTwoDates),
                Tuple.Create<string, Action>(nameof(ParserRejectsWrongYearAndMalformedFiles), ParserRejectsWrongYearAndMalformedFiles),
            };

            var failures = 0;
            foreach (var test in tests)
            {
                try
                {
                    test.Item2();
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
