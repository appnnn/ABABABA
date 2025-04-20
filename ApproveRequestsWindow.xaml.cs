using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class ApproveRequestsWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\ApproveRequests.mdf;Integrated Security=True;Connect Timeout=30");
        ObservableCollection<AppRequest> requests = new ObservableCollection<AppRequest>();

        public ApproveRequestsWindow()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void btnClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void LoadRequests()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM appreq", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                requests.Clear();
                while (reader.Read())
                {
                    requests.Add(new AppRequest
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FirstName = reader["firstname"].ToString(),
                        LastName = reader["lastname"].ToString(),
                        Address = reader["address"].ToString(),
                        Birthday = Convert.ToDateTime(reader["birthday"]),
                        Gender = reader["gender"].ToString(),
                        PhoneNumber = reader["phonenumber"].ToString(),
                        Department = reader["department"].ToString(),
                        Position = reader["position"].ToString(),
                        JoinDate = Convert.ToDateTime(reader["joindate"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString()
                    });
                }

                reader.Close();
                RequestsDataGrid.ItemsSource = requests;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading requests: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is not AppRequest selectedRequest) return;

            var result = MessageBox.Show("Approve this request?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                connection.Open();

                decimal basicSalary = selectedRequest.Position.ToLower() switch
                {
                    "manager" => 50000,
                    "developer" => 40000,
                    "designer" => 35000,
                    "hr" => 30000,
                    _ => 25000
                };

                SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO employees (firstname, lastname, address, birthday, gender, phonenumber, department, position, joindate, email, password, basicsalary) 
                    VALUES (@fn, @ln, @addr, @bd, @gender, @phone, @dept, @pos, @join, @email, @pwd, @salary);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection);

                insertCmd.Parameters.AddWithValue("@fn", selectedRequest.FirstName);
                insertCmd.Parameters.AddWithValue("@ln", selectedRequest.LastName);
                insertCmd.Parameters.AddWithValue("@addr", selectedRequest.Address);
                insertCmd.Parameters.AddWithValue("@bd", selectedRequest.Birthday);
                insertCmd.Parameters.AddWithValue("@gender", selectedRequest.Gender);
                insertCmd.Parameters.AddWithValue("@phone", selectedRequest.PhoneNumber);
                insertCmd.Parameters.AddWithValue("@dept", selectedRequest.Department);
                insertCmd.Parameters.AddWithValue("@pos", selectedRequest.Position);
                insertCmd.Parameters.AddWithValue("@join", selectedRequest.JoinDate);
                insertCmd.Parameters.AddWithValue("@email", selectedRequest.Email);
                insertCmd.Parameters.AddWithValue("@pwd", selectedRequest.Password);
                insertCmd.Parameters.AddWithValue("@salary", basicSalary);

                int newEmployeeId = Convert.ToInt32(insertCmd.ExecuteScalar());

                // Leave entitlements
                int matLeave = selectedRequest.Gender.ToLower() == "female" ? 90 : 0;
                int patLeave = selectedRequest.Gender.ToLower() == "male" ? 8 : 0;

                SqlCommand leaveCmd = new SqlCommand(@"
                    INSERT INTO leaves (employee_id, sick_leaves, annual_leaves, paternity_leaves, maternity_leaves, bereavement_leaves, short_leaves)
                    VALUES (@empId, @sick, @annual, @pat, @mat, @bereavement, @short)", connection);

                leaveCmd.Parameters.AddWithValue("@empId", newEmployeeId);
                leaveCmd.Parameters.AddWithValue("@sick", 10);
                leaveCmd.Parameters.AddWithValue("@annual", 14);
                leaveCmd.Parameters.AddWithValue("@pat", patLeave);
                leaveCmd.Parameters.AddWithValue("@mat", matLeave);
                leaveCmd.Parameters.AddWithValue("@bereavement", 5);
                leaveCmd.Parameters.AddWithValue("@short", 3);
                leaveCmd.ExecuteNonQuery();

                // Delete request
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM appreq WHERE id = @id", connection);
                deleteCmd.Parameters.AddWithValue("@id", selectedRequest.Id);
                deleteCmd.ExecuteNonQuery();

                MessageBox.Show("Request approved and employee added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                requests.Remove(selectedRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during approval: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is not AppRequest selectedRequest) return;

            var result = MessageBox.Show("Reject this request?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                connection.Open();

                SqlCommand deleteCmd = new SqlCommand("DELETE FROM appreq WHERE id = @id", connection);
                deleteCmd.Parameters.AddWithValue("@id", selectedRequest.Id);
                deleteCmd.ExecuteNonQuery();

                requests.Remove(selectedRequest);
                MessageBox.Show("Request rejected.", "Rejected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during rejection: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public class AppRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
