using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class ApproveLeaveRequestsWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\LeaveRequests.mdf;Integrated Security=True;Connect Timeout=30");
        ObservableCollection<LeaveRequest> leaveRequests = new ObservableCollection<LeaveRequest>();

        public ApproveLeaveRequestsWindow()
        {
            InitializeComponent();
            LoadLeaveRequests();
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

        private void LoadLeaveRequests()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM leavereqs", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                leaveRequests.Clear();
                while (reader.Read())
                {
                    leaveRequests.Add(new LeaveRequest
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        EmployeeName = reader["employee_name"].ToString(),
                        LeaveType = reader["leave_type"].ToString(),
                        RequestedDays = Convert.ToInt32(reader["requested_days"]),
                        LeaveDates = reader["leave_dates"].ToString()
                    });
                }

                reader.Close();
                LeaveRequestsDataGrid.ItemsSource = leaveRequests;
            }
            finally
            {
                connection.Close();
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (LeaveRequestsDataGrid.SelectedItem is LeaveRequest selectedRequest)
            {
                var result = MessageBox.Show("Are you sure you want to approve this leave request?", "Confirm Approval", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        connection.Open();

                        // Update employee's leave balance here (example logic, can be expanded)
                        SqlCommand updateLeaveCmd = new SqlCommand("UPDATE employees SET annual_leaves = annual_leaves - @days WHERE employee_name = @name", connection);
                        updateLeaveCmd.Parameters.AddWithValue("@days", selectedRequest.RequestedDays);
                        updateLeaveCmd.Parameters.AddWithValue("@name", selectedRequest.EmployeeName);
                        updateLeaveCmd.ExecuteNonQuery();

                        // Delete from leave requests table after approval
                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM leavereqs WHERE id = @id", connection);
                        deleteCmd.Parameters.AddWithValue("@id", selectedRequest.Id);
                        deleteCmd.ExecuteNonQuery();

                        MessageBox.Show("Leave request approved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        leaveRequests.Remove(selectedRequest);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (LeaveRequestsDataGrid.SelectedItem is LeaveRequest selectedRequest)
            {
                var result = MessageBox.Show("Are you sure you want to reject this leave request?", "Confirm Rejection", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM leavereqs WHERE id = @id", connection);
                        deleteCmd.Parameters.AddWithValue("@id", selectedRequest.Id);
                        deleteCmd.ExecuteNonQuery();

                        MessageBox.Show("Leave request rejected.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        leaveRequests.Remove(selectedRequest);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }

    public class LeaveRequest
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
        public int RequestedDays { get; set; }
        public string LeaveDates { get; set; }
    }
}
