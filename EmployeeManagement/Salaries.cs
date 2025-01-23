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
                // Retrieve employee data for dropdown
                string Query = "SELECT Empid, EmpName FROM EmployeeTbl";
                DataTable dt = Con.GetData(Query);
                EmpCb.DataSource = dt;
                EmpCb.DisplayMember = "EmpName"; // Display employee name in dropdown
                EmpCb.ValueMember = "Empid"; // Use employee ID as the selected value
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }

        int DSal = 0; // Daily salary of the selected employee
        string Period = ""; // Salary payment period
        int d = 1; // Number of days worked

        private void GetSalary()
        {
            try
            {
                if (EmpCb.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid employee.");
                    return;
                }

                // Query to get employee salary based on Empid
                string Query = "SELECT EmpSal FROM EmployeeTbl WHERE Empid = {0}";
                Query = string.Format(Query, EmpCb.SelectedValue);

                // Fetch salary data
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

                // Calculate salary based on days worked
                if (string.IsNullOrEmpty(DaysTb.Text))
                {
                    d = 1; // Default to 1 day if no input
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

                // Display the calculated amount
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
                // Query to show all salaries
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
                // Validate input fields
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

                // Calculate the payment period and salary amount
                Period = PeriodTb.Value.Date.Month.ToString() + " ." + PeriodTb.Value.Date.Year.ToString();
                int Days = Convert.ToInt32(DaysTb.Text);
                int Amount = DSal * Days;

                // Query to insert salary record
                string Query = "INSERT INTO SalaryTbl (Empid, Attendance, Period, Amount, PayDate) VALUES ({0}, {1}, '{2}', {3}, '{4}')";
                Query = string.Format(Query, EmpCb.SelectedValue, Days, Period, Amount, DateTime.Today.Date);

                // Execute the query using the existing SetData method
                Con.SetData(Query);

                // Refresh salary list and clear input fields
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
            // Optional: Handle any logic for AmountTb text change, if required
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
