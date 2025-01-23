using System;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Login : Form
    {
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
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else if (UNameTb.Text == "Admin" && PasswordTb.Text == "Password")
            {
                Employees Obj = new Employees();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong UserName or Password!!!");
                UNameTb.Text = "";
                PasswordTb.Text = "";
            }
        }
    }
}
