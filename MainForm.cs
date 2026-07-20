using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCheckIn
{
    public partial class MainForm : newForm1
    {
        private readonly IUofClient m_uofClient;
        private readonly IAutoCheckScheduler m_dailyCheck;
        private readonly CancellationTokenSource m_formCancellation = new CancellationTokenSource();
        private readonly System.Windows.Forms.Timer m_timer;
        private readonly NotifyIcon m_trayIcon;
        private readonly ContextMenuStrip m_trayMenu;

        public MainForm()
            : this(new CrackClient(), new DailyCheck(new CrackClient()), UiTheme.Amber)
        {
            // Required by the WinForms designer. Program.cs is the runtime
            // composition root and uses the injectable constructor below.
        }

        internal MainForm(IUofClient uofClient, IAutoCheckScheduler dailyCheck, UiTheme theme)
        {
            m_uofClient = uofClient ?? throw new ArgumentNullException(nameof(uofClient));
            m_dailyCheck = dailyCheck ?? throw new ArgumentNullException(nameof(dailyCheck));
            Icon = Properties.Resources.cloudbox;
            ShowIcon = true;
            ApplyTheme(theme ?? UiTheme.Amber);

            checkInButton.Click += btnCheckIn_Click;
            checkOutButton.Click += btnCheckOut_Click;
            autoCheckButton.Click += btnAutoCheck_Click;
            monCheckBox.CheckedChanged += cbxWorkingDay_CheckedChanged;
            tueCheckBox.CheckedChanged += cbxWorkingDay_CheckedChanged;
            wedCheckBox.CheckedChanged += cbxWorkingDay_CheckedChanged;
            thuCheckBox.CheckedChanged += cbxWorkingDay_CheckedChanged;
            friCheckBox.CheckedChanged += cbxWorkingDay_CheckedChanged;

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

        private void ApplyTheme(UiTheme theme)
        {
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = theme.Window;
            ForeColor = theme.Text;
            Text = $"Auto Check V0.7 — {theme.Name}";

            headerPanel.BackColor = theme.Window;
            StylePanel(accountPanel, theme);
            StylePanel(manualPanel, theme);
            StylePanel(schedulePanel, theme);
            statusPanel.BackColor = theme.Window;
            StyleTextBox(idTextBox, theme);
            StyleTextBox(passwordTextBox, theme);
            StylePrimaryButton(checkInButton, theme);
            StyleSecondaryButton(checkOutButton, theme);
            StylePrimaryButton(autoCheckButton, theme);
            statusLabel.ForeColor = theme.MutedText;
            checkInTimeLabel.ForeColor = theme.Accent;
            checkOutTimeLabel.ForeColor = theme.Accent;
        }

        private static void StylePanel(Panel panel, UiTheme theme)
        {
            panel.BackColor = theme.Surface;
            panel.ForeColor = theme.Text;
            foreach (Control child in panel.Controls)
            {
                child.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
                if (!(child is TextBox) && !(child is Button))
                {
                    child.ForeColor = theme.Text;
                }
            }
        }

        private static void StyleTextBox(TextBox textBox, UiTheme theme)
        {
            textBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            textBox.BackColor = theme.Window;
            textBox.ForeColor = theme.Text;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void StylePrimaryButton(Button button, UiTheme theme)
        {
            button.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            button.BackColor = theme.Accent;
            button.ForeColor = theme.Name == "Amber Console" ? Color.FromArgb(24, 24, 27) : Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
        }

        private static void StyleSecondaryButton(Button button, UiTheme theme)
        {
            button.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            button.BackColor = theme.Surface;
            button.ForeColor = theme.Text;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = theme.MutedText;
            button.UseVisualStyleBackColor = false;
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
            if (!string.IsNullOrEmpty(m_dailyCheck.StatusMessage))
            {
                statusLabel.Text = m_dailyCheck.StatusMessage;
            }
            checkInTimeLabel.Text = m_dailyCheck.CheckInMessage;
            checkOutTimeLabel.Text = m_dailyCheck.CheckOutMessage;
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
            if (autoCheckButton.Text == "Stop")
            {
                idTextBox.Enabled = true;
                passwordTextBox.Enabled = true;
                checkInButton.Enabled = true;
                checkOutButton.Enabled = true;
                autoCheckButton.Text = "START AUTO CHECK";
                m_dailyCheck.Stop();
                return;
            }

            string loginId;
            string password;
            if (!TryGetCredentials(out loginId, out password))
            {
                return;
            }

            idTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            checkInButton.Enabled = false;
            checkOutButton.Enabled = false;
            autoCheckButton.Text = "Stop";
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
            statusLabel.Text = $"{action} in progress...";
            try
            {
                var result = await m_uofClient.CheckAsync(
                    loginId, password, action, m_formCancellation.Token);
                statusLabel.Text = result.Message;
            }
            catch (Exception ex)
            {
                // Keep the UI boundary safe even if an alternate IUofClient throws.
                Console.WriteLine("Unexpected manual check error: " + ex);
                statusLabel.Text = "Unexpected check error: " + ex.Message;
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
            loginId = idTextBox.Text;
            password = passwordTextBox.Text;

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
            checkInButton.Enabled = enabled;
            checkOutButton.Enabled = enabled;
            autoCheckButton.Enabled = enabled;
        }
    }
}
