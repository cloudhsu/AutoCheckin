using System;

namespace AutoCheckIn
{
    /// <summary>
    /// Pure, unit-testable decision logic for whether an automatic check-in or
    /// check-out attempt should fire right now. Extracted from DailyCheck so the
    /// window-eligibility rule can be tested without spinning up the scheduler
    /// worker loop.
    /// </summary>
    /// <remarks>
    /// Original behavior required <c>now.Hour == 8</c> (or 18) exactly, so a
    /// machine that was asleep or only booted after the top of that hour would
    /// silently and permanently miss the day's attempt. This policy keeps the
    /// same randomized target-minute trigger but widens eligibility so a late
    /// attempt still fires rather than being lost for the rest of the day.
    /// </remarks>
    internal static class CheckWindowPolicy
    {
        internal static readonly TimeSpan CheckInHour = TimeSpan.FromHours(8);
        internal static readonly TimeSpan CheckOutHour = TimeSpan.FromHours(18);

        // Once the check-in target minute has passed, a late attempt remains
        // eligible until the check-out window opens -- being late is still
        // better than never attempting again for the rest of the day.
        internal static readonly TimeSpan CheckInWindowEnd = CheckOutHour;

        // A late check-out attempt remains eligible until the day rolls over;
        // DailyCheck's own new-day reset handles the midnight boundary.
        internal static readonly TimeSpan CheckOutWindowEnd = TimeSpan.FromHours(24);

        public static bool ShouldAttemptCheckIn(
            DateTime now, int targetMinute, bool alreadyCheckedIn, DateTime nextAttempt)
        {
            return ShouldAttempt(now, CheckInHour, targetMinute, CheckInWindowEnd, alreadyCheckedIn, nextAttempt);
        }

        public static bool ShouldAttemptCheckOut(
            DateTime now, int targetMinute, bool alreadyCheckedOut, DateTime nextAttempt)
        {
            return ShouldAttempt(now, CheckOutHour, targetMinute, CheckOutWindowEnd, alreadyCheckedOut, nextAttempt);
        }

        private static bool ShouldAttempt(
            DateTime now,
            TimeSpan windowStartHour,
            int targetMinute,
            TimeSpan windowEnd,
            bool alreadyDone,
            DateTime nextAttempt)
        {
            if (alreadyDone || now < nextAttempt)
            {
                return false;
            }

            var target = now.Date.Add(windowStartHour).AddMinutes(targetMinute);
            return now >= target && now.TimeOfDay < windowEnd;
        }
    }
}
