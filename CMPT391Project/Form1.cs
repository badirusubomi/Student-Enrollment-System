using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPT391Project
{
    public partial class LogIn : Form
    {
        bool profilePageShown;
        bool cartPageShown;
        public LogIn()
        {
            InitializeComponent();
            flowLayoutPanel1.Hide();
            panel2.Hide();
            cartPage1.Hide();
            profilePage1.Hide();
            profilePageShown = false;
            cartPageShown = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            
            passwordLabel.Hide();
            usernameLabel.Hide();

            password.Hide();
            userName.Hide();
            flowLayoutPanel1.Show();
            panel2.Show();
            loginButton.Hide();
            LoginLabel.Hide();

            //logInPanel.Hide();
            
            panel1.SendToBack();

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            if (cartPageShown)
            {
                cartPage1.Hide();
                cartPageShown = false;
            }
            profilePage1.Show();
            profilePageShown = true;
        }

        private void cartButton_Click(object sender, EventArgs e)
        {
            if (profilePageShown)
            {
                profilePage1.Hide();
                profilePageShown = false;
            }
            cartPage1.Show();
            cartPageShown = true;

        }
    }
}
