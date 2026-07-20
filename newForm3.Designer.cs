namespace AutoCheckIn
{
    partial class newForm3
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Label brand;
        private System.Windows.Forms.Label connectionBadge;
        private System.Windows.Forms.GroupBox loginGroup;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.GroupBox manualGroup;
        private System.Windows.Forms.Button inButton;
        private System.Windows.Forms.Button outButton;
        private System.Windows.Forms.GroupBox automationGroup;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.CheckBox mon;
        private System.Windows.Forms.CheckBox tue;
        private System.Windows.Forms.CheckBox wed;
        private System.Windows.Forms.CheckBox thu;
        private System.Windows.Forms.CheckBox fri;
        private System.Windows.Forms.Label randomWindowLabel;
        private System.Windows.Forms.Label inTime;
        private System.Windows.Forms.Label outTime;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel logPanel;
        private System.Windows.Forms.Label logTitle;
        private System.Windows.Forms.Label logText;
        private System.Windows.Forms.Label footer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.topBar = new System.Windows.Forms.Panel(); this.brand = new System.Windows.Forms.Label(); this.connectionBadge = new System.Windows.Forms.Label();
            this.loginGroup = new System.Windows.Forms.GroupBox(); this.idLabel = new System.Windows.Forms.Label(); this.idTextBox = new System.Windows.Forms.TextBox(); this.passwordLabel = new System.Windows.Forms.Label(); this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.manualGroup = new System.Windows.Forms.GroupBox(); this.inButton = new System.Windows.Forms.Button(); this.outButton = new System.Windows.Forms.Button();
            this.automationGroup = new System.Windows.Forms.GroupBox(); this.dayLabel = new System.Windows.Forms.Label(); this.mon = new System.Windows.Forms.CheckBox(); this.tue = new System.Windows.Forms.CheckBox(); this.wed = new System.Windows.Forms.CheckBox(); this.thu = new System.Windows.Forms.CheckBox(); this.fri = new System.Windows.Forms.CheckBox(); this.randomWindowLabel = new System.Windows.Forms.Label(); this.inTime = new System.Windows.Forms.Label(); this.outTime = new System.Windows.Forms.Label(); this.startButton = new System.Windows.Forms.Button();
            this.logPanel = new System.Windows.Forms.Panel(); this.logTitle = new System.Windows.Forms.Label(); this.logText = new System.Windows.Forms.Label(); this.footer = new System.Windows.Forms.Label();
            this.topBar.SuspendLayout(); this.loginGroup.SuspendLayout(); this.manualGroup.SuspendLayout(); this.automationGroup.SuspendLayout(); this.logPanel.SuspendLayout(); this.SuspendLayout();
            // top bar
            this.topBar.BackColor = System.Drawing.Color.FromArgb(25, 25, 28); this.topBar.Controls.Add(this.brand); this.topBar.Controls.Add(this.connectionBadge); this.topBar.Dock = System.Windows.Forms.DockStyle.Top; this.topBar.Size = new System.Drawing.Size(860, 72);
            this.brand.AutoSize = true; this.brand.Font = new System.Drawing.Font("Segoe UI Semibold", 18F); this.brand.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11); this.brand.Location = new System.Drawing.Point(28, 17); this.brand.Text = "UOF  /  AUTO CHECK";
            this.connectionBadge.BackColor = System.Drawing.Color.FromArgb(45, 45, 49); this.connectionBadge.Font = new System.Drawing.Font("Consolas", 9F); this.connectionBadge.ForeColor = System.Drawing.Color.FromArgb(134, 239, 172); this.connectionBadge.Location = new System.Drawing.Point(661, 20); this.connectionBadge.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0); this.connectionBadge.Size = new System.Drawing.Size(167, 31); this.connectionBadge.Text = "●  SYSTEM READY"; this.connectionBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // login group
            this.loginGroup.Controls.Add(this.idLabel); this.loginGroup.Controls.Add(this.idTextBox); this.loginGroup.Controls.Add(this.passwordLabel); this.loginGroup.Controls.Add(this.passwordTextBox); this.loginGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.loginGroup.ForeColor = System.Drawing.Color.FromArgb(212, 212, 216); this.loginGroup.Location = new System.Drawing.Point(30, 100); this.loginGroup.Size = new System.Drawing.Size(490, 175); this.loginGroup.Text = "  CREDENTIALS  ";
            this.idLabel.AutoSize = true; this.idLabel.Font = new System.Drawing.Font("Segoe UI", 9F); this.idLabel.ForeColor = System.Drawing.Color.FromArgb(161, 161, 170); this.idLabel.Location = new System.Drawing.Point(22, 39); this.idLabel.Text = "EMPLOYEE ID";
            this.idTextBox.BackColor = System.Drawing.Color.FromArgb(24, 24, 27); this.idTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.idTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper; this.idTextBox.Font = new System.Drawing.Font("Consolas", 11F); this.idTextBox.ForeColor = System.Drawing.Color.FromArgb(250, 250, 250); this.idTextBox.Location = new System.Drawing.Point(25, 63); this.idTextBox.Size = new System.Drawing.Size(438, 29); this.idTextBox.Text = "EMPLOYEE01";
            this.passwordLabel.AutoSize = true; this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 9F); this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(161, 161, 170); this.passwordLabel.Location = new System.Drawing.Point(22, 104); this.passwordLabel.Text = "PASSWORD";
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(24, 24, 27); this.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.passwordTextBox.Font = new System.Drawing.Font("Consolas", 11F); this.passwordTextBox.ForeColor = System.Drawing.Color.FromArgb(250, 250, 250); this.passwordTextBox.Location = new System.Drawing.Point(25, 128); this.passwordTextBox.PasswordChar = '●'; this.passwordTextBox.Size = new System.Drawing.Size(438, 29); this.passwordTextBox.Text = "password";
            // manual group
            this.manualGroup.Controls.Add(this.inButton); this.manualGroup.Controls.Add(this.outButton); this.manualGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.manualGroup.ForeColor = System.Drawing.Color.FromArgb(212, 212, 216); this.manualGroup.Location = new System.Drawing.Point(540, 100); this.manualGroup.Size = new System.Drawing.Size(290, 175); this.manualGroup.Text = "  MANUAL CONTROL  ";
            this.inButton.BackColor = System.Drawing.Color.FromArgb(245, 158, 11); this.inButton.FlatAppearance.BorderSize = 0; this.inButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.inButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F); this.inButton.ForeColor = System.Drawing.Color.FromArgb(24, 24, 27); this.inButton.Location = new System.Drawing.Point(24, 40); this.inButton.Size = new System.Drawing.Size(240, 46); this.inButton.Text = "CHECK IN  →"; this.inButton.UseVisualStyleBackColor = false;
            this.outButton.BackColor = System.Drawing.Color.FromArgb(45, 45, 49); this.outButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(82, 82, 91); this.outButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.outButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F); this.outButton.ForeColor = System.Drawing.Color.FromArgb(228, 228, 231); this.outButton.Location = new System.Drawing.Point(24, 104); this.outButton.Size = new System.Drawing.Size(240, 46); this.outButton.Text = "CHECK OUT  →"; this.outButton.UseVisualStyleBackColor = false;
            // automation
            this.automationGroup.Controls.Add(this.dayLabel); this.automationGroup.Controls.Add(this.mon); this.automationGroup.Controls.Add(this.tue); this.automationGroup.Controls.Add(this.wed); this.automationGroup.Controls.Add(this.thu); this.automationGroup.Controls.Add(this.fri); this.automationGroup.Controls.Add(this.randomWindowLabel); this.automationGroup.Controls.Add(this.inTime); this.automationGroup.Controls.Add(this.outTime); this.automationGroup.Controls.Add(this.startButton); this.automationGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 10F); this.automationGroup.ForeColor = System.Drawing.Color.FromArgb(212, 212, 216); this.automationGroup.Location = new System.Drawing.Point(30, 295); this.automationGroup.Size = new System.Drawing.Size(800, 190); this.automationGroup.Text = "  AUTOMATION  ";
            this.dayLabel.AutoSize = true; this.dayLabel.Font = new System.Drawing.Font("Segoe UI", 9F); this.dayLabel.ForeColor = System.Drawing.Color.FromArgb(161, 161, 170); this.dayLabel.Location = new System.Drawing.Point(22, 36); this.dayLabel.Text = "ACTIVE DAYS";
            this.mon.Appearance = System.Windows.Forms.Appearance.Button; this.mon.Location = new System.Drawing.Point(25, 61); this.mon.Text = "MON"; this.tue.Appearance = System.Windows.Forms.Appearance.Button; this.tue.Location = new System.Drawing.Point(82, 61); this.tue.Text = "TUE"; this.wed.Appearance = System.Windows.Forms.Appearance.Button; this.wed.Location = new System.Drawing.Point(139, 61); this.wed.Text = "WED"; this.thu.Appearance = System.Windows.Forms.Appearance.Button; this.thu.Location = new System.Drawing.Point(196, 61); this.thu.Text = "THU"; this.fri.Appearance = System.Windows.Forms.Appearance.Button; this.fri.Location = new System.Drawing.Point(253, 61); this.fri.Text = "FRI";
            this.mon.BackColor = System.Drawing.Color.FromArgb(63, 63, 70); this.mon.Checked = true; this.mon.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11); this.mon.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.mon.Font = new System.Drawing.Font("Consolas", 9F); this.mon.ForeColor = System.Drawing.Color.White; this.mon.Size = new System.Drawing.Size(48, 34); this.mon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.mon.UseVisualStyleBackColor = false;
            this.tue.BackColor = System.Drawing.Color.FromArgb(63, 63, 70); this.tue.Checked = true; this.tue.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11); this.tue.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.tue.Font = new System.Drawing.Font("Consolas", 9F); this.tue.ForeColor = System.Drawing.Color.White; this.tue.Size = new System.Drawing.Size(48, 34); this.tue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.tue.UseVisualStyleBackColor = false;
            this.wed.BackColor = System.Drawing.Color.FromArgb(63, 63, 70); this.wed.Checked = true; this.wed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11); this.wed.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.wed.Font = new System.Drawing.Font("Consolas", 9F); this.wed.ForeColor = System.Drawing.Color.White; this.wed.Size = new System.Drawing.Size(48, 34); this.wed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.wed.UseVisualStyleBackColor = false;
            this.thu.BackColor = System.Drawing.Color.FromArgb(63, 63, 70); this.thu.Checked = true; this.thu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11); this.thu.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.thu.Font = new System.Drawing.Font("Consolas", 9F); this.thu.ForeColor = System.Drawing.Color.White; this.thu.Size = new System.Drawing.Size(48, 34); this.thu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.thu.UseVisualStyleBackColor = false;
            this.fri.BackColor = System.Drawing.Color.FromArgb(63, 63, 70); this.fri.Checked = true; this.fri.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11); this.fri.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.fri.Font = new System.Drawing.Font("Consolas", 9F); this.fri.ForeColor = System.Drawing.Color.White; this.fri.Size = new System.Drawing.Size(48, 34); this.fri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.fri.UseVisualStyleBackColor = false;
            this.randomWindowLabel.AutoSize = true; this.randomWindowLabel.Font = new System.Drawing.Font("Segoe UI", 9F); this.randomWindowLabel.ForeColor = System.Drawing.Color.FromArgb(161, 161, 170); this.randomWindowLabel.Location = new System.Drawing.Point(22, 111); this.randomWindowLabel.Text = "RANDOMIZED WINDOWS";
            this.inTime.AutoSize = true; this.inTime.Font = new System.Drawing.Font("Consolas", 10F); this.inTime.ForeColor = System.Drawing.Color.FromArgb(253, 186, 116); this.inTime.Location = new System.Drawing.Point(23, 139); this.inTime.Text = "IN   08:50 → 08:58";
            this.outTime.AutoSize = true; this.outTime.Font = new System.Drawing.Font("Consolas", 10F); this.outTime.ForeColor = System.Drawing.Color.FromArgb(253, 186, 116); this.outTime.Location = new System.Drawing.Point(208, 139); this.outTime.Text = "OUT  18:01 → 18:09";
            this.startButton.BackColor = System.Drawing.Color.FromArgb(245, 158, 11); this.startButton.FlatAppearance.BorderSize = 0; this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.startButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F); this.startButton.ForeColor = System.Drawing.Color.FromArgb(24, 24, 27); this.startButton.Location = new System.Drawing.Point(544, 51); this.startButton.Size = new System.Drawing.Size(226, 91); this.startButton.Text = "▶  START AUTO CHECK"; this.startButton.UseVisualStyleBackColor = false;
            // log panel
            this.logPanel.BackColor = System.Drawing.Color.FromArgb(24, 24, 27); this.logPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.logPanel.Controls.Add(this.logTitle); this.logPanel.Controls.Add(this.logText); this.logPanel.Location = new System.Drawing.Point(30, 505); this.logPanel.Size = new System.Drawing.Size(800, 76);
            this.logTitle.AutoSize = true; this.logTitle.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold); this.logTitle.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11); this.logTitle.Location = new System.Drawing.Point(17, 12); this.logTitle.Text = "STATUS OUTPUT";
            this.logText.AutoSize = true; this.logText.Font = new System.Drawing.Font("Consolas", 9.5F); this.logText.ForeColor = System.Drawing.Color.FromArgb(161, 161, 170); this.logText.Location = new System.Drawing.Point(17, 41); this.logText.Text = "> ready — waiting for operator command";
            this.footer.AutoSize = true; this.footer.Font = new System.Drawing.Font("Consolas", 8.5F); this.footer.ForeColor = System.Drawing.Color.FromArgb(113, 113, 122); this.footer.Location = new System.Drawing.Point(31, 597); this.footer.Text = "LOCAL SESSION  |  HTTP TIMEOUT 30s  |  RETRY POLICY 5m";
            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.FromArgb(39, 39, 42); this.ClientSize = new System.Drawing.Size(860, 625); this.Controls.Add(this.footer); this.Controls.Add(this.logPanel); this.Controls.Add(this.automationGroup); this.Controls.Add(this.manualGroup); this.Controls.Add(this.loginGroup); this.Controls.Add(this.topBar); this.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; this.MaximizeBox = false; this.Name = "newForm3"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Auto Check-In — Amber Console";
            this.topBar.ResumeLayout(false); this.topBar.PerformLayout(); this.loginGroup.ResumeLayout(false); this.loginGroup.PerformLayout(); this.manualGroup.ResumeLayout(false); this.automationGroup.ResumeLayout(false); this.automationGroup.PerformLayout(); this.logPanel.ResumeLayout(false); this.logPanel.PerformLayout(); this.ResumeLayout(false); this.PerformLayout();
        }
    }
}
