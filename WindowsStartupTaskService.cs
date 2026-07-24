using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCheckIn
{
    internal sealed class WindowsStartupTaskService : IStartupTaskService
    {
        private const string TaskName = "AutoCheckIn";
        private readonly string m_executablePath;

        public WindowsStartupTaskService()
        {
            m_executablePath = Path.GetFullPath(Application.ExecutablePath);
        }

        public TaskScheduleResult GetStatus()
        {
            var command = Run($"/Query /TN \"{TaskName}\"");
            if (command.ExitCode != 0 && !IsTaskMissing(command.Output))
            {
                ErrorLogger.Log("Windows Task Scheduler status", CleanError(command.Output));
            }

            return command.ExitCode == 0
                ? new TaskScheduleResult(true, true, "Windows startup task is enabled.")
                : new TaskScheduleResult(true, false, "Windows startup task is not configured.");
        }

        public Task<TaskScheduleResult> CreateAsync()
        {
            return Task.Run(() =>
            {
                var taskCommand = "\\\"" + m_executablePath + "\\\" -a";
                var command = Run(
                    $"/Create /TN \"{TaskName}\" /SC ONLOGON /TR \"{taskCommand}\" /RL LIMITED /F");
                if (command.ExitCode != 0)
                {
                    ErrorLogger.Log("Windows Task Scheduler create", CleanError(command.Output));
                }

                return command.ExitCode == 0
                    ? new TaskScheduleResult(true, true, "Windows startup task was created.")
                    : new TaskScheduleResult(false, false, CleanError(command.Output));
            });
        }

        public Task<TaskScheduleResult> DeleteAsync()
        {
            return Task.Run(() =>
            {
                var command = Run($"/Delete /TN \"{TaskName}\" /F");
                if (command.ExitCode != 0)
                {
                    ErrorLogger.Log("Windows Task Scheduler delete", CleanError(command.Output));
                }

                return command.ExitCode == 0
                    ? new TaskScheduleResult(true, false, "Windows startup task was removed.")
                    : new TaskScheduleResult(false, true, CleanError(command.Output));
            });
        }

        private static CommandResult Run(string arguments)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Environment.SystemDirectory, "schtasks.exe"),
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = System.Text.Encoding.Default,
                    StandardErrorEncoding = System.Text.Encoding.Default
                };

                using (var process = Process.Start(startInfo))
                {
                    var output = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
                    if (!process.WaitForExit(15000))
                    {
                        try { process.Kill(); } catch { }
                        return new CommandResult(-1, "Task Scheduler command timed out.");
                    }
                    return new CommandResult(process.ExitCode, output);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Windows Task Scheduler", ex);
                return new CommandResult(-1, ex.Message);
            }
        }

        private static string CleanError(string output)
        {
            return string.IsNullOrWhiteSpace(output)
                ? "Windows Task Scheduler operation failed."
                : output.Trim();
        }

        private static bool IsTaskMissing(string output)
        {
            return !string.IsNullOrEmpty(output) &&
                (output.IndexOf("cannot find", StringComparison.OrdinalIgnoreCase) >= 0 ||
                 output.IndexOf("找不到", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private sealed class CommandResult
        {
            public CommandResult(int exitCode, string output)
            {
                ExitCode = exitCode;
                Output = output;
            }

            public int ExitCode { get; }
            public string Output { get; }
        }
    }
}
