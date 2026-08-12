using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoCheckIn
{
    /// <summary>
    /// Immutable result of loading one calendar year's Taiwan government
    /// holiday data: either a usable set of holiday dates, or an explanation
    /// of why none is available (network/parse/filesystem failure).
    /// </summary>
    internal sealed class HolidayCalendarResult
    {
        public HolidayCalendarResult(bool available, IEnumerable<DateTime> holidays, string message)
        {
            Available = available;
            Holidays = new HashSet<DateTime>((holidays ?? Enumerable.Empty<DateTime>()).Select(d => d.Date));
            Message = message ?? string.Empty;
        }

        public bool Available { get; }
        public HashSet<DateTime> Holidays { get; }
        public string Message { get; }

        public static HolidayCalendarResult Unavailable(string message) =>
            new HolidayCalendarResult(false, Enumerable.Empty<DateTime>(), message);
    }
}
