using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPT391Project
{
    public partial class ClassSearch : UserControl
    {
        SqlDataAdapter adpt;
        DataTable dt;
        int cID = 0; //course id
        int secID = 0; //section id
        int tsID = 0;//timeslot id

        string uName = " ";//student id/username
        string sem = " ";//semester
        string yr = " ";//year

        string sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;


        public ClassSearch()
        {
            InitializeComponent();
        }

        public string getUser
        {
            get; set;

        }
        private void showClasses()
        {

            //var sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;

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
                        adpt = new SqlDataAdapter(cmd);
                        dt = new DataTable();
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
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                cID = Int32.Parse(row.Cells["courseID"].Value.ToString());
                secID = Int32.Parse(row.Cells["secID"].Value.ToString());
                tsID = Int32.Parse(row.Cells["timeslotID"].Value.ToString());
                sem = row.Cells["sem"].Value.ToString();
                yr = row.Cells["year"].Value.ToString();
                uName = getUser;
                string courseName = row.Cells["courseName"].Value.ToString();
                MessageBox.Show("You have select " + cID + " " + courseName + " in Section "
                    + secID + " for " + sem + " " + yr + "");


            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textBox1.Text)) & comboBox1.SelectedIndex != -1 & comboBox2.SelectedIndex != -1)
            {
                showClasses();
            }
            else MessageBox.Show(" You have to enter a Class Name, Semester and Year to search");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID is " + cID + " and the Sec is " + secID);
            int returnedValue = 1;

            using (SqlConnection conn = new SqlConnection(sqlConn))
            {
                try
                {
                    conn.Open();
                    // Using the check login procedure
                    using (SqlCommand cmd = new SqlCommand("add_to_cart", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@studentID", Int32.Parse(uName));
                        cmd.Parameters.AddWithValue("@courseID", cID);
                        cmd.Parameters.AddWithValue("@sectionID", secID);
                        cmd.Parameters.AddWithValue("@semester", sem);
                        cmd.Parameters.AddWithValue("@year", yr);
                        cmd.Parameters.AddWithValue("@timeslotID", tsID);

                        SqlParameter returnedParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnedParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;


                        cmd.ExecuteNonQuery();


                        returnedValue = (int)cmd.Parameters["@ReturnValue"].Value;
                        System.Console.WriteLine(returnedValue);
                        if (returnedValue == -1) MessageBox.Show("Unable to Enroll in class. Please check cart for time conflicts or ensure Pre Requisite requirments are met");
                        else if (returnedValue >= 0) MessageBox.Show("Successfully enrolled in class !");
                    }
                }

                catch (SqlException exception)
                {
                    System.Diagnostics.Debug.WriteLine(returnedValue.ToString());
                    System.Diagnostics.Debug.WriteLine(exception.Message);
                }
            }
            
        }
    }
    
}
