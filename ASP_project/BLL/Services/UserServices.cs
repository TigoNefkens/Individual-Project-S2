using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BCrypt.Net;
using System.Reflection.Metadata.Ecma335;

namespace BLL.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public ValidationResultOB TryRegisterUser(string userName, string password)
        {
            try
            {
                if (_userRepo.CheckIfUserExists(userName).isException)
                {
                    return new ValidationResultOB(true, "This email-address is already being used");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var registerResult = _userRepo.RegisterUser(userName, hashedPassword);

                if (registerResult.isException)
                {
                    return new ValidationResultOB(true, registerResult.exception);
                }

                return new ValidationResultOB(false, "Registration succesfull");
            }
            catch
            {
                return new ValidationResultOB(true, "Something went wrong, try again later");
            }
        }

        public ValidationResultOB TryLoginUser(string userName, string password)
        {

            try
            {
                var hashedPasswordFromDb = _userRepo.GetHashedPassword(userName);

                if (hashedPasswordFromDb == null)
                {
                    return new ValidationResultOB(true, "No user found under that email-address");
                }

                bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDb);

                if (passwordMatches)
                {
                    return new ValidationResultOB(false, "Login Successful");
                }

                return new ValidationResultOB(true, "Username or password is incorrect");
            }
            catch
            {
                return new ValidationResultOB(true, "Something went wrong, try again later");
            }
        }

        public User GetUser(string userName)
        {
            var result = _userRepo.GetUser(userName);

            if (result != null)
            {
                User user = new User(result.Id, result.userName, result.role);

                return user;
            }

            return null;
        }
    }
}
