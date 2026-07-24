using System;
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
        static void Main()
        {
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
            Application.Run(new MainForm(
                new CredentialSavingUofClient(new CrackClient(), credentialStore),
                new DailyCheck(new CredentialSavingUofClient(new CrackClient(), credentialStore)),
                credentialStore,
                new WindowsStartupTaskService(),
                UiTheme.Blue,
                autoStart));
        }
    }
}
