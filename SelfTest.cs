using System;

namespace AutoCheckIn
{
    /// <summary>
    /// Lightweight in-process regression checks, run via <c>AutoCheckIn.exe -selftest</c>.
    /// This intentionally avoids adding a second MSBuild project/solution entry (a real
    /// risk of corrupting the old-style .sln/.csproj by hand) while still giving the
    /// window-eligibility policy and other pure logic a repeatable, automated check.
    /// A future move to a dedicated test project (see docs/superpowers/plans) can
    /// supersede this without changing what is being asserted.
    /// </summary>
    internal static class SelfTest
    {
        public static int RunAll()
        {
            var failures = 0;
            failures += Run(nameof(CheckIn_NotYetAtTargetMinute_DoesNotFire), CheckIn_NotYetAtTargetMinute_DoesNotFire);
            failures += Run(nameof(CheckIn_AtTargetMinute_Fires), CheckIn_AtTargetMinute_Fires);
            failures += Run(nameof(CheckIn_LateAfterOriginalHour_StillFiresBeforeCheckOutWindow), CheckIn_LateAfterOriginalHour_StillFiresBeforeCheckOutWindow);
            failures += Run(nameof(CheckIn_AfterCheckOutWindowOpens_DoesNotFire), CheckIn_AfterCheckOutWindowOpens_DoesNotFire);
            failures += Run(nameof(CheckIn_AlreadyDone_NeverFiresAgain), CheckIn_AlreadyDone_NeverFiresAgain);
            failures += Run(nameof(CheckIn_BeforeRetryCooldown_DoesNotFire), CheckIn_BeforeRetryCooldown_DoesNotFire);
            failures += Run(nameof(CheckOut_NotYetAtTargetMinute_DoesNotFire), CheckOut_NotYetAtTargetMinute_DoesNotFire);
            failures += Run(nameof(CheckOut_LateInEvening_StillFiresBeforeMidnight), CheckOut_LateInEvening_StillFiresBeforeMidnight);

            Console.WriteLine(failures == 0
                ? "SelfTest: all cases passed."
                : $"SelfTest: {failures} case(s) FAILED.");
            return failures == 0 ? 0 : 1;
        }

        private static int Run(string name, Action test)
        {
            try
            {
                test();
                Console.WriteLine("PASS " + name);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAIL " + name + ": " + ex.Message);
                return 1;
            }
        }

        private static void CheckIn_NotYetAtTargetMinute_DoesNotFire()
        {
            var now = new DateTime(2026, 8, 12, 8, 49, 0);
            AssertFalse(CheckWindowPolicy.ShouldAttemptCheckIn(now, 55, false, now.Date), "should not fire before target minute");
        }

        private static void CheckIn_AtTargetMinute_Fires()
        {
            var now = new DateTime(2026, 8, 12, 8, 55, 0);
            AssertTrue(CheckWindowPolicy.ShouldAttemptCheckIn(now, 55, false, now.Date), "should fire at target minute");
        }

        private static void CheckIn_LateAfterOriginalHour_StillFiresBeforeCheckOutWindow()
        {
            // Regression case: the original logic required now.Hour == 8 exactly, so a
            // machine waking up at 9:30 (e.g. after sleep) would silently and
            // permanently miss the day's check-in. This must now still fire.
            var now = new DateTime(2026, 8, 12, 9, 30, 0);
            AssertTrue(CheckWindowPolicy.ShouldAttemptCheckIn(now, 55, false, now.Date), "late check-in must still fire before check-out window opens");
        }

        private static void CheckIn_AfterCheckOutWindowOpens_DoesNotFire()
        {
            var now = new DateTime(2026, 8, 12, 18, 1, 0);
            AssertFalse(CheckWindowPolicy.ShouldAttemptCheckIn(now, 55, false, now.Date), "check-in window closes once check-out window opens");
        }

        private static void CheckIn_AlreadyDone_NeverFiresAgain()
        {
            var now = new DateTime(2026, 8, 12, 9, 0, 0);
            AssertFalse(CheckWindowPolicy.ShouldAttemptCheckIn(now, 55, true, now.Date), "already-succeeded check-in must not re-fire");
        }

        private static void CheckIn_BeforeRetryCooldown_DoesNotFire()
        {
            var now = new DateTime(2026, 8, 12, 8, 56, 0);
            var nextAttempt = now.AddMinutes(4); // still inside the 5-minute failure retry cooldown
            AssertFalse(CheckWindowPolicy.ShouldAttemptCheckIn(now, 55, false, nextAttempt), "must respect the failure retry cooldown");
        }

        private static void CheckOut_NotYetAtTargetMinute_DoesNotFire()
        {
            var now = new DateTime(2026, 8, 12, 18, 4, 0);
            AssertFalse(CheckWindowPolicy.ShouldAttemptCheckOut(now, 5, false, now.Date), "should not fire before check-out target minute");
        }

        private static void CheckOut_LateInEvening_StillFiresBeforeMidnight()
        {
            // Regression case: the original logic required now.Hour == 18 exactly.
            var now = new DateTime(2026, 8, 12, 23, 45, 0);
            AssertTrue(CheckWindowPolicy.ShouldAttemptCheckOut(now, 5, false, now.Date), "late check-out must still fire before the day rolls over");
        }

        private static void AssertTrue(bool condition, string message)
        {
            if (!condition) throw new Exception(message);
        }

        private static void AssertFalse(bool condition, string message)
        {
            if (condition) throw new Exception(message);
        }
    }
}
