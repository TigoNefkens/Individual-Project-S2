using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserDTO
    {
        public int Id { get; private set; }
        public string userName { get; private set; }

        public List<CategoryDTO> likedCategories { get; private set; }

        public int role { get; private set; }

        public UserDTO(int Id, string userName, int role)
        {
            this.Id = Id;
            this.userName = userName;
            this.role = role;
            this.likedCategories = likedCategories;
        }

        public UserDTO(int Id, string userName, int role, List<CategoryDTO> likedCategories)
        {
            this.Id = Id;
            this.userName = userName;
            this.role = role;
            this.likedCategories = likedCategories;
        }
    }
}
