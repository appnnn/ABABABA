using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Utils
{
    public static class SessionManager
    {
        // Holds the currently logged-in user
        private static User _currentUser;
        private static DateTime _sessionStartTime;

        // Get Current User
        public static User CurrentUser
        {
            get
            {
                // Optional: Check if the session has expired (e.g., 30 minutes)
                if (_currentUser != null && (DateTime.Now - _sessionStartTime).TotalMinutes > 30)
                {
                    Logout(); // Force logout after session timeout
                }
                return _currentUser;
            }
            private set => _currentUser = value;
        }

        // Login method to start a session
        public static void Login(User user)
        {
            _currentUser = user;
            _sessionStartTime = DateTime.Now; // Record when the session started
        }

        // Logout method to end the session
        public static void Logout()
        {

            _currentUser = null;
            _sessionStartTime = DateTime.MinValue; // Reset session time
        }

        // Check if the user is authenticated
        public static bool IsAuthenticated => _currentUser != null;

        // Check if the user has a specific role
        public static bool HasRole(string role) => _currentUser?.Role == role;
    }
}