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
       

        public LogIn()
        {
            InitializeComponent();

            passwordLabel.Show();
            usernameLabel.Show();

            password.Show();
            userName.Show();

            flowLayoutPanel1.Hide();
            panel2.Hide();
            cartPage1.Hide();
            profilePage1.Hide();
            classSearch1.Hide();

            LoginLabel.Show();


            
           
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
            cartPage1.Show();
            profilePage1.Show();
            classSearch1.Show();


            flowLayoutPanel1.Show();
            profilePage1.Show();
            profilePage1.BringToFront();


        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
           profilePage1.BringToFront();
        }

        private void cartButton_Click(object sender, EventArgs e)
        {
           
            cartPage1.BringToFront();

        }

        private void classSearchButton_Click(object sender, EventArgs e)
        {
           
            classSearch1.BringToFront();
        }

        private void cartPage1_Load(object sender, EventArgs e)
        {
             
        }
    }
}
