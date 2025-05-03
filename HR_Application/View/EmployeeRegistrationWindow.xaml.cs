using HR_Application.Repositories;
using HR_Application.ViewModel;
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

namespace HR_Application.View
{

    public partial class EmployeeRegistrationWindow : Window
    {

        //public EmployeeRegistrationWindow()
        //{
        //    InitializeComponent();
        //    DataContextChanged += OnDataContextChanged;
        //}

        //private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{

        //}

        public EmployeeRegistrationWindow()
        {
            InitializeComponent();

            // Inject concrete implementation of IEmployeeRepository
            DataContext = new EmployeeRegistrationViewModel(new EmployeeRepository());
        }


    }
}