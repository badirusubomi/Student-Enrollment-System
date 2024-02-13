using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPT391Project
{
    public partial class ClassSearch : UserControl
    {
        SqlDataAdapter adpt;
        DataTable dt;

        public ClassSearch()
        {
            InitializeComponent();
        }

        private void showClasses()
        {
            DataTable dt = new DataTable();
            adpt = new SqlDataAdapter();
            var sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;

            // Default local host database with name CMPT391Database
            using (SqlConnection conn = new SqlConnection(sqlConn))
            {
                try
                {
                    conn.Open();
                    // Using the check login procedure
                    using (SqlCommand cmd = new SqlCommand("show_classes", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;


                        // Get inputted semester and year
                        cmd.Parameters.AddWithValue("@courseName", textBox1.Text);
                        cmd.Parameters.AddWithValue("@sem", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@year", comboBox2.Text);

                        adpt.Fill(dt);
                        dataGridView1.DataSource = dt;
                        conn.Close();
                    }
                }
                catch (SqlException exception)
                {

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( !( String.IsNullOrEmpty(textBox1.Text) ) || comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
            {
                showClasses();
            }
            else  MessageBox.Show(" You have to enter a Class Name, Semester and Year to search");

        }
    }
}
