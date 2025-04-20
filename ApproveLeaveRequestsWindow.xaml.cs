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
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\ApproveRequests.mdf;Integrated Security=True;Connect Timeout=30");
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
                        EmployeeId = Convert.ToInt32(reader["employee_id"]),
                        EmployeeName = reader["name"].ToString(),
                        LeaveType = reader["leavetype"].ToString(),
                        RequestedDays = Convert.ToInt32(reader["numberofleaves"]),
                        LeaveDates = reader["datesofleaves"].ToString()
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

                        // Determine which column to update based on LeaveType
                        string leaveColumn = selectedRequest.LeaveType.ToLower() switch
                        {
                            "sick leave" => "sick_leaves",
                            "annual leave" => "annual_leaves",
                            "paternity leave" => "paternity_leaves",
                            "maternity leave" => "maternity_leaves",
                            "bereavement leave" => "bereavement_leaves",
                            "short leave" => "short_leaves",
                            _ => null
                        };

                        if (leaveColumn == null)
                        {
                            MessageBox.Show("Invalid leave type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // 1. Deduct from the specific leave column in leaves table
                        string updateLeaveSql = $@"
                                    UPDATE leaves
                                    SET {leaveColumn} = {leaveColumn} - @days
                                    WHERE employee_id = @empId";

                        SqlCommand updateLeaveCmd = new SqlCommand(updateLeaveSql, connection);
                        updateLeaveCmd.Parameters.AddWithValue("@days", selectedRequest.RequestedDays);
                        updateLeaveCmd.Parameters.AddWithValue("@empId", selectedRequest.EmployeeId);
                        updateLeaveCmd.ExecuteNonQuery();


                        // 3. Remove the request from leavereqs
                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM leavereqs WHERE id = @id", connection);
                        deleteCmd.Parameters.AddWithValue("@id", selectedRequest.Id);
                        deleteCmd.ExecuteNonQuery();

                        MessageBox.Show("Leave request approved and leave balance updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        leaveRequests.Remove(selectedRequest);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while processing request: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
        public int RequestedDays { get; set; }
        public string LeaveDates { get; set; }
    }
}
