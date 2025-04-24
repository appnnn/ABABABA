using HR_Application.Utils;
using HR_Application.View.HR_Role.UserControls;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static HR_Application.View.HR_Role.UserControls.EmployeeManagement;

namespace HR_Application.View
{
    /// <summary>
    /// Interaction logic for HRDashboardWindow.xaml
    /// </summary>
    public partial class HRDashboardWindow : Window
    {
        public HRDashboardWindow()
        {
            InitializeComponent();
            LoadHome(); // Default page
        }

        private void LoadHome()
        {
            MainContentArea.Content = new Home();
        }

        private void LoadEmployeeManagement()
        {
            MainContentArea.Content = new EmployeeManagement();
        }

        private void LoadAttendanceAndLeave()
        {
            MainContentArea.Content = new AttendanceAndLeave();
        }

        private void LoadPayrollManagement()
        {
            MainContentArea.Content = new PayrollManagement();
        }

        private void LoadPerformanceAndReports()
        {
            MainContentArea.Content = new PerformanceAndReports();
        }
        private void LoadNotifications()
        {
            MainContentArea.Content = new Notifications();
        }

        private bool IsMaximize = false;
        
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeOrRestoreWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }


        // Event handlers for menu buttons
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Logout();

            var loginWindow = new LoginWindow(App.AuthServiceInstance);

            loginWindow.Show(); 

            this.Close();
        }
        
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            LoadHome();
        }

        private void EmployeeManagementButton_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployeeManagement();
        }

        private void AttendanceAndLeaveButton_Click(object sender, RoutedEventArgs e)
        {
            LoadAttendanceAndLeave();
        }

        private void PayrollManagementButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPayrollManagement();
        }

        private void PerformanceAndReportsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPerformanceAndReports();
        }
        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadNotifications();
        }
    }




}
