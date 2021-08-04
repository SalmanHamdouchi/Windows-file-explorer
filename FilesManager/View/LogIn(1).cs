using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using static FilesManager.View.GUI;
using FilesManager.View;

namespace FilesMnage.View
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            Time.Parent = LockScreen;

            this.username.Left = (this.LockScreen.Width - username.Width) / 2;
            this.username.Top = ((this.LockScreen.Height) / 2) + 102;

            this.password.Left = (this.LockScreen.Width - password.Width) / 2;
            this.password.Top = ((this.LockScreen.Height) / 2) + 151;

            this.userPicture.Left = (this.LockScreen.Width - userPicture.Width) / 2;
            this.userPicture.Top = ((this.LockScreen.Height) / 2) - 85;

            this.logInBtn.Left = (this.LockScreen.Width - logInBtn.Width) / 2;
            this.logInBtn.Top = ((this.LockScreen.Height) / 2) + 201;

        }

        private void FileManager_Load(object sender, EventArgs e)
        {
            LockScreen.Size = this.Size;
            LockScreen.Location = this.Location;
            LockScreen.BackgroundImage = Image.FromFile(@"F:\Study\S3\C#\FilesManagers\resources\lockscreen.jpg");
        }

        private void Time_Click(object sender, EventArgs e)
        {

        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString("HH:mm");
            Date.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

        }

        private void date_Click(object sender, EventArgs e)
        {

        }

        private void Date_DoubleClick(object sender, EventArgs e)
        {

        }


        private void Timer_Tick(object sender, EventArgs e)
        {

        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            /* Time.Location = new Point(Time.Location.X, Time.Location.Y - 10);

             if (Time.Location.Y < -100)
             {
                 animationTimer.Stop();
             }*/
        }

        private void FileManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Time.Visible = false;
                Date.Visible = false;
                displayOverlay();
                displayLoginForm();
            }
        }
        private void displayLoginForm()
        {
            userPicture.Visible = true;
            username.Visible = true;
            password.Visible = true;
            logInBtn.Visible = true;
        }
        private void displayOverlay()
        {
            OverlayBG.BackColor = Color.FromArgb(100, Color.Black);

            OverlayBG.Size = this.Size;
            OverlayBG.Visible = true;

        }
        private void LockScreen_SizeChanged(object sender, EventArgs e)
        {

        }

        private void username1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void startExplorer(String username, String password)
        {
           // setUsers();
            if (!startSession(username, password))
                return;
            Explorer ex = new Explorer();
            ex.Closed += (s, args) => this.Close();
            clockTimer.Enabled = false;
            ex.Show();

        }
        private void logIn_Click(object sender, EventArgs e)
        {
            String username = this.username.Text;
            String password = this.password.Text;

            startExplorer(username, password);

        }
    }
}
