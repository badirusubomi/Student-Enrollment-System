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
    public partial class ProfilePage : UserControl
    {
        public ProfilePage()
        {
            InitializeComponent();

            var sqlConn = ConfigurationManager.ConnectionStrings["myConnStr"].ConnectionString;

            // Default local host database with name CMPT391Database
            using (SqlConnection connection = new SqlConnection(sqlConn))
            {
                try
                {
                    connection.Open();
                    // Using the check login procedure
                    using (SqlCommand cmd = new SqlCommand("show_user", connection))
                    //using (SqlCommand cmd = new SqlCommand("check_login", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        // Get inputted username/password
                        //cmd.Parameters.AddWithValue("@username", userName.text);

                        // Parameter for retrieving return value
                        SqlParameter returnedParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnedParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;


                        cmd.ExecuteNonQuery();


                        returnedValue = (int)cmd.Parameters["@ReturnValue"].Value;


                    }
                }
                catch (SqlException exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void emailLabel_Click(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
