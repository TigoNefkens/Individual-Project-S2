using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CategoryDTO
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public CategoryDTO(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

}
