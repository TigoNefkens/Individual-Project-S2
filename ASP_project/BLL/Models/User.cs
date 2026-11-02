using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string userName { get; private set; }

        public List<Category> likedCategories { get; private set; }

        public int role { get; private set; }

        public User(int Id, string userName, int role, List<Category> likedCategories)
        {
            this.Id = Id;
            this.userName = userName;
            this.role = role;
            this.likedCategories = likedCategories;
        }

        public User(int Id, string userName, int role)
        {
            this.Id = Id;
            this.userName = userName;
            this.role = role;
        }

    }
}
