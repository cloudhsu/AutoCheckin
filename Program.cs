using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCheckIn
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            var arguments = Environment.GetCommandLineArgs().Skip(1).ToArray();

            if (arguments.Any(argument => string.Equals(argument, "-selftest", StringComparison.OrdinalIgnoreCase)))
            {
                return SelfTest.RunAll();
            }

            if (arguments.Any(argument => string.Equals(argument, "-diagnose", StringComparison.OrdinalIgnoreCase)))
            {
                return RunLoginDiagnostic();
            }

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) =>
                ErrorLogger.Log("Application.ThreadException", args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                ErrorLogger.Log(
                    "AppDomain.UnhandledException; terminating=" + args.IsTerminating,
                    args.ExceptionObject as Exception ??
                    new Exception(args.ExceptionObject?.ToString() ?? "Unknown unhandled exception."));
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                ErrorLogger.Log("TaskScheduler.UnobservedTaskException", args.Exception);
                args.SetObserved();
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var autoStart = Environment.GetCommandLineArgs()
                .Skip(1)
                .Any(argument => string.Equals(argument, "-a", StringComparison.OrdinalIgnoreCase));
            var credentialStore = new DpapiCredentialStore();
            var holidayDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HolidayData");
            var holidayProvider = OfficialTaiwanHolidayProvider.CreateDefault(holidayDirectory);
            Application.Run(new MainForm(
                new CredentialSavingUofClient(new CrackClient(), credentialStore),
                new DailyCheck(new CredentialSavingUofClient(new CrackClient(), credentialStore), holidayProvider),
                credentialStore,
                new WindowsStartupTaskService(),
                UiTheme.Blue,
                autoStart));
            return 0;
        }

        /// <summary>
        /// Read-only UOF login probe (never submits check-in/check-out). Uses
        /// whatever credentials are already saved via normal app usage; it does
        /// not prompt for new ones, so it never handles a password that wasn't
        /// already trusted to disk by this same application.
        /// </summary>
        private static int RunLoginDiagnostic()
        {
            var credentials = new DpapiCredentialStore().Load();
            if (credentials == null)
            {
                MessageBox.Show(
                    "No saved credentials were found. Run the app normally and perform one manual " +
                    "check-in/out first (which saves credentials), then run 'AutoCheckIn.exe -diagnose' again.",
                    "AutoCheckIn diagnostic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 1;
            }

            var client = new CrackClient();
            var report = client.DiagnoseLoginAsync(credentials.LoginId, credentials.Password)
                .GetAwaiter().GetResult();

            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoCheckIn-diagnose.txt");
            try
            {
                File.WriteAllText(reportPath, report);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Diagnostic report write", ex);
            }

            MessageBox.Show(
                "Diagnostic finished. No check-in/check-out was submitted.\r\n\r\nReport: " + reportPath,
                "AutoCheckIn diagnostic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 0;
        }
    }
}
