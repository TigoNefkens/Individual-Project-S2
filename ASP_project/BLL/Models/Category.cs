using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Category
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public Category(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
