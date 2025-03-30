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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ABABABA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            // Optionally validate Step 1 inputs here.
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
            // Example: Reset Step 2 fields.
            DepartmentComboBox.SelectedIndex = 0;
            PositionComboBox.SelectedIndex = 0;
            JoinedDatePicker.Text = String.Empty;
            EmailAddressTextBox.Text = string.Empty;
            PasswordBox.Text = string.Empty;
            ConfirmPasswordBox.Text = string.Empty;

            // Optionally, clear Step 1 fields as well.
 
            MessageBox.Show("Registration submitted!");
        }
    }
}
