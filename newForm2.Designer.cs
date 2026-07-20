namespace AutoCheckIn
{
    partial class newForm2
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.Label appLabel;
        private System.Windows.Forms.Label navAccount;
        private System.Windows.Forms.Label navManual;
        private System.Windows.Forms.Label navSchedule;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label heading;
        private System.Windows.Forms.Label greeting;
        private System.Windows.Forms.Panel accountCard;
        private System.Windows.Forms.Label accountTitle;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Panel actionCard;
        private System.Windows.Forms.Label actionTitle;
        private System.Windows.Forms.Button inButton;
        private System.Windows.Forms.Button outButton;
        private System.Windows.Forms.Panel scheduleCard;
        private System.Windows.Forms.Label scheduleTitle;
        private System.Windows.Forms.Label daysLabel;
        private System.Windows.Forms.CheckBox mon;
        private System.Windows.Forms.CheckBox tue;
        private System.Windows.Forms.CheckBox wed;
        private System.Windows.Forms.CheckBox thu;
        private System.Windows.Forms.CheckBox fri;
        private System.Windows.Forms.Label inSchedule;
        private System.Windows.Forms.Label outSchedule;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label footerStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sidebar = new System.Windows.Forms.Panel(); this.logoLabel = new System.Windows.Forms.Label(); this.appLabel = new System.Windows.Forms.Label();
            this.navAccount = new System.Windows.Forms.Label(); this.navManual = new System.Windows.Forms.Label(); this.navSchedule = new System.Windows.Forms.Label(); this.versionLabel = new System.Windows.Forms.Label();
            this.heading = new System.Windows.Forms.Label(); this.greeting = new System.Windows.Forms.Label();
            this.accountCard = new System.Windows.Forms.Panel(); this.accountTitle = new System.Windows.Forms.Label(); this.idLabel = new System.Windows.Forms.Label(); this.idTextBox = new System.Windows.Forms.TextBox(); this.passwordLabel = new System.Windows.Forms.Label(); this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.actionCard = new System.Windows.Forms.Panel(); this.actionTitle = new System.Windows.Forms.Label(); this.inButton = new System.Windows.Forms.Button(); this.outButton = new System.Windows.Forms.Button();
            this.scheduleCard = new System.Windows.Forms.Panel(); this.scheduleTitle = new System.Windows.Forms.Label(); this.daysLabel = new System.Windows.Forms.Label();
            this.mon = new System.Windows.Forms.CheckBox(); this.tue = new System.Windows.Forms.CheckBox(); this.wed = new System.Windows.Forms.CheckBox(); this.thu = new System.Windows.Forms.CheckBox(); this.fri = new System.Windows.Forms.CheckBox();
            this.inSchedule = new System.Windows.Forms.Label(); this.outSchedule = new System.Windows.Forms.Label(); this.startButton = new System.Windows.Forms.Button(); this.footerStatus = new System.Windows.Forms.Label();
            this.sidebar.SuspendLayout(); this.accountCard.SuspendLayout(); this.actionCard.SuspendLayout(); this.scheduleCard.SuspendLayout(); this.SuspendLayout();
            // sidebar
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(30, 64, 175); this.sidebar.Controls.Add(this.logoLabel); this.sidebar.Controls.Add(this.appLabel); this.sidebar.Controls.Add(this.navAccount); this.sidebar.Controls.Add(this.navManual); this.sidebar.Controls.Add(this.navSchedule); this.sidebar.Controls.Add(this.versionLabel); this.sidebar.Dock = System.Windows.Forms.DockStyle.Left; this.sidebar.Size = new System.Drawing.Size(210, 620);
            this.logoLabel.BackColor = System.Drawing.Color.FromArgb(96, 165, 250); this.logoLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 20F); this.logoLabel.ForeColor = System.Drawing.Color.White; this.logoLabel.Location = new System.Drawing.Point(26, 25); this.logoLabel.Size = new System.Drawing.Size(52, 52); this.logoLabel.Text = "A"; this.logoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.appLabel.AutoSize = true; this.appLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 13F); this.appLabel.ForeColor = System.Drawing.Color.White; this.appLabel.Location = new System.Drawing.Point(87, 35); this.appLabel.Text = "AutoCheck";
            this.navAccount.BackColor = System.Drawing.Color.FromArgb(37, 75, 190); this.navAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.navAccount.ForeColor = System.Drawing.Color.White; this.navAccount.Location = new System.Drawing.Point(18, 125); this.navAccount.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.navAccount.Size = new System.Drawing.Size(174, 45); this.navAccount.Text = "01   Account"; this.navAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navManual.Font = new System.Drawing.Font("Segoe UI", 10F); this.navManual.ForeColor = System.Drawing.Color.FromArgb(191, 219, 254); this.navManual.Location = new System.Drawing.Point(18, 178); this.navManual.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.navManual.Size = new System.Drawing.Size(174, 45); this.navManual.Text = "02   Quick action"; this.navManual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navSchedule.Font = new System.Drawing.Font("Segoe UI", 10F); this.navSchedule.ForeColor = System.Drawing.Color.FromArgb(191, 219, 254); this.navSchedule.Location = new System.Drawing.Point(18, 231); this.navSchedule.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.navSchedule.Size = new System.Drawing.Size(174, 45); this.navSchedule.Text = "03   Schedule"; this.navSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.versionLabel.AutoSize = true; this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(147, 197, 253); this.versionLabel.Location = new System.Drawing.Point(26, 575); this.versionLabel.Text = "UOF Assistant  v1.0";
            // heading
            this.heading.AutoSize = true; this.heading.Font = new System.Drawing.Font("Segoe UI Semibold", 21F); this.heading.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); this.heading.Location = new System.Drawing.Point(244, 25); this.heading.Text = "Attendance dashboard";
            this.greeting.AutoSize = true; this.greeting.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.greeting.Location = new System.Drawing.Point(248, 70); this.greeting.Text = "Configure your account and daily check-in schedule.";
            // accountCard
            this.accountCard.BackColor = System.Drawing.Color.White; this.accountCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.accountCard.Controls.Add(this.accountTitle); this.accountCard.Controls.Add(this.idLabel); this.accountCard.Controls.Add(this.idTextBox); this.accountCard.Controls.Add(this.passwordLabel); this.accountCard.Controls.Add(this.passwordTextBox); this.accountCard.Location = new System.Drawing.Point(250, 115); this.accountCard.Size = new System.Drawing.Size(330, 220);
            this.accountTitle.AutoSize = true; this.accountTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F); this.accountTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); this.accountTitle.Location = new System.Drawing.Point(20, 18); this.accountTitle.Text = "Account credentials";
            this.idLabel.AutoSize = true; this.idLabel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); this.idLabel.Location = new System.Drawing.Point(21, 60); this.idLabel.Text = "Employee ID";
            this.idTextBox.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.idTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.idTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F); this.idTextBox.Location = new System.Drawing.Point(24, 84); this.idTextBox.Size = new System.Drawing.Size(280, 31); this.idTextBox.Text = "EMPLOYEE01";
            this.passwordLabel.AutoSize = true; this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); this.passwordLabel.Location = new System.Drawing.Point(21, 130); this.passwordLabel.Text = "Password";
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F); this.passwordTextBox.Location = new System.Drawing.Point(24, 154); this.passwordTextBox.PasswordChar = '●'; this.passwordTextBox.Size = new System.Drawing.Size(280, 31); this.passwordTextBox.Text = "password";
            // actionCard
            this.actionCard.BackColor = System.Drawing.Color.White; this.actionCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.actionCard.Controls.Add(this.actionTitle); this.actionCard.Controls.Add(this.inButton); this.actionCard.Controls.Add(this.outButton); this.actionCard.Location = new System.Drawing.Point(598, 115); this.actionCard.Size = new System.Drawing.Size(276, 220);
            this.actionTitle.AutoSize = true; this.actionTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F); this.actionTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); this.actionTitle.Location = new System.Drawing.Point(20, 18); this.actionTitle.Text = "Quick action";
            this.inButton.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); this.inButton.FlatAppearance.BorderSize = 0; this.inButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.inButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.inButton.ForeColor = System.Drawing.Color.White; this.inButton.Location = new System.Drawing.Point(24, 68); this.inButton.Size = new System.Drawing.Size(226, 48); this.inButton.Text = "CHECK IN"; this.inButton.UseVisualStyleBackColor = false;
            this.outButton.BackColor = System.Drawing.Color.White; this.outButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225); this.outButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.outButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.outButton.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.outButton.Location = new System.Drawing.Point(24, 133); this.outButton.Size = new System.Drawing.Size(226, 48); this.outButton.Text = "CHECK OUT"; this.outButton.UseVisualStyleBackColor = false;
            // scheduleCard
            this.scheduleCard.BackColor = System.Drawing.Color.White; this.scheduleCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.scheduleCard.Controls.Add(this.scheduleTitle); this.scheduleCard.Controls.Add(this.daysLabel); this.scheduleCard.Controls.Add(this.mon); this.scheduleCard.Controls.Add(this.tue); this.scheduleCard.Controls.Add(this.wed); this.scheduleCard.Controls.Add(this.thu); this.scheduleCard.Controls.Add(this.fri); this.scheduleCard.Controls.Add(this.inSchedule); this.scheduleCard.Controls.Add(this.outSchedule); this.scheduleCard.Controls.Add(this.startButton); this.scheduleCard.Location = new System.Drawing.Point(250, 355); this.scheduleCard.Size = new System.Drawing.Size(624, 190);
            this.scheduleTitle.AutoSize = true; this.scheduleTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F); this.scheduleTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); this.scheduleTitle.Location = new System.Drawing.Point(20, 17); this.scheduleTitle.Text = "Automatic schedule";
            this.daysLabel.AutoSize = true; this.daysLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.daysLabel.Location = new System.Drawing.Point(21, 57); this.daysLabel.Text = "Working days";
            this.mon.Location = new System.Drawing.Point(24, 83); this.mon.Text = "Mon"; this.tue.Location = new System.Drawing.Point(88, 83); this.tue.Text = "Tue"; this.wed.Location = new System.Drawing.Point(150, 83); this.wed.Text = "Wed"; this.thu.Location = new System.Drawing.Point(218, 83); this.thu.Text = "Thu"; this.fri.Location = new System.Drawing.Point(282, 83); this.fri.Text = "Fri";
            this.mon.AutoSize = true; this.mon.Checked = true; this.mon.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.tue.AutoSize = true; this.tue.Checked = true; this.tue.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.wed.AutoSize = true; this.wed.Checked = true; this.wed.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.thu.AutoSize = true; this.thu.Checked = true; this.thu.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.fri.AutoSize = true; this.fri.Checked = true; this.fri.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.inSchedule.AutoSize = true; this.inSchedule.BackColor = System.Drawing.Color.FromArgb(239, 246, 255); this.inSchedule.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216); this.inSchedule.Location = new System.Drawing.Point(24, 130); this.inSchedule.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6); this.inSchedule.Text = "IN   08:50–08:58";
            this.outSchedule.AutoSize = true; this.outSchedule.BackColor = System.Drawing.Color.FromArgb(239, 246, 255); this.outSchedule.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216); this.outSchedule.Location = new System.Drawing.Point(173, 130); this.outSchedule.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6); this.outSchedule.Text = "OUT   18:01–18:09";
            this.startButton.BackColor = System.Drawing.Color.FromArgb(22, 163, 74); this.startButton.FlatAppearance.BorderSize = 0; this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.startButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.startButton.ForeColor = System.Drawing.Color.White; this.startButton.Location = new System.Drawing.Point(426, 65); this.startButton.Size = new System.Drawing.Size(170, 76); this.startButton.Text = "START\r\nAUTO CHECK"; this.startButton.UseVisualStyleBackColor = false;
            // footer
            this.footerStatus.AutoSize = true; this.footerStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.footerStatus.Location = new System.Drawing.Point(250, 572); this.footerStatus.Text = "●  System ready     |     Next action will appear here";
            // newForm2
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.ClientSize = new System.Drawing.Size(910, 620); this.Controls.Add(this.footerStatus); this.Controls.Add(this.scheduleCard); this.Controls.Add(this.actionCard); this.Controls.Add(this.accountCard); this.Controls.Add(this.greeting); this.Controls.Add(this.heading); this.Controls.Add(this.sidebar); this.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; this.MaximizeBox = false; this.Name = "newForm2"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Auto Check-In — Fluent Blue";
            this.sidebar.ResumeLayout(false); this.sidebar.PerformLayout(); this.accountCard.ResumeLayout(false); this.accountCard.PerformLayout(); this.actionCard.ResumeLayout(false); this.actionCard.PerformLayout(); this.scheduleCard.ResumeLayout(false); this.scheduleCard.PerformLayout(); this.ResumeLayout(false); this.PerformLayout();
        }
    }
}
