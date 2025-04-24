using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.ViewModel
{
    class DashboardViewModel
    {
        public User CurrentUser { get; private set; }

        public DashboardViewModel(User user)
        {
            // Assign the logged-in user to a property
            CurrentUser = user;

            // You can now use CurrentUser.Role, CurrentUser.Username, etc.
        }
    }

}
