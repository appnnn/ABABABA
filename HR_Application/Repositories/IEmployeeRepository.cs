using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Repositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        //Employee GetEmployeeById(int id);

        DashboardInfoModel GetDashboardInfo();
    }
}
