using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRecommendationStrategy
    {
        List<Tab> GetRecommendedTabs(User user, List<Tab> tabs);
    }
}
