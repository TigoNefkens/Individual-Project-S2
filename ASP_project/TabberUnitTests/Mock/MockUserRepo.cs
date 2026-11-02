using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabberUnitTests.Mock
{
    internal class MockUserRepo : IUserRepo
    {
        private List<UserDTO> _users = new List<UserDTO>();

        public MockUserRepo() 
        {
            CategoryDTO categoryDTO1 = new CategoryDTO(5, "Metal");
            CategoryDTO categoryDTO2 = new CategoryDTO(4, "Rock");

            List<CategoryDTO> categories = new List<CategoryDTO>();

            categories.Add(categoryDTO1);
            categories.Add(categoryDTO2);

            UserDTO userDTO = new UserDTO(1, "Tigo", 1, categories);

            _users.Add(userDTO);
        }

        public DBValidator RegisterUser(string username, string hashedPassword)
        {
            if (_users.Any(u => u.userName == username))
            {
                return new DBValidator (true, "This username is already taken" );
            }

            return new DBValidator(false, "Succeeded");
        }

        public DBValidator CheckIfUserExists(string username)
        {
            bool exists = _users.Any(u => u.userName == username);
            return new DBValidator (true, "Username exists" );
        }

        public string GetHashedPassword(string username)
        {
            return "";
        }

        public UserDTO GetUser(string username)
        {
            return _users[0];
        }
    }
}
