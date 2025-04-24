using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Role { get; set; } // "Admin", "HRManager", "Employee"
        public string Password { get; set; } // For storing hashed passwords
        public string FirstName { get; set; } 
        public string LastName { get; set; }
    }
}
