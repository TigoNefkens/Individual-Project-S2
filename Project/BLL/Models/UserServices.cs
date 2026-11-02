using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace BLL.Models
{
    public class UserServices : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public bool RegisterUser(string userName, string hashedPassword)
        {
            if (_userRepo.RegisterUser(userName, hashedPassword) == true)
            {
                return true;
            }
            return false;
        }
    }
}
