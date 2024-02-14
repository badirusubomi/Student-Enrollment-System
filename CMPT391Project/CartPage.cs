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
        SqlDataAdapter adpt;
        DataTable dt;
        string uName = " ";
        //int returnedValue = 0;

     
        public CartPage()
        {
            InitializeComponent();
            uName = getUser;
            showClasses();
            

           
        }

        
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
                        cmd.Parameters.AddWithValue("@userName", 1);

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
    }
}
