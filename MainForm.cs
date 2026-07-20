using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCheckIn
{
    public partial class MainForm : Form
    {
        private readonly IUofClient m_uofClient;
        private readonly IAutoCheckScheduler m_dailyCheck;
        private readonly CancellationTokenSource m_formCancellation = new CancellationTokenSource();
        private readonly System.Windows.Forms.Timer m_timer;
        private readonly NotifyIcon m_trayIcon;
        private readonly ContextMenuStrip m_trayMenu;

        public MainForm()
            : this(new CrackClient(), new DailyCheck(new CrackClient()))
        {
            // Required by the WinForms designer. Program.cs is the runtime
            // composition root and uses the injectable constructor below.
        }

        internal MainForm(IUofClient uofClient, IAutoCheckScheduler dailyCheck)
        {
            m_uofClient = uofClient ?? throw new ArgumentNullException(nameof(uofClient));
            m_dailyCheck = dailyCheck ?? throw new ArgumentNullException(nameof(dailyCheck));
            InitializeComponent();

            Icon = Properties.Resources.cloudbox;

            m_timer = new System.Windows.Forms.Timer { Interval = 100 };
            m_timer.Tick += Timer_Tick;
            m_timer.Start();

            m_trayMenu = new ContextMenuStrip();
            m_trayMenu.Items.Add("Open", null, OnOpen);
            m_trayMenu.Items.Add("Exit", null, OnExit);

            m_trayIcon = new NotifyIcon
            {
                Text = "Auto check",
                Icon = Properties.Resources.cloudbox,
                ContextMenuStrip = m_trayMenu
            };
            m_trayIcon.DoubleClick += (s, e) => ShowMainWindow();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                m_trayIcon.Visible = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            m_formCancellation.Cancel();
            m_dailyCheck.Dispose();
            m_timer.Stop();
            m_trayIcon.Visible = false;
            base.OnFormClosing(e);
        }

        private void OnOpen(object sender, EventArgs e) => ShowMainWindow();
        private void OnExit(object sender, EventArgs e) => Application.Exit();

        private void ShowMainWindow()
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
            m_trayIcon.Visible = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblAutoStatus.Text = m_dailyCheck.StatusMessage;
            lblCheckIn.Text = m_dailyCheck.CheckInMessage;
            lblCheckOut.Text = m_dailyCheck.CheckOutMessage;
        }

        private async void btnCheckIn_Click(object sender, EventArgs e)
        {
            await RunManualCheckAsync(CheckAction.CheckIn);
        }

        private async void btnCheckOut_Click(object sender, EventArgs e)
        {
            await RunManualCheckAsync(CheckAction.CheckOut);
        }

        private void btnAutoCheck_Click(object sender, EventArgs e)
        {
            if (btnAutoCheck.Text == "Stop")
            {
                txtID.Enabled = true;
                txtPD.Enabled = true;
                btnCheckIn.Enabled = true;
                btnCheckOut.Enabled = true;
                btnAutoCheck.Text = "Auto Check";
                m_dailyCheck.Stop();
                return;
            }

            string loginId;
            string password;
            if (!TryGetCredentials(out loginId, out password))
            {
                return;
            }

            txtID.Enabled = false;
            txtPD.Enabled = false;
            btnCheckIn.Enabled = false;
            btnCheckOut.Enabled = false;
            btnAutoCheck.Text = "Stop";
            m_dailyCheck.Start(loginId, password);
        }

        private void cbxWorkingDay_CheckedChanged(object sender, EventArgs e)
        {
            var box = sender as CheckBox;
            int day;
            if (box != null && int.TryParse(box.Tag?.ToString(), out day) && day >= 0 && day <= 6)
            {
                m_dailyCheck.SetWorkingDay((DayOfWeek)day, box.Checked);
            }
        }

        private async Task RunManualCheckAsync(CheckAction action)
        {
            string loginId;
            string password;
            if (!TryGetCredentials(out loginId, out password))
            {
                return;
            }

            SetActionButtonsEnabled(false);
            lblStatus.Text = $"{action} in progress...";
            try
            {
                var result = await m_uofClient.CheckAsync(
                    loginId, password, action, m_formCancellation.Token);
                lblStatus.Text = result.Message;
            }
            catch (Exception ex)
            {
                // Keep the UI boundary safe even if an alternate IUofClient throws.
                Console.WriteLine("Unexpected manual check error: " + ex);
                lblStatus.Text = "Unexpected check error: " + ex.Message;
            }
            finally
            {
                if (!IsDisposed)
                {
                    SetActionButtonsEnabled(true);
                }
            }
        }

        private bool TryGetCredentials(out string loginId, out string password)
        {
            loginId = txtID.Text;
            password = txtPD.Text;

            if (string.IsNullOrWhiteSpace(loginId))
            {
                MessageBox.Show("Please enter ID.");
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Password.");
                return false;
            }

            return true;
        }

        private void SetActionButtonsEnabled(bool enabled)
        {
            btnCheckIn.Enabled = enabled;
            btnCheckOut.Enabled = enabled;
            btnAutoCheck.Enabled = enabled;
        }
    }
}
