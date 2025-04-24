using HR_Application.Utils;
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
    /// <summary>
    /// Interaction logic for AdminDashboardWindow.xaml
    /// </summary>
    public partial class AdminDashboardWindow : Window
    {
        public AdminDashboardWindow()
        {
            InitializeComponent();
            // Check if the current user has the Admin role
            if (!SessionManager.HasRole("Admin"))
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
        }
    }
}
