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
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\ApproveRequests.mdf;Integrated Security=True;Connect Timeout=30");
        public SignupWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();

            this.Close();
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

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = ((TextBox)RegistrationStepOne.Children[0]).Text.Trim();
            string lastName = ((TextBox)RegistrationStepOne.Children[1]).Text.Trim();
            string address = ((TextBox)RegistrationStepOne.Children[2]).Text.Trim();
            string birthday = ((TextBox)RegistrationStepOne.Children[3]).Text.Trim();
            string gender = ((ComboBox)RegistrationStepOne.Children[4]).Text;
            string phone = ((TextBox)RegistrationStepOne.Children[5]).Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(birthday) ||
                string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please fill in all fields in Step 1.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Date and year validation
            if (!DateTime.TryParse(birthday, out DateTime parsedBirthday))
            {
                MessageBox.Show("Please enter a valid Birthday.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int year = parsedBirthday.Year;
            if (year < 1965 || year > 2009)
            {
                MessageBox.Show("Please enter a valid Birthday.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Phone number format validation
            Regex phoneRegex = new Regex(@"^\d{10}$");
            if (!phoneRegex.IsMatch(phone))
            {
                MessageBox.Show("Please enter a valid 10-digit phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RegistrationStepOne.Visibility = Visibility.Collapsed;
            RegistrationStepTwo.Visibility = Visibility.Visible;
        }




        // Back button click handler: Return to Step One.
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationStepTwo.Visibility = Visibility.Collapsed;
            RegistrationStepOne.Visibility = Visibility.Visible;
        }

        // Submit button click handler: Process the registration.
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string department = DepartmentComboBox.Text;
            string position = PositionComboBox.Text;
            string joinDate = JoinedDatePicker.Text.Trim();
            string email = EmailAddressTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Validate Step 2 fields
            if (string.IsNullOrEmpty(department) || string.IsNullOrEmpty(position) ||
                string.IsNullOrEmpty(joinDate) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields in Step 2.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate joined date format and year
            if (!DateTime.TryParse(joinDate, out DateTime parsedJoinDate))
            {
                MessageBox.Show("Please enter a valid Join Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (parsedJoinDate.Year < 1930 || parsedJoinDate.Year > 2009)
            {
                MessageBox.Show("Join Date year must be between 1930 and 2009.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate email format
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate password strength
            Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,30}$");
            if (!passwordRegex.IsMatch(password))
            {
                MessageBox.Show("Password must be 8-30 characters and include upper/lowercase letters and at least one number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string firstName = ((TextBox)RegistrationStepOne.Children[0]).Text.Trim();
            string lastName = ((TextBox)RegistrationStepOne.Children[1]).Text.Trim();
            string address = ((TextBox)RegistrationStepOne.Children[2]).Text.Trim();
            string birthday = ((TextBox)RegistrationStepOne.Children[3]).Text.Trim();
            string gender = ((ComboBox)RegistrationStepOne.Children[4]).Text;
            string phone = ((TextBox)RegistrationStepOne.Children[5]).Text.Trim();

            // Insert into DB
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO appreq (firstname, lastname, address, birthday, gender, phonenumber, department, position, joindate, email, password) " +
                                                "VALUES (@FirstName, @LastName, @Address, @Birthday, @Gender, @Phone, @Department, @Position, @JoinDate, @Email, @Password)", connection);

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Birthday", DateTime.Parse(birthday));
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@JoinDate", parsedJoinDate);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); // 🔒 Consider hashing in real apps

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Signup request submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // or reset form
                }
                else
                {
                    MessageBox.Show("Something went wrong while submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
