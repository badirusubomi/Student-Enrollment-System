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


        int cID = 0;
        int secID = 0;
        string sem = " ";
        string yr = " ";
        string uName = " ";

        string sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;
        SqlDataAdapter adpt;
        DataTable dt;
       
        //int returnedValue = 0;

     
        public CartPage()
        {
            InitializeComponent();
            
            

           
        }

        
        private void showClasses()
        {
            string sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;

            //var sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;

            uName = getUser;
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
                        cmd.Parameters.AddWithValue("@userName",uName);

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
            showClasses();
        }
        
        private void enroll_Click(object sender, EventArgs e)
        {
          


        }

        private void enrollButton_Click(object sender, EventArgs e)
        {
            if ( !(String.IsNullOrEmpty(sem) )  && !(String.IsNullOrEmpty(yr) ) )
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
                            cmd.Parameters.AddWithValue("@semester", sem);
                            cmd.Parameters.AddWithValue("@year", yr);



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
            else MessageBox.Show("Please select a class first");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                cID = Int32.Parse(row.Cells["courseID"].Value.ToString());
                secID = Int32.Parse(row.Cells["secID"].Value.ToString());
                //int tsID = Int32.Parse(row.Cells["timeslotID"].Value.ToString());
                sem = row.Cells["sem"].Value.ToString();
                yr = row.Cells["year"].Value.ToString();
                string courseName = row.Cells["courseName"].Value.ToString();
                MessageBox.Show("You have select " + cID + " " + courseName + " in Section "
                    + secID + " for " + sem + " " + yr + "");


            }
        }
    }


   
 
}
