using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Interfaces;
using DAL.Data;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class RecommendationServices : IRecommendationServices
    {
        private readonly IUserRepo _userRepo;

        private readonly ITabRepo _tabRepo;

        public RecommendationServices(IUserRepo userRepo, ITabRepo tabRepo)
        {
            _userRepo = userRepo;
            _tabRepo = tabRepo;
        }

        public List<Tab> GetRecommendedTabs(IRecommendationStrategy recommendedStrategy, string username)
        {
            List<Tab> allTabs = new List<Tab>(); 

            foreach (var T in _tabRepo.GetAllTabs()) // from DTO to tab object
            {
                if (T.Publicity)
                {
                    Category category = new Category(T.Category.ID, T.Category.Name);
                    User maker = new User(T.User.Id, T.User.userName, T.User.role);
                    Tab tab = new Tab(T.TabID, T.TabContent, T.Publicity, category, maker, T.TabName, T.viewCount);
                    allTabs.Add(tab);
                }
            }

            if (string.IsNullOrWhiteSpace(username)) // If isn't logged in
            {
                return recommendedStrategy.GetRecommendedTabs(null, allTabs);
            }

            var userDTO = _userRepo.GetUser(username); // Get user for categories

            List<Category> categories = new List<Category>();

            foreach (var C in userDTO.likedCategories)
            {
                Category category = new Category(C.ID, C.Name);
                categories.Add(category);
            }

            User user = new User(userDTO.Id, userDTO.userName, userDTO.role, categories);  // from userDTO to user object

            List<Tab> recommendedTabs = recommendedStrategy.GetRecommendedTabs(user, allTabs);

            return recommendedTabs;
        }
    }
}
