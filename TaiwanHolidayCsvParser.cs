using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoCheckIn
{
    /// <summary>
    /// Validates and parses the official Taiwan government holiday CSV format
    /// (dataset 14718). Strict by design: a truncated download, wrong-year file,
    /// or format drift must fail loudly rather than silently become a valid
    /// (and wrong) cache file that the automatic scheduler would trust.
    /// </summary>
    internal static class TaiwanHolidayCsvParser
    {
        private static readonly string[] ExpectedHeaders = { "西元日期", "星期", "是否放假", "備註" };
        private static readonly string[] DateFormats = { "yyyyMMdd", "yyyy/M/d" };

        public static HolidayCalendarResult Parse(string csv, int expectedYear)
        {
            if (csv == null)
            {
                throw new ArgumentNullException(nameof(csv));
            }

            // Official files are UTF-8 with a BOM. StreamReader normally strips this
            // automatically, but a string that arrived already-decoded (a cached
            // file re-read as text, or a fixture) may still carry the literal BOM
            // character, so strip it explicitly regardless of how the text arrived.
            if (csv.Length > 0 && csv[0] == '﻿')
            {
                csv = csv.Substring(1);
            }

            var lines = new List<string>();
            using (var reader = new StringReader(csv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }

            if (lines.Count == 0)
            {
                throw new FormatException("Holiday CSV is empty.");
            }

            var header = ParseLine(lines[0]);
            if (header.Count != ExpectedHeaders.Length || !header.SequenceEqual(ExpectedHeaders, StringComparer.Ordinal))
            {
                throw new FormatException("Holiday CSV header does not match the official format.");
            }

            var holidays = new List<DateTime>();
            var seenDates = new HashSet<DateTime>();

            for (var i = 1; i < lines.Count; i++)
            {
                var fields = ParseLine(lines[i]);
                if (fields.Count != ExpectedHeaders.Length)
                {
                    throw new FormatException(
                        "Holiday CSV row " + (i + 1) + " does not have " + ExpectedHeaders.Length + " fields.");
                }

                DateTime date;
                if (!TryParseDate(fields[0], out date))
                {
                    throw new FormatException(
                        "Holiday CSV row " + (i + 1) + " has an unrecognized date: '" + fields[0] + "'.");
                }

                if (date.Year != expectedYear)
                {
                    throw new FormatException(
                        "Holiday CSV row " + (i + 1) + " date " + date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) +
                        " does not belong to expected year " + expectedYear + ".");
                }

                if (!seenDates.Add(date))
                {
                    throw new FormatException(
                        "Holiday CSV row " + (i + 1) + " duplicates date " + date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".");
                }

                if (fields[2] == "2")
                {
                    holidays.Add(date);
                }
            }

            // A truncated or partial download must never become a "valid" cache
            // file -- require the file to actually cover the whole year.
            var expectedDayCount = DateTime.IsLeapYear(expectedYear) ? 366 : 365;
            if (seenDates.Count != expectedDayCount)
            {
                throw new FormatException(
                    "Holiday CSV has " + seenDates.Count + " dates; expected exactly " + expectedDayCount +
                    " for year " + expectedYear + ".");
            }

            return new HolidayCalendarResult(true, holidays, "Loaded " + expectedYear + " holiday calendar.");
        }

        private static bool TryParseDate(string text, out DateTime date)
        {
            return DateTime.TryParseExact(
                text, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        /// <summary>
        /// Minimal quote-aware CSV line splitter: comma-separated fields, fields
        /// optionally wrapped in double quotes, and a doubled quote ("") as an
        /// escaped literal quote inside a quoted field.
        /// </summary>
        private static List<string> ParseLine(string line)
        {
            var fields = new List<string>();
            var current = new StringBuilder();
            var inQuotes = false;

            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            current.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
                else if (c == '"')
                {
                    inQuotes = true;
                }
                else if (c == ',')
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            fields.Add(current.ToString());
            return fields;
        }
    }
}
