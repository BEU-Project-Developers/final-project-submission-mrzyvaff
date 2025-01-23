using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Salaries : Form
    {
        Functions Con;
        public Salaries()
        {
            InitializeComponent();
            Con = new Functions();
            ShowSalaries();
            GetEmployees();
        }
        private void GetEmployees()
        {
            string Query = "select * from EmployeeTbl";
            EmpCb.DisplayMember = Con.GetData(Query).Columns["EmpName"].ToString();
            EmpCb.ValueMember = Con.GetData(Query).Columns["Empid"].ToString();
            EmpCb.DataSource = Con.GetData(Query);

        }
        int DSal = 0;
        string Period = "";
        private void GetSalary()
        {
            string Query = "select EmpSal from EmployeeTbl where empName={0}";
            Query = string.Format(Query, EmpCb.SelectedValue.ToString());
            foreach (DataRow dr in Con.GetData(Query).Rows) 
            {
                DSal = Convert.ToInt32(dr["EmpSal"].ToString());
            }
            if (DaysTb.Text == "")
            {
                AmountTb.Text = "Rs" + (d * DSal);
            }
            //else if(Convert.ToInt32(DaysTb.Text)>31)
            //{
            //    Message.Show("Days Can not be Greater that 31");
            //}
            else
            {
                d = Convert.ToInt32(DaysTb.Text);
                AmountTb.Text = "Rs" + (d * DSal);
            }
            //EmpCb.DataSource = Con.GetData(Query);

        }
        private void ShowSalaries()
        {
            try
            {
                string Query = "SELECT * FROM SalaryTbl";
                SalaryList.DataSource = Con.GetData(Query);
            }
            catch ( Exception)
            {
                throw;
 
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void SalaryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int d = 1;
        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (EmpCb.SelectedIndex == -1 || DaysTb.Text == "" || PeriodTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {

                    Period = PeriodTb.Value.Date.Month.ToString() + " ." + PeriodTb.Value.Date.Year.ToString();
                    int Amount = DSal * Convert.ToInt32(DaysTb.Text);
                    int Days=Convert.ToInt32(DaysTb.Text);
                    string Query = "INSERT INTO SalaryTbl VALUES ({0}, {1},' {2}', {3}, '{4}')";
                    Query = string.Format(Query,EmpCb.SelectedValue.ToString(),Days,Period,Amount,DateTime.Today.Date);
                    Con.SetData(Query);
                    ShowSalaries();
                    MessageBox.Show("Salary Paid!!!");
                    DaysTb.Text = "";
                    
                }
            }

            catch(Exception Ex) 
            {
                MessageBox.Show(Ex.Message);
            }


        }

        private void EmpCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetSalary();
        }

        private void AmountTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
        }
    }
}
