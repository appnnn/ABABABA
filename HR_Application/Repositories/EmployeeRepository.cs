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
    public class EmployeeRepository : IEmployeeRepository
    {
        public void AddEmployee(Employee employee)
        {
            using var connection = DbConnectionHelper.GetConnection();
            connection.Open();

            var query = @"
                INSERT INTO RegisterUsers (
                    FirstName, Email, PhoneNumber, Department, Position, Salary,
                    DateOfBirth, JoiningDate, Username, Password, Role, Gender, Address, LastName
                ) VALUES (
                    @FirstName, @Email, @PhoneNumber, @Department, @Position, @Salary,
                    @DateOfBirth, @JoiningDate, @Username, @Password, @Role, @Gender, @Address, @LastName
                )";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Department", employee.Department);
            command.Parameters.AddWithValue("@Position", employee.Position);
            command.Parameters.AddWithValue("@Salary", employee.Salary);
            command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
            command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
            command.Parameters.AddWithValue("@Username", employee.Username);
            command.Parameters.AddWithValue("@Password", employee.Password);
            command.Parameters.AddWithValue("@Role", employee.Role);
            command.Parameters.AddWithValue("@Gender", employee.Gender);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@LastName", employee.LastName);

            command.ExecuteNonQuery();
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using var connection = DbConnectionHelper.GetConnection();
            connection.Open();

            var query = "SELECT * FROM employees";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                //employees.Add(new Employee
                //{
                //    UserId = Convert.ToInt32(reader["UserId"]),
                //    FirstName = reader["FirstName"].ToString(),
                //    LastName = reader["LastName"].ToString(),
                //    Email = reader["Email"].ToString(),
                //    PhoneNumber = reader["PhoneNumber"].ToString(),
                //    Department = reader["Department"].ToString(),
                //    Position = reader["Position"].ToString(),
                //    Salary = Convert.ToDecimal(reader["Salary"]),
                //    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                //    JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                //    ProfilePicturePath = reader["ProfilePicturePath"] as string,
                //    Username = reader["Username"].ToString(),
                //    Password = reader["Password"].ToString(),
                //    Role = reader["Role"].ToString(),
                //    Gender = reader["Gender"].ToString(),
                //    Address = reader["Address"].ToString()
                //});
                employees.Add(new Employee
                {
                    UserId = Convert.ToInt32(reader["id"]),
                    FirstName = reader["firstname"].ToString(),
                    LastName = reader["lastname"].ToString(),
                    Email = reader["email"].ToString(),
                    PhoneNumber = reader["phonenumber"].ToString(),
                    Department = reader["department"].ToString(),
                    Position = reader["position"].ToString(),
                    Salary = Convert.ToDecimal(reader["basicsalary"]),
                    DateOfBirth = Convert.ToDateTime(reader["birthday"]),
                    JoiningDate = Convert.ToDateTime(reader["joindate"]),
                    //ProfilePicturePath = reader["ProfilePicturePath"] as string,
                    Username = reader["username"].ToString(),
                    Password = reader["password"].ToString(),
                    Role = reader["position"].ToString(),
                    Gender = reader["gender"].ToString(),
                    Address = reader["address"].ToString()
                });
            }

            return employees;
        }

        //public Employee GetEmployeeById(int id)
        //{
        //    // Similar implementation as above, but filter by Id
        //    throw new NotImplementedException();
        //}
    }
}
