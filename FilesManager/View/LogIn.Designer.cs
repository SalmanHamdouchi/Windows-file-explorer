using System;
using System.Drawing;

namespace FilesMnage.View
{
    partial class LogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            this.LockScreen = new System.Windows.Forms.Panel();
            this.password = new FilesManager.WTextBox();
            this.username = new FilesManager.WTextBox();
            this.Date = new System.Windows.Forms.Label();
            this.logInBtn = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.userPicture = new System.Windows.Forms.PictureBox();
            this.OverlayBG = new System.Windows.Forms.Panel();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LockScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // LockScreen
            // 
            this.LockScreen.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LockScreen.Controls.Add(this.password);
            this.LockScreen.Controls.Add(this.username);
            this.LockScreen.Controls.Add(this.Date);
            this.LockScreen.Controls.Add(this.logInBtn);
            this.LockScreen.Controls.Add(this.Time);
            this.LockScreen.Controls.Add(this.userPicture);
            this.LockScreen.Controls.Add(this.OverlayBG);
            this.LockScreen.Location = new System.Drawing.Point(1, 0);
            this.LockScreen.Name = "LockScreen";
            this.LockScreen.Size = new System.Drawing.Size(1202, 592);
            this.LockScreen.TabIndex = 0;
            this.LockScreen.SizeChanged += new System.EventHandler(this.LockScreen_SizeChanged);
            // 
            // password
            // 
            this.password.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.password.ForeColor = System.Drawing.SystemColors.Menu;
            this.password.Location = new System.Drawing.Point(502, 394);
            this.password.Name = "password";
            this.password.Placeholder = "Enter Your Password...";
            this.password.Size = new System.Drawing.Size(250, 34);
            this.password.TabIndex = 2;
            this.password.UseSystemPasswordChar = true;
            this.password.Visible = false;
            // 
            // username
            // 
            this.username.AllowDrop = true;
            this.username.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username.Font = new System.Drawing.Font("Segoe UI", 15.5F);
            this.username.ForeColor = System.Drawing.SystemColors.Menu;
            this.username.Location = new System.Drawing.Point(502, 342);
            this.username.Name = "username";
            this.username.Placeholder = "Enter Your Username...";
            this.username.Size = new System.Drawing.Size(250, 35);
            this.username.TabIndex = 0;
            this.username.Visible = false;
            this.username.TextChanged += new System.EventHandler(this.username1_TextChanged);
            // 
            // Date
            // 
            this.Date.AutoSize = true;
            this.Date.BackColor = System.Drawing.Color.Transparent;
            this.Date.Font = new System.Drawing.Font("Segoe UI Light", 40.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date.ForeColor = System.Drawing.Color.Snow;
            this.Date.Location = new System.Drawing.Point(19, 720);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(650, 72);
            this.Date.TabIndex = 0;
            this.Date.Text = "Tuesday, 19 November 2019";
            this.Date.DoubleClick += new System.EventHandler(this.Date_DoubleClick);
            // 
            // logInBtn
            // 
            this.logInBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.logInBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.logInBtn.CausesValidation = false;
            this.logInBtn.FlatAppearance.BorderSize = 0;
            this.logInBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logInBtn.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.logInBtn.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.logInBtn.Location = new System.Drawing.Point(578, 446);
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new System.Drawing.Size(92, 40);
            this.logInBtn.TabIndex = 3;
            this.logInBtn.Text = "Log In";
            this.logInBtn.UseVisualStyleBackColor = false;
            this.logInBtn.Visible = false;
            this.logInBtn.Click += new System.EventHandler(this.logIn_Click);
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.BackColor = System.Drawing.Color.Transparent;
            this.Time.Font = new System.Drawing.Font("Segoe UI Light", 70.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.ForeColor = System.Drawing.Color.Snow;
            this.Time.Location = new System.Drawing.Point(12, 610);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(252, 125);
            this.Time.TabIndex = 0;
            this.Time.Text = "23:16";
            this.Time.Click += new System.EventHandler(this.Time_Click);
            // 
            // userPicture
            // 
            this.userPicture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.userPicture.BackColor = System.Drawing.Color.Transparent;
            this.userPicture.Image = ((System.Drawing.Image)(resources.GetObject("userPicture.Image")));
            this.userPicture.Location = new System.Drawing.Point(546, 158);
            this.userPicture.Name = "userPicture";
            this.userPicture.Size = new System.Drawing.Size(167, 166);
            this.userPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPicture.TabIndex = 0;
            this.userPicture.TabStop = false;
            this.userPicture.Visible = false;
            this.userPicture.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // OverlayBG
            // 
            this.OverlayBG.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OverlayBG.Location = new System.Drawing.Point(3, 517);
            this.OverlayBG.Name = "OverlayBG";
            this.OverlayBG.Size = new System.Drawing.Size(146, 10);
            this.OverlayBG.TabIndex = 1;
            this.OverlayBG.Visible = false;
            // 
            // clockTimer
            // 
            this.clockTimer.Enabled = true;
            this.clockTimer.Tick += new System.EventHandler(this.clockTimer_Tick);
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 1;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 579);
            this.Controls.Add(this.LockScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogIn";
            this.Text = "Log In";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FileManager_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileManager_KeyDown);
            this.LockScreen.ResumeLayout(false);
            this.LockScreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LockScreen;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.PictureBox userPicture;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel OverlayBG;
        private FilesManager.WTextBox username;
        private FilesManager.WTextBox password;
        private System.Windows.Forms.Button logInBtn;
    }
}