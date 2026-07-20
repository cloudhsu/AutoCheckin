using System;
using System.Windows.Forms;

namespace AutoCheckIn
{
    public partial class MainForm : Form
    {
        CrackClient m_crack = new CrackClient();
        DailyCheck m_dailyCheck = new DailyCheck();
        Timer m_timer;

        NotifyIcon m_trayIcon;
        ContextMenuStrip m_trayMenu;

        public MainForm()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.cloudbox;

            m_timer = new Timer();
            m_timer.Interval = 100;
            m_timer.Tick += Timer_Tick;
            m_timer.Start();

            m_trayMenu = new ContextMenuStrip();
            m_trayMenu.Items.Add("Open", null, OnOpen);
            m_trayMenu.Items.Add("Exit", null, OnExit);

            m_trayIcon = new NotifyIcon();
            m_trayIcon.Text = "Auto check";
            m_trayIcon.Icon = Properties.Resources.cloudbox;
            m_trayIcon.ContextMenuStrip = m_trayMenu;
            //m_trayIcon.Visible = true;
            m_trayIcon.DoubleClick += (s, e) => ShowMainWindow();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                m_trayIcon.Visible = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                m_trayIcon.Visible = false;
                base.OnFormClosing(e);
            }
        }

        void OnOpen(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void ShowMainWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            m_trayIcon.Visible = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.lblAutoStatus.Text = m_dailyCheck.StatusMessage;
            this.lblCheckIn.Text = m_dailyCheck.CheckInMessage;
            this.lblCheckOut.Text = m_dailyCheck.CheckOutMessage;

        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string sId = this.txtID.Text;
            string sPd = this.txtPD.Text;
            


            if(string.IsNullOrEmpty(sId))
            {
                MessageBox.Show("Please enter ID.");
                return;
            }
            if (string.IsNullOrEmpty(sPd))
            {
                MessageBox.Show("Please enter Password.");
                return;
            }

            this.lblStatus.Text = "";

            Console.WriteLine("Check In with ID={0}, PD={1}", sId, sPd);

            m_crack.LoginID = sId;
            m_crack.Password = sPd;
            m_crack.checkInOut(CrackClient.CheckAction.CheckIn);

            this.lblStatus.Text = m_crack.StatusMessage;
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            string sId = this.txtID.Text;
            string sPd = this.txtPD.Text;
            

            if (string.IsNullOrEmpty(sId))
            {
                MessageBox.Show("Please enter ID.");
                return;
            }
            if (string.IsNullOrEmpty(sPd))
            {
                MessageBox.Show("Please enter Password.");
                return;
            }

            this.lblStatus.Text = "";

            Console.WriteLine("Check Out with ID={0}, PD={1}", sId, sPd);

            m_crack.LoginID = sId;
            m_crack.Password = sPd;

            m_crack.checkInOut(CrackClient.CheckAction.CheckOut);

            this.lblStatus.Text = m_crack.StatusMessage;
        }

        private void btnAutoCheck_Click(object sender, EventArgs e)
        {
            if (this.btnAutoCheck.Text == "Stop")
            {
                this.txtID.Enabled = true;
                this.txtPD.Enabled = true;
                this.btnCheckIn.Enabled = true;
                this.btnCheckOut.Enabled = true;
                this.btnAutoCheck.Text = "Auto Check";
                m_dailyCheck.Stop();
            }
            else
            {
                string sId = this.txtID.Text;
                string sPd = this.txtPD.Text;

                if (string.IsNullOrEmpty(sId))
                {
                    MessageBox.Show("Please enter ID.");
                    return;
                }
                if (string.IsNullOrEmpty(sPd))
                {
                    MessageBox.Show("Please enter Password.");
                    return;
                }

                Console.WriteLine("Auto Check with ID={0}, PD={1}", sId, sPd);

                m_dailyCheck.LoginID = sId;
                m_dailyCheck.Password = sPd;

                this.txtID.Enabled = false;
                this.txtPD.Enabled = false;
                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = false;
                this.btnAutoCheck.Text = "Stop";
                m_dailyCheck.Start();
            }
        }

        private void cbxWorkingDay_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;

            if (box != null)
            {
                int day = int.Parse(box.Tag.ToString());
                Console.WriteLine($"CheckBox={box.Name} is {box.Checked}");

                m_dailyCheck.setDayOff(day,box.Checked);
            }

        }
        
    }
}
