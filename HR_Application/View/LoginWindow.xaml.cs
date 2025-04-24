using HR_Application.Model;
using HR_Application.Services;
using HR_Application.Utils;
using HR_Application.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HR_Application.View
{
    public partial class LoginWindow : Window
    {
 
        private readonly AuthService _authService;

        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Authenticate the user
            var authenticatedUser = _authService.Authenticate(username, password);

            if (authenticatedUser != null)
            {
                // Start a session with the authenticated user
                SessionManager.Login(authenticatedUser);
                
                // Open the corresponding dashboard based on the user's role
                Window? dashboardWindow = authenticatedUser.Role switch
                {
                    "Admin" => new AdminDashboardWindow(),
                    "HRManager" => new HRDashboardWindow(),
                    "Employee" => new EmployeeDashboardWindow(),
                    _ => null
                };

                if (dashboardWindow != null)
                {
                    dashboardWindow.DataContext = new DashboardViewModel(authenticatedUser); // ✅ passing user to ViewModel
                    dashboardWindow.Show();
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Unknown user role.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        // Close button handler
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Shut down application
        }

        // Make window draggable from canvas
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.CaptureMouse();
                this.DragMove();
                this.ReleaseMouseCapture();
            }
        }

        public void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the text fields
            UsernameTextBox.Clear();
            PasswordBox.Clear();
            // Optionally, reset any other UI elements or states
        }

        // Username Placeholder Logic
        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernamePlaceholder2.Visibility = Visibility.Hidden; // hide on focus
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                UsernamePlaceholder2.Visibility = Visibility.Visible; // show if empty
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                UsernamePlaceholder2.Visibility = Visibility.Hidden; // hide when typing
            else if (!UsernameTextBox.IsFocused)
                UsernamePlaceholder2.Visibility = Visibility.Visible;
        }


        // Password Placeholder Logic
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernamePlaceholder1.Visibility = Visibility.Hidden; // hide on focus
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
                UsernamePlaceholder1.Visibility = Visibility.Visible; // show if empty
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PasswordBox.Password))
                UsernamePlaceholder1.Visibility = Visibility.Hidden; // hide when typing
            else if (!PasswordBox.IsFocused)
                UsernamePlaceholder1.Visibility = Visibility.Visible;
        }


    }
}
