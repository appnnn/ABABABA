using HR_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string Username);
    }
}
