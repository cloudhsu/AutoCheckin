using System;
using System.Linq;
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
                UiTheme.Random(),
                autoStart));
        }
    }
}
