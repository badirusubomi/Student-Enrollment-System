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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CMPT391Project
{
    public partial class CartPage : UserControl
    {

        int cID = 1;
        int secID = 1;
        string sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;
        SqlDataAdapter adpt;
        DataTable dt;
       



        private void showClasses()
        {
            string sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;

            //var sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;
           
            
            // Default local host database with name CMPT391Database
            using (SqlConnection conn = new SqlConnection(sqlConn))
            {
                try
                {
                    conn.Open();
                    // Using the check login procedure
                    using (SqlCommand cmd = new SqlCommand("show_cart", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;


                        // Get inputted semester and year
                        cmd.Parameters.AddWithValue("@userName", Program.globalString);

                        adpt = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adpt.Fill(dt);
                        dataGridView1.DataSource = dt;
                        conn.Close();
                    }
                }
                catch (SqlException exception)
                {
                    //System.Diagnostics.Debug.WriteLine(returnedValue.ToString());
                    System.Diagnostics.Debug.WriteLine(exception.Message);

                }
            }

        }
        public string getUser
        {
            get; set;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int returnedValue = 0;
            using (SqlConnection conn = new SqlConnection(sqlConn))
            {
                try
                {
                    conn.Open();
                    // Using the check login procedure
                    using (SqlCommand cmd = new SqlCommand("enroll_class", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;


                        // Get inputted semester and year
                        cmd.Parameters.AddWithValue("@studentID", Program.globalString);

                        cmd.Parameters.AddWithValue("@courseID", cID);
                        cmd.Parameters.AddWithValue("@sectionID", secID);
                        cmd.Parameters.AddWithValue("@semester", "FALL");
                        cmd.Parameters.AddWithValue("@year", "2024");



                        SqlParameter returnedParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnedParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();

                        returnedValue = (int)cmd.Parameters["@ReturnValue"].Value;

                    }
                }
                catch (SqlException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                catch (IndexOutOfRangeException exception2)
                {
                    MessageBox.Show(exception2.Message);
                }
            }


            if (returnedValue < 0)
            {
                MessageBox.Show("Error - " + returnedValue);
            }
            else
            {
                MessageBox.Show("Successfully Enrolled");
            }


        }
    }


   
 
}
