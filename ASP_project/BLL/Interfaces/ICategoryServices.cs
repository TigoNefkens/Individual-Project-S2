using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryServices
    {
        List<Category> GetCategories();

        ValidationResultOB SetUserCategories(int userID, int categoryID);

        List<Category> GetUserCategories(int userID);
    }
}
