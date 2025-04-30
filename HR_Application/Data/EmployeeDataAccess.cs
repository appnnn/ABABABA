//using HR_Application.Model;
//using HR_Application.ViewModel;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HR_Application.Data
//{

//    public static class EmployeeDataAccess
//    {
//        private static string connectionString = "Server=DESKTOP-AANH3TO\\SQLEXPRESS;Database=HR_Application;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

//        public static List<EmployeeDisplayModel> GetEmployeesAsMembers()
//        {
//            try
//            {
//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    string query = "SELECT * FROM RegisterUsers";
//                    using (SqlCommand command = new SqlCommand(query, connection))
//                    {
//                        connection.Open();
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            var employees = new List<Employee>();
//                            while (reader.Read())
//                            {
//                                employees.Add(new Employee
//                                {
//                                    UserId = Convert.ToInt32(reader["UserId"]),
//                                    FirstName = reader["FirstName"].ToString(),
//                                    LastName = reader["LastName"].ToString(),
//                                    Email = reader["Email"].ToString(),
//                                    PhoneNumber = reader["PhoneNumber"].ToString(),
//                                    Department = reader["Department"].ToString(),
//                                    Position = reader["Position"].ToString(),
//                                    Salary = Convert.ToDecimal(reader["Salary"]),
//                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
//                                    JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
//                                    ProfilePicturePath = reader["ProfilePicturePath"].ToString(),
//                                    Username = reader["Username"].ToString(),
//                                    Password = reader["Password"].ToString(),
//                                    Role = reader["Role"].ToString(),
//                                    Gender = reader["Gender"].ToString(),
//                                    Address = reader["Address"].ToString()
//                                });
//                            }

//                            // Map employees to members
//                            return EmployeeMapper.MapEmployeesToMembers(employees);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error retrieving employees from the database.", ex);
//            }
//        }
//    }
//}