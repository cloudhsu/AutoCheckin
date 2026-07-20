using System;
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
            var credentialStore = new DpapiCredentialStore();
            Application.Run(new MainForm(
                new CredentialSavingUofClient(new CrackClient(), credentialStore),
                new DailyCheck(new CredentialSavingUofClient(new CrackClient(), credentialStore)),
                credentialStore,
                new WindowsStartupTaskService(),
                UiTheme.Random()));
        }
    }
}
