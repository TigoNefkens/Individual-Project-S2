using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITabRepo
    {
        List<TabDTO> GetTabsFromUser(int userID);

        List<TabDTO> GetAllTabs();

        List<TabDTO> GetTabsBySearch(string searchTerm);
        TabDTO GetTabById(int id);
        DBValidator IncreaseViewCount(int tabId);
    }
}
