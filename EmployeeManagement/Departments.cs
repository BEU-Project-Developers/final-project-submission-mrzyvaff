using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Departments : Form
    {
        Functions Con;

        public Departments()
        {
            InitializeComponent();
            Con = new Functions();
            ShowDepartments();
        }

        private void ShowDepartments()
        {
            string Query = "SELECT * FROM DepartmentTbl";
            DepList.DataSource = Con.GetData(Query);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTb.Text == "")
                {
                    MessageBox.Show("Missing Data!!!");
                }
                else
                {
                    string Dep = DepNameTb.Text;
                    string Query = $"INSERT INTO DepartmentTbl values('{Dep}')";
                    Con.SetData(Query);
                    ShowDepartments();
                    MessageBox.Show("Department Added!!!");
                    DepNameTb.Text = "";
                    DepNameTb.Clear();
                    DepNameTb.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;
        private void DepList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DepNameTb.Text = DepList.SelectedRows[0].Cells[0].Value.ToString();
            if (DepNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DepList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTb.Text == "")
                {
                    MessageBox.Show("Missing Data!!!");
                }
                else
                {
                    string Dep = DepNameTb.Text;
                    string Query = "Update DepartmentTbl set DepName=$'{Dep}' where DepId={1}";
                    Query = string.Format(Query, DepNameTb.Text, Key);
                    Con.SetData(Query);
                    ShowDepartments();
                    MessageBox.Show("Department Updated!!!");
                    DepNameTb.Text = "";
                    DepNameTb.Clear();
                    DepNameTb.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTb.Text == "")
                {
                    MessageBox.Show("Missing Data!!!");
                }
                else
                {
                    string Dep = DepNameTb.Text;
                    string Query = $"DELETE FROM DepartmentTbl WHERE DepName='{Dep}'";
                    Con.SetData(Query); 
                    ShowDepartments(); 
                    MessageBox.Show("Department Deleted!!!"); 
                    DepNameTb.Text = ""; 
                    DepNameTb.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); 
            }
        }


        private void EmpLbl_Click(object sender, EventArgs e)
        {
            Employees Obj= new Employees();
            Obj.Show();
            //this.Hide(); 
        }

        private void SalaryLbl_Click(object sender, EventArgs e)
        {
            Salaries Obj = new Salaries();
            Obj.Show();

        }

        private void Logout_Click(object sender, EventArgs e)
        {

            Login Obj = new Login();
            Obj.Show();
        }
    }
}
