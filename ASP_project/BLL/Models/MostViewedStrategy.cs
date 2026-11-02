using BLL.Interfaces;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class MostViewedStrategy : IRecommendationStrategy
    {

        public List<Tab> GetRecommendedTabs(User user, List<Tab> tabs)
        {
            List<Tab> recommendedTabs = tabs.OrderByDescending(t => t.viewCount).Take(3).ToList();

            return recommendedTabs;
        }
    }
}
