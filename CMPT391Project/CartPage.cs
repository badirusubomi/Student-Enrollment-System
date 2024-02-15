using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPT391Project
{
    public partial class CartPage : UserControl
    {
        public CartPage()
        {
            InitializeComponent();
        }
    }

    private void showCart()
    {

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
                    cmd.Parameters.AddWithValue("@studentId", Program.globalString);
                    
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
}
