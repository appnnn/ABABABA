using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Model
{
    public class DepartmentModel
    {
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

}
