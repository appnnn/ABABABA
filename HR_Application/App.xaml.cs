//using HR_Application.Data;
//using HR_Application.Repositories;
//using HR_Application.Services;
//using System.Configuration;
//using System.Data;
//using System.Windows;



//namespace HR_Application;



//public partial class App : Application
//{

//}

using System;
using System.Windows;
using HR_Application.Data;
using HR_Application.Repositories;
using HR_Application.Services;
using HR_Application.View;

namespace HR_Application
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Step 1: Initialize the repository
                var userRepository = new UserRepository();

                // Step 2: Initialize the AuthService
                var authService = new AuthService(userRepository);

                // Step 3: Show the LoginWindow
                var loginWindow = new LoginWindow(authService);

                // Set the LoginWindow as the main application window
                MainWindow = loginWindow;
                loginWindow.Show();
            }
            catch (Exception ex)
            {
                // Log the exception details
                MessageBox.Show($"An error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(); // Shut down the application if an error occurs
            }
        }
    }
}