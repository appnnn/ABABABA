using HR_Application.Model;
using HR_Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Windows;


namespace HR_Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string Username, string Password)
        {
            // Fetch the user from the database
            var user = _userRepository.GetUserByUsername(Username);
            //MessageBox.Show( user.Password, "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);

            if (user != null)
            {
                Console.WriteLine($"Retrieved User: {user.Username}, Role: {user.Role}, Password: {user.Password}");

                // Check if the stored password is a valid BCrypt hash
                if (IsBCryptHash(user.Password))
                {
                    // Verify the password using BCrypt
                    if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
                    {
                        return user; // Successful authentication
                    }
                }
                else
                {
                    // Compare the plain-text password directly
                    if (Password.Equals(user.Password, StringComparison.Ordinal))
                    {
                        return user; // Successful authentication
                    }
                }
            }

            return null; // Authentication failed

        }

        /// <summary>
        /// Checks if the given string is a valid BCrypt hash.
        /// </summary>
        private bool IsBCryptHash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // A valid BCrypt hash starts with $2a$, $2b$, or $2y$
            return input.StartsWith("$2a$") || input.StartsWith("$2b$") || input.StartsWith("$2y$");
        }
    }

}
