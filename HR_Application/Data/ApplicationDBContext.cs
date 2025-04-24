using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace HR_Application.Data
{
    public static class DbConnectionHelper
    {
        private const string ConnectionString = "Server=DESKTOP-AANH3TO\\SQLEXPRESS;Database=HR_Application;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}