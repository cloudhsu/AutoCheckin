using System;
using System.IO;
using System.Text;

namespace AutoCheckIn
{
    internal static class ErrorLogger
    {
        private const string LogFileName = "AutoCheckIn-error.txt";
        private static readonly object SyncRoot = new object();

        public static string LogPath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFileName);

        public static void Log(string source, Exception exception)
        {
            if (exception != null)
            {
                Write(source, exception.ToString());
            }
        }

        public static void Log(string source, string message)
        {
            Write(source, message);
        }

        private static void Write(string source, string details)
        {
            try
            {
                var entry = new StringBuilder()
                    .AppendLine("------------------------------------------------------------")
                    .AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    .AppendLine("Source: " + (source ?? "Unknown"))
                    .AppendLine(details ?? string.Empty)
                    .ToString();

                lock (SyncRoot)
                {
                    File.AppendAllText(LogPath, entry, Encoding.UTF8);
                }
            }
            catch
            {
                // Logging must never cause a second application failure.
            }
        }
    }
}
