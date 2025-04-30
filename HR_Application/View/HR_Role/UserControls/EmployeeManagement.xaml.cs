using HR_Application.Data;
using HR_Application.View;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HR_Application.Model;
using HR_Application.Repositories;
using HR_Application.ViewModel;

namespace HR_Application.View.HR_Role.UserControls
{
    //public partial class EmployeeManagement : UserControl
    //{

    //    public EmployeeManagement()
    //    {
    //        InitializeComponent();
    //        LoadMembersData();
    //    }

    //    private void LoadMembersData()
    //    {
    //        try
    //        {
    //            // Fetch employees from the database and map to members
    //            var members = EmployeeDataAccess.GetEmployeesAsMembers();


    //            // Convert the list to an ObservableCollection for binding
    //            var observableMembers = new ObservableCollection<EmployeeDisplayModel>(members);

    //            // Assign the collection to the DataGrid's ItemsSource
    //            membersDataGrid.ItemsSource = observableMembers;
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Error loading employees: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    //        }
    //    }

    //    private void addButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        var registrationWindow = new EmployeeRegistrationWindow();
    //        registrationWindow.ShowDialog();
    //    }
    //}


    public partial class EmployeeManagement : UserControl
    {
        public EmployeeManagement()
        {
            InitializeComponent();

            // Inject repository manually (until full DI is set up)
            var repo = new EmployeeRepository(); // Or resolve from DI container later
            DataContext = new EmployeeManagementViewModel(repo);
        }
    }
}