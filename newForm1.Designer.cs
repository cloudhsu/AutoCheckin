namespace AutoCheckIn
{
    partial class newForm1
    {
        private System.ComponentModel.IContainer components = null;
        protected System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        protected System.Windows.Forms.Panel accountPanel;
        private System.Windows.Forms.Label accountTitle;
        private System.Windows.Forms.Label idLabel;
        protected System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label passwordLabel;
        protected System.Windows.Forms.TextBox passwordTextBox;
        protected System.Windows.Forms.Panel manualPanel;
        private System.Windows.Forms.Label manualTitle;
        protected System.Windows.Forms.Button checkInButton;
        protected System.Windows.Forms.Button checkOutButton;
        protected System.Windows.Forms.Panel schedulePanel;
        private System.Windows.Forms.Label scheduleTitle;
        private System.Windows.Forms.Label workingDaysLabel;
        protected System.Windows.Forms.CheckBox monCheckBox;
        protected System.Windows.Forms.CheckBox tueCheckBox;
        protected System.Windows.Forms.CheckBox wedCheckBox;
        protected System.Windows.Forms.CheckBox thuCheckBox;
        protected System.Windows.Forms.CheckBox friCheckBox;
        protected System.Windows.Forms.Label checkInTimeLabel;
        protected System.Windows.Forms.Label checkOutTimeLabel;
        protected System.Windows.Forms.Button autoCheckButton;
        protected System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label statusDot;
        protected System.Windows.Forms.Label statusLabel;
        protected System.Windows.Forms.Panel startupPanel;
        private System.Windows.Forms.Label startupTitleLabel;
        protected System.Windows.Forms.Label startupStatusLabel;
        protected System.Windows.Forms.Button createStartupButton;
        protected System.Windows.Forms.Button deleteStartupButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.accountPanel = new System.Windows.Forms.Panel();
            this.accountTitle = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.manualPanel = new System.Windows.Forms.Panel();
            this.manualTitle = new System.Windows.Forms.Label();
            this.checkInButton = new System.Windows.Forms.Button();
            this.checkOutButton = new System.Windows.Forms.Button();
            this.schedulePanel = new System.Windows.Forms.Panel();
            this.scheduleTitle = new System.Windows.Forms.Label();
            this.workingDaysLabel = new System.Windows.Forms.Label();
            this.monCheckBox = new System.Windows.Forms.CheckBox();
            this.tueCheckBox = new System.Windows.Forms.CheckBox();
            this.wedCheckBox = new System.Windows.Forms.CheckBox();
            this.thuCheckBox = new System.Windows.Forms.CheckBox();
            this.friCheckBox = new System.Windows.Forms.CheckBox();
            this.checkInTimeLabel = new System.Windows.Forms.Label();
            this.checkOutTimeLabel = new System.Windows.Forms.Label();
            this.autoCheckButton = new System.Windows.Forms.Button();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusDot = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.startupPanel = new System.Windows.Forms.Panel();
            this.startupTitleLabel = new System.Windows.Forms.Label();
            this.startupStatusLabel = new System.Windows.Forms.Label();
            this.createStartupButton = new System.Windows.Forms.Button();
            this.deleteStartupButton = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.accountPanel.SuspendLayout();
            this.manualPanel.SuspendLayout();
            this.schedulePanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.startupPanel.SuspendLayout();
            this.SuspendLayout();
            // headerPanel
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(31, 45, 53);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(720, 92);
            // titleLabel
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(236, 245, 244);
            this.titleLabel.Location = new System.Drawing.Point(30, 17);
            this.titleLabel.Text = "Auto Check-In";
            // subtitleLabel
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(142, 169, 170);
            this.subtitleLabel.Location = new System.Drawing.Point(34, 58);
            this.subtitleLabel.Text = "UOF attendance assistant  •  Secure workstation session";
            // accountPanel
            this.accountPanel.BackColor = System.Drawing.Color.FromArgb(43, 58, 66);
            this.accountPanel.Controls.Add(this.accountTitle);
            this.accountPanel.Controls.Add(this.idLabel);
            this.accountPanel.Controls.Add(this.idTextBox);
            this.accountPanel.Controls.Add(this.passwordLabel);
            this.accountPanel.Controls.Add(this.passwordTextBox);
            this.accountPanel.Location = new System.Drawing.Point(30, 112);
            this.accountPanel.Size = new System.Drawing.Size(321, 182);
            // accountTitle
            this.accountTitle.AutoSize = true;
            this.accountTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.accountTitle.ForeColor = System.Drawing.Color.White;
            this.accountTitle.Location = new System.Drawing.Point(22, 15);
            this.accountTitle.Text = "ACCOUNT";
            // idLabel
            this.idLabel.AutoSize = true;
            this.idLabel.ForeColor = System.Drawing.Color.FromArgb(181, 199, 200);
            this.idLabel.Location = new System.Drawing.Point(20, 53);
            this.idLabel.Text = "Employee ID";
            // idTextBox
            this.idTextBox.BackColor = System.Drawing.Color.FromArgb(31, 45, 53);
            this.idTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.idTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.idTextBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.idTextBox.ForeColor = System.Drawing.Color.White;
            this.idTextBox.Location = new System.Drawing.Point(26, 73);
            this.idTextBox.Size = new System.Drawing.Size(275, 29);
            // passwordLabel
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(181, 199, 200);
            this.passwordLabel.Location = new System.Drawing.Point(20, 111);
            this.passwordLabel.Text = "Password";
            // passwordTextBox
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(31, 45, 53);
            this.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.passwordTextBox.ForeColor = System.Drawing.Color.White;
            this.passwordTextBox.Location = new System.Drawing.Point(26, 132);
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.Size = new System.Drawing.Size(275, 29);
            // manualPanel
            this.manualPanel.BackColor = System.Drawing.Color.FromArgb(43, 58, 66);
            this.manualPanel.Controls.Add(this.manualTitle);
            this.manualPanel.Controls.Add(this.checkInButton);
            this.manualPanel.Controls.Add(this.checkOutButton);
            this.manualPanel.Location = new System.Drawing.Point(369, 112);
            this.manualPanel.Size = new System.Drawing.Size(321, 182);
            // manualTitle
            this.manualTitle.AutoSize = true;
            this.manualTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.manualTitle.ForeColor = System.Drawing.Color.White;
            this.manualTitle.Location = new System.Drawing.Point(22, 15);
            this.manualTitle.Text = "QUICK ACTION";
            // checkInButton
            this.checkInButton.BackColor = System.Drawing.Color.FromArgb(38, 166, 154);
            this.checkInButton.FlatAppearance.BorderSize = 0;
            this.checkInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkInButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.checkInButton.ForeColor = System.Drawing.Color.White;
            this.checkInButton.Location = new System.Drawing.Point(26, 57);
            this.checkInButton.Size = new System.Drawing.Size(275, 43);
            this.checkInButton.Text = "CHECK IN";
            this.checkInButton.UseVisualStyleBackColor = false;
            // checkOutButton
            this.checkOutButton.BackColor = System.Drawing.Color.FromArgb(55, 72, 80);
            this.checkOutButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(78, 104, 109);
            this.checkOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkOutButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.checkOutButton.ForeColor = System.Drawing.Color.FromArgb(218, 232, 232);
            this.checkOutButton.Location = new System.Drawing.Point(26, 112);
            this.checkOutButton.Size = new System.Drawing.Size(275, 43);
            this.checkOutButton.Text = "CHECK OUT";
            this.checkOutButton.UseVisualStyleBackColor = false;
            // schedulePanel
            this.schedulePanel.BackColor = System.Drawing.Color.FromArgb(43, 58, 66);
            this.schedulePanel.Controls.Add(this.scheduleTitle);
            this.schedulePanel.Controls.Add(this.workingDaysLabel);
            this.schedulePanel.Controls.Add(this.monCheckBox);
            this.schedulePanel.Controls.Add(this.tueCheckBox);
            this.schedulePanel.Controls.Add(this.wedCheckBox);
            this.schedulePanel.Controls.Add(this.thuCheckBox);
            this.schedulePanel.Controls.Add(this.friCheckBox);
            this.schedulePanel.Controls.Add(this.checkInTimeLabel);
            this.schedulePanel.Controls.Add(this.checkOutTimeLabel);
            this.schedulePanel.Controls.Add(this.autoCheckButton);
            this.schedulePanel.Location = new System.Drawing.Point(30, 310);
            this.schedulePanel.Size = new System.Drawing.Size(660, 164);
            // scheduleTitle
            this.scheduleTitle.AutoSize = true;
            this.scheduleTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.scheduleTitle.ForeColor = System.Drawing.Color.White;
            this.scheduleTitle.Location = new System.Drawing.Point(22, 15);
            this.scheduleTitle.Text = "AUTOMATION SCHEDULE";
            // workingDaysLabel
            this.workingDaysLabel.AutoSize = true;
            this.workingDaysLabel.ForeColor = System.Drawing.Color.FromArgb(181, 199, 200);
            this.workingDaysLabel.Location = new System.Drawing.Point(20, 50);
            this.workingDaysLabel.Text = "Working days";
            // weekday checkboxes
            this.monCheckBox.Location = new System.Drawing.Point(26, 74); this.monCheckBox.Tag = "1"; this.monCheckBox.Text = "Mon";
            this.tueCheckBox.Location = new System.Drawing.Point(91, 74); this.tueCheckBox.Tag = "2"; this.tueCheckBox.Text = "Tue";
            this.wedCheckBox.Location = new System.Drawing.Point(156, 74); this.wedCheckBox.Tag = "3"; this.wedCheckBox.Text = "Wed";
            this.thuCheckBox.Location = new System.Drawing.Point(226, 74); this.thuCheckBox.Tag = "4"; this.thuCheckBox.Text = "Thu";
            this.friCheckBox.Location = new System.Drawing.Point(291, 74); this.friCheckBox.Tag = "5"; this.friCheckBox.Text = "Fri";
            this.monCheckBox.AutoSize = true; this.monCheckBox.Checked = true; this.monCheckBox.ForeColor = System.Drawing.Color.FromArgb(218, 232, 232);
            this.tueCheckBox.AutoSize = true; this.tueCheckBox.Checked = true; this.tueCheckBox.ForeColor = System.Drawing.Color.FromArgb(218, 232, 232);
            this.wedCheckBox.AutoSize = true; this.wedCheckBox.Checked = true; this.wedCheckBox.ForeColor = System.Drawing.Color.FromArgb(218, 232, 232);
            this.thuCheckBox.AutoSize = true; this.thuCheckBox.Checked = true; this.thuCheckBox.ForeColor = System.Drawing.Color.FromArgb(218, 232, 232);
            this.friCheckBox.AutoSize = true; this.friCheckBox.Checked = true; this.friCheckBox.ForeColor = System.Drawing.Color.FromArgb(218, 232, 232);
            // time labels
            this.checkInTimeLabel.AutoSize = true;
            this.checkInTimeLabel.ForeColor = System.Drawing.Color.FromArgb(112, 205, 195);
            this.checkInTimeLabel.Location = new System.Drawing.Point(20, 116);
            this.checkInTimeLabel.Text = "IN  08:50–08:58";
            this.checkOutTimeLabel.AutoSize = true;
            this.checkOutTimeLabel.ForeColor = System.Drawing.Color.FromArgb(112, 205, 195);
            this.checkOutTimeLabel.Location = new System.Drawing.Point(170, 116);
            this.checkOutTimeLabel.Text = "OUT  18:01–18:09";
            // autoCheckButton
            this.autoCheckButton.BackColor = System.Drawing.Color.FromArgb(38, 166, 154);
            this.autoCheckButton.FlatAppearance.BorderSize = 0;
            this.autoCheckButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autoCheckButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.autoCheckButton.ForeColor = System.Drawing.Color.White;
            this.autoCheckButton.Location = new System.Drawing.Point(438, 58);
            this.autoCheckButton.Size = new System.Drawing.Size(198, 58);
            this.autoCheckButton.Text = "START AUTO CHECK";
            this.autoCheckButton.UseVisualStyleBackColor = false;
            // startupPanel
            this.startupPanel.BackColor = System.Drawing.Color.FromArgb(43, 58, 66);
            this.startupPanel.Controls.Add(this.startupTitleLabel);
            this.startupPanel.Controls.Add(this.startupStatusLabel);
            this.startupPanel.Controls.Add(this.createStartupButton);
            this.startupPanel.Controls.Add(this.deleteStartupButton);
            this.startupPanel.Location = new System.Drawing.Point(30, 490);
            this.startupPanel.Size = new System.Drawing.Size(660, 80);
            this.startupTitleLabel.AutoSize = true;
            this.startupTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.startupTitleLabel.ForeColor = System.Drawing.Color.White;
            this.startupTitleLabel.Location = new System.Drawing.Point(22, 12);
            this.startupTitleLabel.Text = "WINDOWS STARTUP";
            this.startupStatusLabel.AutoEllipsis = true;
            this.startupStatusLabel.AutoSize = false;
            this.startupStatusLabel.ForeColor = System.Drawing.Color.FromArgb(181, 199, 200);
            this.startupStatusLabel.Location = new System.Drawing.Point(20, 44);
            this.startupStatusLabel.Size = new System.Drawing.Size(390, 22);
            this.startupStatusLabel.Text = "Checking Task Scheduler...";
            this.createStartupButton.BackColor = System.Drawing.Color.FromArgb(38, 166, 154);
            this.createStartupButton.FlatAppearance.BorderSize = 0;
            this.createStartupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createStartupButton.ForeColor = System.Drawing.Color.White;
            this.createStartupButton.Location = new System.Drawing.Point(438, 18);
            this.createStartupButton.Size = new System.Drawing.Size(92, 43);
            this.createStartupButton.Text = "ADD";
            this.createStartupButton.UseVisualStyleBackColor = false;
            this.deleteStartupButton.BackColor = System.Drawing.Color.FromArgb(55, 72, 80);
            this.deleteStartupButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(78, 104, 109);
            this.deleteStartupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteStartupButton.ForeColor = System.Drawing.Color.White;
            this.deleteStartupButton.Location = new System.Drawing.Point(544, 18);
            this.deleteStartupButton.Size = new System.Drawing.Size(92, 43);
            this.deleteStartupButton.Text = "REMOVE";
            this.deleteStartupButton.UseVisualStyleBackColor = false;
            // statusPanel
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(31, 45, 53);
            this.statusPanel.Controls.Add(this.statusDot);
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Location = new System.Drawing.Point(30, 586);
            this.statusPanel.Size = new System.Drawing.Size(660, 46);
            this.statusDot.AutoSize = true;
            this.statusDot.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.statusDot.ForeColor = System.Drawing.Color.FromArgb(38, 166, 154);
            this.statusDot.Location = new System.Drawing.Point(17, 10);
            this.statusDot.Text = "●";
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(181, 199, 200);
            this.statusLabel.Location = new System.Drawing.Point(44, 15);
            this.statusLabel.Text = "Ready  •  Waiting for automatic check schedule";
            // newForm1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(35, 48, 56);
            this.ClientSize = new System.Drawing.Size(720, 660);
            this.Controls.Add(this.startupPanel);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.schedulePanel);
            this.Controls.Add(this.manualPanel);
            this.Controls.Add(this.accountPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "newForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Check-In — Graphite Teal";
            this.headerPanel.ResumeLayout(false); this.headerPanel.PerformLayout();
            this.accountPanel.ResumeLayout(false); this.accountPanel.PerformLayout();
            this.manualPanel.ResumeLayout(false); this.manualPanel.PerformLayout();
            this.schedulePanel.ResumeLayout(false); this.schedulePanel.PerformLayout();
            this.statusPanel.ResumeLayout(false); this.statusPanel.PerformLayout();
            this.startupPanel.ResumeLayout(false); this.startupPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
