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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HR_Application.View.HR_Role.UserControls
{
    /// <summary>
    /// Interaction logic for DepartmentManagement.xaml
    /// </summary>
    public partial class DepartmentManagement : UserControl
    {
        public DepartmentManagement()
        {
            InitializeComponent();
            DataContext = new DepartmentManagementViewModel(new DepartmentRepository());
        }
       
    }
}
