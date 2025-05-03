using HR_Application.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HR_Application.Services
{
    public static class EmployeeMapper
    {
        private static readonly Random _random = new Random();

        public static List<EmployeeDisplayModel> MapEmployeesToEmployeeDisplayModel(List<Employee> employees)
        {
            return employees.Select((employee, index) => new EmployeeDisplayModel
            {
                Character = employee.FirstName.Length > 0 ? employee.FirstName[0].ToString() : "?", // Default to "?" if FirstName is empty
                BgColor = GetRandomBrush(),
                Name = $"{employee.FirstName} {employee.LastName}",
                Department = employee.Department,
                Position = employee.Position,
                Role = employee.Role,
                Gender = employee.Gender,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                EmployeeID = employee.EmployeeID,
                JoinDate = DateOnly.FromDateTime(employee.JoiningDate),
                RowNumber = index + 1

            }).ToList();
        }

        private static Brush GetRandomBrush()
        {
            // Generate a random color
            var randomColor = Color.FromRgb((byte)_random.Next(256), (byte)_random.Next(256), (byte)_random.Next(256));
            return new SolidColorBrush(randomColor);
        }
    }
}
