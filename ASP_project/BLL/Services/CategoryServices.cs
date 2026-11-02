using BLL.Interfaces;
using BLL.Models;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryServices(ICategoryRepo CategoryRepo)
        {
            _categoryRepo = CategoryRepo;
        }

        public List<Category> GetCategories()
        {
            List<Category> categoryList = new List<Category>();

            foreach (var C in _categoryRepo.GetCategoryDTOs())
            {
                Category category = new Category(C.ID, C.Name);
                categoryList.Add(category);
            }

            return categoryList;
        }

        public ValidationResultOB SetUserCategories(int userID, int categoryID)
        {
            var result = _categoryRepo.SetUserCategories(userID, categoryID);
            if (result.isException)
            {
                ValidationResultOB resultOB = new ValidationResultOB(true, result.exception);
                return resultOB;
            }
            else
            {
                ValidationResultOB resultOB = new ValidationResultOB(false, result.exception);
                return resultOB;
            }
            
        }

        public List<Category> GetUserCategories(int userId)
        {

            List<int> likedCategoryIds = _categoryRepo.GetUserCategories(userId);

            List<Category> allCategories = GetCategories();

            List<Category> userCategories = allCategories
                .Where(cat => likedCategoryIds.Contains(cat.ID))
                .ToList();

            return userCategories;
        }

    }
}
