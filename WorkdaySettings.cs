using System;
using System.IO;

namespace AutoCheckIn
{
    /// <summary>
    /// Persists the user's Mon-Fri working-day checkbox selection beside the
    /// executable, so it survives an application restart. This data is not
    /// sensitive (unlike credentials), so it is stored as plain text rather
    /// than DPAPI-encrypted, following the same "small class, file beside the
    /// executable" shape already used by DpapiCredentialStore and ErrorLogger.
    /// </summary>
    internal static class WorkdaySettings
    {
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "workdays.cfg");

        /// <summary>
        /// Returns the saved Mon-Fri flags (index 0 = Monday .. 4 = Friday), or
        /// null if no saved file exists or it could not be parsed -- callers
        /// should fall back to their own default (currently all-true) in that case.
        /// </summary>
        public static bool[] Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return null;
                }

                var text = File.ReadAllText(FilePath).Trim();
                if (text.Length != 5)
                {
                    return null;
                }

                var flags = new bool[5];
                for (var i = 0; i < 5; i++)
                {
                    if (text[i] != '0' && text[i] != '1')
                    {
                        return null;
                    }

                    flags[i] = text[i] == '1';
                }

                return flags;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Workday settings load", ex);
                return null;
            }
        }

        /// <summary>
        /// Saves the Mon-Fri flags (index 0 = Monday .. 4 = Friday). Failures are
        /// logged and swallowed -- losing this preference must never interrupt
        /// the working scheduler.
        /// </summary>
        public static void Save(bool[] mondayToFridayFlags)
        {
            if (mondayToFridayFlags == null || mondayToFridayFlags.Length != 5)
            {
                throw new ArgumentException("Expected exactly five flags (Monday..Friday).", nameof(mondayToFridayFlags));
            }

            try
            {
                var chars = new char[5];
                for (var i = 0; i < 5; i++)
                {
                    chars[i] = mondayToFridayFlags[i] ? '1' : '0';
                }

                var temporaryPath = FilePath + ".tmp";
                File.WriteAllText(temporaryPath, new string(chars));
                if (File.Exists(FilePath))
                {
                    File.Replace(temporaryPath, FilePath, null);
                }
                else
                {
                    File.Move(temporaryPath, FilePath);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Workday settings save", ex);
            }
        }
    }
}
