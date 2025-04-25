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
        //private const string ConnectionString = "Server=DESKTOP-AANH3TO\\SQLEXPRESS;Database=HR_Application;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        private const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\lessons\\2nd year 2nd sem\\visual programming\\group_project\\asdf.mdf\";Integrated Security=True;Connect Timeout=30";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}