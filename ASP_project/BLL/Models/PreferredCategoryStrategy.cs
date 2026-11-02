using BLL.Interfaces;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PreferredCategoryStrategy : IRecommendationStrategy
    {

        public List<Tab> GetRecommendedTabs(User user, List<Tab> tabs)
        {

            List<Tab> recommendedTabs = tabs.Where(tab => user.likedCategories.Any(cat => cat.ID == tab.Category.ID)).ToList();

            return recommendedTabs;
        }
    }
}
