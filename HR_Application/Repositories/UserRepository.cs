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
            string query = "SELECT Username, Role, Password , FirstName, LastName FROM RegisterUsers  WHERE Username = @Username";

            using (var connection = DbConnectionHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Username = reader["Username"].ToString(),
                                Role = reader["Role"].ToString(),
                                Password = reader["Password"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }
    }
}
