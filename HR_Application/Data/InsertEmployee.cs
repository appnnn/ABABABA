using HR_Application.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Data
{
    public static class InsertEmployee
    {
        public static void InsertEmployeeDB(Employee employee)
        {
            try
            {
                string connectionString = "Server=DESKTOP-AANH3TO\\SQLEXPRESS;Database=HR_Application;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
                string query = @"
                    INSERT INTO RegisterUsers (
                        FirstName, Email, PhoneNumber, Department, Position, Salary, 
                        DateOfBirth, JoiningDate, Username, Password, Role, Gender, Address, LastName
                    ) VALUES (
                        @FirstName, @Email, @PhoneNumber, @Department, @Position, @Salary, 
                        @DateOfBirth, @JoiningDate, @Username, @Password, @Role, @Gender, @Address ,@LastName
                    )";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    //command.Parameters.AddWithValue("@ProfilePicturePath", employee.ProfilePicturePath);
                    command.Parameters.AddWithValue("@Username", employee.Username);
                    command.Parameters.AddWithValue("@Password", employee.Password);
                    command.Parameters.AddWithValue("@Role", employee.Role);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                //throw new Exception("Error inserting employee into the database.", ex);
                throw new Exception($"Error inserting employee into the database. Details: {ex.Message}", ex);

            }
        }
    }
}