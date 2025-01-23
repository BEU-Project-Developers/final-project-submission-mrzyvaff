using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Login : Form
    {
        // Connection string to your database
        private string ConStr = @"Data Source=DESKTOP-58SBNBO\SQLEXPRESS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Login()
        {
            InitializeComponent();
        }

        private void ResetLbl_Click(object sender, EventArgs e)
        {
            UNameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM User_Table WHERE user_name = @username AND user_pass = @password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UNameTb.Text);
                        cmd.Parameters.AddWithValue("@password", PasswordTb.Text);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("Login successful!");
                            Departments departmentForm = new Departments();
                            departmentForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Wrong UserName or Password!!!");
                            UNameTb.Text = "";
                            PasswordTb.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
