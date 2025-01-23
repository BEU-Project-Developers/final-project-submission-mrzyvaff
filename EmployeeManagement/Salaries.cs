using System;
using System.Data;
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
            try
            {
                string Query = "SELECT Empid, EmpName FROM EmployeeTbl";
                DataTable dt = Con.GetData(Query);
                EmpCb.DataSource = dt;
                EmpCb.DisplayMember = "EmpName";
                EmpCb.ValueMember = "Empid";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }

        int DSal = 0; 
        string Period = ""; 
        int d = 1;

        private void GetSalary()
        {
            try
            {
                if (EmpCb.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid employee.");
                    return;
                }

                
                string Query = "SELECT EmpSal FROM EmployeeTbl WHERE Empid = {0}";
                Query = string.Format(Query, EmpCb.SelectedValue);

                
                DataTable dt = Con.GetData(Query);
                if (dt.Rows.Count > 0)
                {
                    DSal = Convert.ToInt32(dt.Rows[0]["EmpSal"]);
                }
                else
                {
                    MessageBox.Show("Salary information not found for the selected employee.");
                    return;
                }

                
                if (string.IsNullOrEmpty(DaysTb.Text))
                {
                    d = 1; 
                }
                else
                {
                    if (int.TryParse(DaysTb.Text, out int days) && days > 0)
                    {
                        d = days;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number of days.");
                        return;
                    }
                }

              
                AmountTb.Text = "Rs " + (d * DSal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching salary: " + ex.Message);
            }
        }

        private void ShowSalaries()
        {
            try
            {
                
                string Query = "SELECT * FROM SalaryTbl";
                SalaryList.DataSource = Con.GetData(Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading salaries: " + ex.Message);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (EmpCb.SelectedIndex == -1 || string.IsNullOrEmpty(DaysTb.Text) || string.IsNullOrEmpty(PeriodTb.Text))
                {
                    MessageBox.Show("Missing data. Please fill in all fields.");
                    return;
                }

                if (EmpCb.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid employee.");
                    return;
                }

                
                Period = PeriodTb.Value.Date.Month.ToString() + " ." + PeriodTb.Value.Date.Year.ToString();
                int Days = Convert.ToInt32(DaysTb.Text);
                int Amount = DSal * Days;

                
                string Query = "INSERT INTO SalaryTbl (Employee, Attendance, Period, Amount, PayDate) VALUES ({0}, {1}, '{2}', {3}, '{4}')";
                Query = string.Format(Query, EmpCb.SelectedValue, Days, Period, Amount, DateTime.Today.Date);

                
                Con.SetData(Query);

                
                ShowSalaries();
                MessageBox.Show("Salary Paid!");
                DaysTb.Text = "";
                AmountTb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding salary: " + ex.Message);
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

        private void label10_Click(object sender, EventArgs e)
        {
            Employees emp = new Employees();
            emp.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Departments departments = new Departments();
            departments.Show();
        }
    }
}
