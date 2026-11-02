using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BLL.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        ValidationResultOB TryRegisterUser(string username, string password);
        ValidationResultOB TryLoginUser(string username, string password);
        User GetUser(string userName);
    }
}
