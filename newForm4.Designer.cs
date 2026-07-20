namespace AutoCheckIn
{
    partial class newForm4
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label brand;
        private System.Windows.Forms.Label pageTitle;
        private System.Windows.Forms.Label themeHint;
        private System.Windows.Forms.Panel tealSwatch;
        private System.Windows.Forms.Panel blueSwatch;
        private System.Windows.Forms.Panel amberSwatch;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label accountHeading;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label manualHeading;
        private System.Windows.Forms.Button checkInButton;
        private System.Windows.Forms.Button checkOutButton;
        private System.Windows.Forms.Label securityNote;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label scheduleHeading;
        private System.Windows.Forms.Label scheduleCaption;
        private System.Windows.Forms.FlowLayoutPanel dayPanel;
        private System.Windows.Forms.CheckBox mon;
        private System.Windows.Forms.CheckBox tue;
        private System.Windows.Forms.CheckBox wed;
        private System.Windows.Forms.CheckBox thu;
        private System.Windows.Forms.CheckBox fri;
        private System.Windows.Forms.Panel timeline;
        private System.Windows.Forms.Label morningDot;
        private System.Windows.Forms.Label morningTitle;
        private System.Windows.Forms.Label morningTime;
        private System.Windows.Forms.Label eveningDot;
        private System.Windows.Forms.Label eveningTitle;
        private System.Windows.Forms.Label eveningTime;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel statusBar;
        private System.Windows.Forms.Label statusTitle;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.Label statusTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Panel(); this.brand = new System.Windows.Forms.Label(); this.pageTitle = new System.Windows.Forms.Label(); this.themeHint = new System.Windows.Forms.Label(); this.tealSwatch = new System.Windows.Forms.Panel(); this.blueSwatch = new System.Windows.Forms.Panel(); this.amberSwatch = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel(); this.accountHeading = new System.Windows.Forms.Label(); this.idLabel = new System.Windows.Forms.Label(); this.idTextBox = new System.Windows.Forms.TextBox(); this.passwordLabel = new System.Windows.Forms.Label(); this.passwordTextBox = new System.Windows.Forms.TextBox(); this.manualHeading = new System.Windows.Forms.Label(); this.checkInButton = new System.Windows.Forms.Button(); this.checkOutButton = new System.Windows.Forms.Button(); this.securityNote = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel(); this.scheduleHeading = new System.Windows.Forms.Label(); this.scheduleCaption = new System.Windows.Forms.Label(); this.dayPanel = new System.Windows.Forms.FlowLayoutPanel(); this.mon = new System.Windows.Forms.CheckBox(); this.tue = new System.Windows.Forms.CheckBox(); this.wed = new System.Windows.Forms.CheckBox(); this.thu = new System.Windows.Forms.CheckBox(); this.fri = new System.Windows.Forms.CheckBox(); this.timeline = new System.Windows.Forms.Panel(); this.morningDot = new System.Windows.Forms.Label(); this.morningTitle = new System.Windows.Forms.Label(); this.morningTime = new System.Windows.Forms.Label(); this.eveningDot = new System.Windows.Forms.Label(); this.eveningTitle = new System.Windows.Forms.Label(); this.eveningTime = new System.Windows.Forms.Label(); this.startButton = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.Panel(); this.statusTitle = new System.Windows.Forms.Label(); this.statusText = new System.Windows.Forms.Label(); this.statusTime = new System.Windows.Forms.Label();
            this.header.SuspendLayout(); this.leftPanel.SuspendLayout(); this.contentPanel.SuspendLayout(); this.dayPanel.SuspendLayout(); this.timeline.SuspendLayout(); this.statusBar.SuspendLayout(); this.SuspendLayout();
            // header
            this.header.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.header.Controls.Add(this.brand); this.header.Controls.Add(this.pageTitle); this.header.Controls.Add(this.themeHint); this.header.Controls.Add(this.tealSwatch); this.header.Controls.Add(this.blueSwatch); this.header.Controls.Add(this.amberSwatch); this.header.Dock = System.Windows.Forms.DockStyle.Top; this.header.Size = new System.Drawing.Size(980, 92);
            this.brand.BackColor = System.Drawing.Color.FromArgb(15, 23, 42); this.brand.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.brand.ForeColor = System.Drawing.Color.White; this.brand.Location = new System.Drawing.Point(28, 24); this.brand.Size = new System.Drawing.Size(48, 44); this.brand.Text = "AC"; this.brand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageTitle.AutoSize = true; this.pageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold); this.pageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); this.pageTitle.Location = new System.Drawing.Point(92, 18); this.pageTitle.Text = "Daily attendance";
            this.themeHint.AutoSize = true; this.themeHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.themeHint.Location = new System.Drawing.Point(96, 57); this.themeHint.Text = "One layout · three interchangeable themes";
            this.tealSwatch.BackColor = System.Drawing.Color.FromArgb(38, 166, 154); this.tealSwatch.Location = new System.Drawing.Point(858, 36); this.tealSwatch.Size = new System.Drawing.Size(24, 24);
            this.blueSwatch.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); this.blueSwatch.Location = new System.Drawing.Point(891, 36); this.blueSwatch.Size = new System.Drawing.Size(24, 24);
            this.amberSwatch.BackColor = System.Drawing.Color.FromArgb(245, 158, 11); this.amberSwatch.Location = new System.Drawing.Point(924, 36); this.amberSwatch.Size = new System.Drawing.Size(24, 24);
            // left panel
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(30, 41, 59); this.leftPanel.Controls.Add(this.accountHeading); this.leftPanel.Controls.Add(this.idLabel); this.leftPanel.Controls.Add(this.idTextBox); this.leftPanel.Controls.Add(this.passwordLabel); this.leftPanel.Controls.Add(this.passwordTextBox); this.leftPanel.Controls.Add(this.manualHeading); this.leftPanel.Controls.Add(this.checkInButton); this.leftPanel.Controls.Add(this.checkOutButton); this.leftPanel.Controls.Add(this.securityNote); this.leftPanel.Location = new System.Drawing.Point(28, 116); this.leftPanel.Size = new System.Drawing.Size(300, 468);
            this.accountHeading.AutoSize = true; this.accountHeading.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold); this.accountHeading.ForeColor = System.Drawing.Color.White; this.accountHeading.Location = new System.Drawing.Point(23, 22); this.accountHeading.Text = "Your account";
            this.idLabel.AutoSize = true; this.idLabel.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); this.idLabel.Location = new System.Drawing.Point(24, 70); this.idLabel.Text = "Employee ID";
            this.idTextBox.BackColor = System.Drawing.Color.FromArgb(51, 65, 85); this.idTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.idTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper; this.idTextBox.ForeColor = System.Drawing.Color.White; this.idTextBox.Location = new System.Drawing.Point(27, 96); this.idTextBox.Size = new System.Drawing.Size(246, 32); this.idTextBox.Text = "EMPLOYEE01";
            this.passwordLabel.AutoSize = true; this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); this.passwordLabel.Location = new System.Drawing.Point(24, 145); this.passwordLabel.Text = "Password";
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(51, 65, 85); this.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.passwordTextBox.ForeColor = System.Drawing.Color.White; this.passwordTextBox.Location = new System.Drawing.Point(27, 171); this.passwordTextBox.PasswordChar = '●'; this.passwordTextBox.Size = new System.Drawing.Size(246, 32); this.passwordTextBox.Text = "password";
            this.manualHeading.AutoSize = true; this.manualHeading.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.manualHeading.ForeColor = System.Drawing.Color.White; this.manualHeading.Location = new System.Drawing.Point(23, 238); this.manualHeading.Text = "Manual attendance";
            this.checkInButton.BackColor = System.Drawing.Color.FromArgb(38, 166, 154); this.checkInButton.FlatAppearance.BorderSize = 0; this.checkInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.checkInButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.checkInButton.ForeColor = System.Drawing.Color.White; this.checkInButton.Location = new System.Drawing.Point(27, 278); this.checkInButton.Size = new System.Drawing.Size(246, 48); this.checkInButton.Text = "Check in now"; this.checkInButton.UseVisualStyleBackColor = false;
            this.checkOutButton.BackColor = System.Drawing.Color.FromArgb(51, 65, 85); this.checkOutButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 116, 139); this.checkOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.checkOutButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.checkOutButton.ForeColor = System.Drawing.Color.White; this.checkOutButton.Location = new System.Drawing.Point(27, 339); this.checkOutButton.Size = new System.Drawing.Size(246, 48); this.checkOutButton.Text = "Check out now"; this.checkOutButton.UseVisualStyleBackColor = false;
            this.securityNote.AutoSize = false; this.securityNote.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); this.securityNote.Location = new System.Drawing.Point(27, 410); this.securityNote.Size = new System.Drawing.Size(246, 40); this.securityNote.Text = "Credentials remain in this local session.";
            // content panel
            this.contentPanel.BackColor = System.Drawing.Color.White; this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.contentPanel.Controls.Add(this.scheduleHeading); this.contentPanel.Controls.Add(this.scheduleCaption); this.contentPanel.Controls.Add(this.dayPanel); this.contentPanel.Controls.Add(this.timeline); this.contentPanel.Controls.Add(this.startButton); this.contentPanel.Location = new System.Drawing.Point(350, 116); this.contentPanel.Size = new System.Drawing.Size(602, 358);
            this.scheduleHeading.AutoSize = true; this.scheduleHeading.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold); this.scheduleHeading.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); this.scheduleHeading.Location = new System.Drawing.Point(27, 23); this.scheduleHeading.Text = "Automatic schedule";
            this.scheduleCaption.AutoSize = true; this.scheduleCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.scheduleCaption.Location = new System.Drawing.Point(31, 62); this.scheduleCaption.Text = "Select active weekdays and review randomized windows.";
            // day panel
            this.dayPanel.Controls.Add(this.mon); this.dayPanel.Controls.Add(this.tue); this.dayPanel.Controls.Add(this.wed); this.dayPanel.Controls.Add(this.thu); this.dayPanel.Controls.Add(this.fri); this.dayPanel.Location = new System.Drawing.Point(31, 100); this.dayPanel.Size = new System.Drawing.Size(537, 45); this.dayPanel.WrapContents = false;
            this.mon.Appearance = System.Windows.Forms.Appearance.Button; this.mon.Checked = true; this.mon.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.mon.Size = new System.Drawing.Size(94, 38); this.mon.Text = "MON"; this.mon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tue.Appearance = System.Windows.Forms.Appearance.Button; this.tue.Checked = true; this.tue.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.tue.Size = new System.Drawing.Size(94, 38); this.tue.Text = "TUE"; this.tue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.wed.Appearance = System.Windows.Forms.Appearance.Button; this.wed.Checked = true; this.wed.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.wed.Size = new System.Drawing.Size(94, 38); this.wed.Text = "WED"; this.wed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.thu.Appearance = System.Windows.Forms.Appearance.Button; this.thu.Checked = true; this.thu.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.thu.Size = new System.Drawing.Size(94, 38); this.thu.Text = "THU"; this.thu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fri.Appearance = System.Windows.Forms.Appearance.Button; this.fri.Checked = true; this.fri.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.fri.Size = new System.Drawing.Size(94, 38); this.fri.Text = "FRI"; this.fri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // timeline
            this.timeline.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.timeline.Controls.Add(this.morningDot); this.timeline.Controls.Add(this.morningTitle); this.timeline.Controls.Add(this.morningTime); this.timeline.Controls.Add(this.eveningDot); this.timeline.Controls.Add(this.eveningTitle); this.timeline.Controls.Add(this.eveningTime); this.timeline.Location = new System.Drawing.Point(31, 163); this.timeline.Size = new System.Drawing.Size(537, 88);
            this.morningDot.AutoSize = true; this.morningDot.ForeColor = System.Drawing.Color.FromArgb(38, 166, 154); this.morningDot.Location = new System.Drawing.Point(20, 18); this.morningDot.Text = "●";
            this.morningTitle.AutoSize = true; this.morningTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.morningTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); this.morningTitle.Location = new System.Drawing.Point(48, 13); this.morningTitle.Text = "Morning check-in";
            this.morningTime.AutoSize = true; this.morningTime.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); this.morningTime.Location = new System.Drawing.Point(49, 48); this.morningTime.Text = "Random between 08:50 and 08:58";
            this.eveningDot.AutoSize = true; this.eveningDot.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11); this.eveningDot.Location = new System.Drawing.Point(290, 18); this.eveningDot.Text = "●";
            this.eveningTitle.AutoSize = true; this.eveningTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.eveningTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); this.eveningTitle.Location = new System.Drawing.Point(318, 13); this.eveningTitle.Text = "Evening check-out";
            this.eveningTime.AutoSize = true; this.eveningTime.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); this.eveningTime.Location = new System.Drawing.Point(319, 48); this.eveningTime.Text = "Random between 18:01 and 18:09";
            this.startButton.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); this.startButton.FlatAppearance.BorderSize = 0; this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.startButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.startButton.ForeColor = System.Drawing.Color.White; this.startButton.Location = new System.Drawing.Point(31, 274); this.startButton.Size = new System.Drawing.Size(537, 54); this.startButton.Text = "Start automatic attendance"; this.startButton.UseVisualStyleBackColor = false;
            // status bar
            this.statusBar.BackColor = System.Drawing.Color.White; this.statusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.statusBar.Controls.Add(this.statusTitle); this.statusBar.Controls.Add(this.statusText); this.statusBar.Controls.Add(this.statusTime); this.statusBar.Location = new System.Drawing.Point(350, 494); this.statusBar.Size = new System.Drawing.Size(602, 90);
            this.statusTitle.AutoSize = true; this.statusTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.statusTitle.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74); this.statusTitle.Location = new System.Drawing.Point(25, 17); this.statusTitle.Text = "●  Ready";
            this.statusText.AutoSize = true; this.statusText.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); this.statusText.Location = new System.Drawing.Point(28, 50); this.statusText.Text = "Waiting for the next scheduled attendance action";
            this.statusTime.AutoSize = true; this.statusTime.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.statusTime.Location = new System.Drawing.Point(478, 34); this.statusTime.Text = "MON  08:42";
            // newForm4
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None; this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249); this.ClientSize = new System.Drawing.Size(980, 612); this.Controls.Add(this.statusBar); this.Controls.Add(this.contentPanel); this.Controls.Add(this.leftPanel); this.Controls.Add(this.header); this.Font = new System.Drawing.Font("Segoe UI", 11F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; this.MaximizeBox = false; this.Name = "newForm4"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Auto Check-In — Adaptive Split Layout";
            this.header.ResumeLayout(false); this.header.PerformLayout(); this.leftPanel.ResumeLayout(false); this.leftPanel.PerformLayout(); this.contentPanel.ResumeLayout(false); this.contentPanel.PerformLayout(); this.dayPanel.ResumeLayout(false); this.timeline.ResumeLayout(false); this.timeline.PerformLayout(); this.statusBar.ResumeLayout(false); this.statusBar.PerformLayout(); this.ResumeLayout(false);
        }
    }
}
