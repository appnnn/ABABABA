using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LeaveRequestWindow.xaml
    /// </summary>
    public partial class LeaveRequestWindow : Window
    {
        private SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\ApproveRequests.mdf;Integrated Security=True;Connect Timeout=30");

        private int employeeId = 17; // Replace with actual logged-in employee ID
        private string gender;
        private string fullName;
        private Dictionary<string, int> remainingLeaves = new Dictionary<string, int>();
        public LeaveRequestWindow()
        {
            InitializeComponent();
            LoadEmployeeLeaveInfo();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadEmployeeLeaveInfo()
        {
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();

                // Get gender and name
                SqlCommand cmdEmp = new SqlCommand("SELECT firstname, lastname, gender FROM employees WHERE id = @id", conn);
                cmdEmp.Parameters.AddWithValue("@id", employeeId);
                SqlDataReader readerEmp = cmdEmp.ExecuteReader();
                if (readerEmp.Read())
                {
                    gender = readerEmp["gender"].ToString().ToLower();
                    fullName = readerEmp["firstname"] + " " + readerEmp["lastname"];
                }
                readerEmp.Close();

                // Get remaining leaves
                SqlCommand cmdLeave = new SqlCommand("SELECT * FROM leaves WHERE employee_id = @id", conn);
                cmdLeave.Parameters.AddWithValue("@id", employeeId);
                SqlDataReader readerLeave = cmdLeave.ExecuteReader();
                if (readerLeave.Read())
                {
                    LeavesList.Items.Clear();
                    remainingLeaves.Clear();

                    if (gender == "male")
                    {
                        AddLeave("Paternity Leave", Convert.ToInt32(readerLeave["paternity_leaves"]));
                    }
                    else
                    {
                        AddLeave("Maternity Leave", Convert.ToInt32(readerLeave["maternity_leaves"]));
                    }
                    AddLeave("Sick Leave", Convert.ToInt32(readerLeave["sick_leaves"]));
                    AddLeave("Annual Leave", Convert.ToInt32(readerLeave["annual_leaves"]));
                    AddLeave("Bereavement Leave", Convert.ToInt32(readerLeave["bereavement_leaves"]));
                    AddLeave("Short Leave", Convert.ToInt32(readerLeave["short_leaves"]));
                }
                readerLeave.Close();

                LeaveTypeComboBox.ItemsSource = remainingLeaves.Keys.ToList();
            }
        }


        private void AddLeave(string name, int count)
        {
            LeavesList.Items.Add($"{name}: {count}");
            remainingLeaves[name] = count;
        }

        private void SubmitLeaveRequest_Click(object sender, RoutedEventArgs e)
        {
            // Correct way to get the selected leave type
            string selectedType = LeaveTypeComboBox.SelectedItem as string;
            var selectedDates = LeaveCalendar.SelectedDates;

            if (selectedType == null || selectedDates.Count == 0)
            {
                MessageBox.Show("Please select a leave type and dates.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int requestedCount = selectedDates.Count;
            if (!remainingLeaves.ContainsKey(selectedType) || requestedCount > remainingLeaves[selectedType])
            {
                MessageBox.Show("Requested leaves exceed available balance.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                connection.Open();
                SqlCommand insertCmd = new SqlCommand(
                    "INSERT INTO leavereqs (employee_id, name, leavetype, numberofleaves, datesofleaves) " +
                    "VALUES (@empid, @name, @type, @count, @dates)", connection);

                insertCmd.Parameters.AddWithValue("@empid", employeeId);
                insertCmd.Parameters.AddWithValue("@name", fullName);
                insertCmd.Parameters.AddWithValue("@type", selectedType);
                insertCmd.Parameters.AddWithValue("@count", requestedCount);

                // Save dates in ISO format, separated by commas
                string formattedDates = string.Join(", ", selectedDates.Select(d => d.ToString("yyyy-MM-dd")));
                insertCmd.Parameters.AddWithValue("@dates", formattedDates);

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Leave request submitted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadEmployeeLeaveInfo(); // Refresh list
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
