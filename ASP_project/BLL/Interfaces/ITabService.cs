using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITabService
    {
        List<Tab> GetTabsFromUser(int userId);
        List<Tab> GetTabsBySearch(string searchTerms);
        public ValidationResultOB IncreaseViewCount(int tabId);
        public Tab GetTabById(int tabId);
        List<Tab> GetAllTabs();
    }
}
