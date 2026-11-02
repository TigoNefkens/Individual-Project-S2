using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;

namespace TabberUnitTests.Mock
{
    public class TestStrategy : IRecommendationStrategy
    {
        public List<Tab> GetRecommendedTabs(User user, List<Tab> allTabs)
        {
            return allTabs;
        }
    }
}
