using HR_Application.Data;
using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HR_Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByUsername(string username)
        {
            User user = null;

            // Query to fetch user by username
            //string query = "SELECT Username, Role, Password , FirstName, LastName FROM RegisterUsers  WHERE Username = @Username";
            string query = "SELECT username, position , password , firstname, lastname FROM employees  WHERE username = @username";

            using (var connection = DbConnectionHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Username = reader["username"].ToString(),
                                Role = reader["position"].ToString(),
                                Password = reader["password"].ToString(),
                                FirstName = reader["firstname"].ToString(),
                                LastName = reader["lastname"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }
    }
}
