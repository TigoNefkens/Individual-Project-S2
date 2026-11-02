using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICategoryRepo
    {
        public List<CategoryDTO> GetCategoryDTOs();

        public DBValidator SetUserCategories(int userID, int categoryID);

        public List<int> GetUserCategories(int userID);
    }
}
