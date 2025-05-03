using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Repositories
{
    public interface IDepartmentRepository
    {
        //void AddDepartment(Department department);
        //void UpdateDepartment(Department department);
        //void DeleteDepartment(int departmentId);
        List<DepartmentModel> GetAllDepartments();
        //void AssignEmployeeToDepartment(int departmentId, Employee employee);
    }

}
