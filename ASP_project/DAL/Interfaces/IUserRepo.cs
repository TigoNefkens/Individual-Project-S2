using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        DBValidator RegisterUser(string username, string hashedPassword);
        DBValidator CheckIfUserExists(string userName);
        string GetHashedPassword(string username);
        UserDTO GetUser(string username);
    }
}
