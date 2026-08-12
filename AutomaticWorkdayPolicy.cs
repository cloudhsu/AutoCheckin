using System;

namespace AutoCheckIn
{
    /// <summary>
    /// Pure eligibility rule combining the user's weekday selection with the
    /// (possibly unavailable) holiday calendar. A selected weekday is suppressed
    /// by a known holiday; missing holiday data never blocks a selected weekday,
    /// and holiday data never enables a day the user did not select.
    /// </summary>
    internal static class AutomaticWorkdayPolicy
    {
        public static bool IsEligible(DateTime date, bool weekdaySelected, HolidayCalendarResult calendar)
        {
            if (!weekdaySelected)
            {
                return false;
            }

            return calendar == null || !calendar.Available || !calendar.Holidays.Contains(date.Date);
        }
    }
}
