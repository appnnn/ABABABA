using HR_Application.Data;
using HR_Application.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        //private readonly List<Department> _departments = new List<Department>();

        //public void AddDepartment(Department department)
        //{
        //    department.DepartmentID = _departments.Count + 1; // Simulate auto-increment
        //    _departments.Add(department);
        //}

        //public void UpdateDepartment(Department department)
        //{
        //    var existing = _departments.FirstOrDefault(d => d.DepartmentId == department.DepartmentId);
        //    if (existing != null)
        //    {
        //        existing.Name = department.Name;
        //        existing.Description = department.Description;
        //    }
        //}

        //public void DeleteDepartment(int departmentId)
        //{
        //    var department = _departments.FirstOrDefault(d => d.DepartmentId == departmentId);
        //    if (department != null)
        //    {
        //        _departments.Remove(department);
        //    }
        //}

        public List<DepartmentModel> GetAllDepartments()
        {
            var departments = new List<DepartmentModel>();

            using var connection = DbConnectionHelper.GetConnection();
            connection.Open();

            var query = "SELECT * FROM Department";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                departments.Add(new DepartmentModel
                {
                    DepartmentID = reader["DepartmentID"].ToString(),
                    DepartmentName = reader["DepartmentName"].ToString()
                });
            }

            return departments;
        }

        //public void AssignEmployeeToDepartment(int departmentId, Employee employee)
        //{
        //    var department = _departments.FirstOrDefault(d => d.DepartmentId == departmentId);
        //    if (department != null)
        //    {
        //        department.Employees.Add(employee);
        //    }
        //}
    }

}
