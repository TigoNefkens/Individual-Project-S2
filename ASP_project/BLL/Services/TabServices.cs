using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TabServices : ITabService
    {
        private readonly ITabRepo _tabRepo;

        public TabServices(ITabRepo tabRepo)
        {
            _tabRepo = tabRepo;
        }

        public List<Tab> GetTabsFromUser(int userID)
        {
            List<Tab> UserTabs = new List<Tab>();
            foreach (var T in _tabRepo.GetTabsFromUser(userID))
            {
                Category category = new Category(T.Category.ID, T.Category.Name);
                User user = new User(T.User.Id, T.User.userName, T.User.role);
                Tab tab = new Tab(T.TabID, T.TabContent, T.Publicity, category, user, T.TabName, T.viewCount);
                UserTabs.Add(tab);
            }

            return UserTabs;
        }

        public Tab GetTabById(int tabId)
        {
            var result = _tabRepo.GetTabById(tabId);

            Category category = new Category(result.Category.ID, result.Category.Name);

            User user = new User(result.User.Id, result.User.userName, result.User.role);

            Tab tab = new Tab(result.TabID, result.TabContent, result.Publicity, category, user, result.TabName, result.viewCount);

            return tab;
        }

        public List<Tab> GetAllTabs()
        {
            List<Tab> allTabs = new List<Tab>();
            foreach (var T in _tabRepo.GetAllTabs())
            {
                if (T.Publicity)
                {
                    Category category = new Category(T.Category.ID, T.Category.Name);
                    User maker = new User(T.User.Id, T.User.userName, T.User.role);
                    Tab tab = new Tab(T.TabID, T.TabContent, T.Publicity, category, maker, T.TabName, T.viewCount);
                    allTabs.Add(tab);
                }
            }

            return allTabs; 
        }

        public List<Tab> GetTabsBySearch(string searchTerms)
        {
            List<Tab> searchedTabs = new List<Tab>();
            foreach (var T in _tabRepo.GetTabsBySearch(searchTerms))
            {
                if (T.Publicity)
                {
                    Category category = new Category(T.Category.ID, T.Category.Name);
                    User maker = new User(T.User.Id, T.User.userName, T.User.role);
                    Tab tab = new Tab(T.TabID, T.TabContent, T.Publicity, category, maker, T.TabName, T.viewCount);
                    searchedTabs.Add(tab);
                }
            }

            return searchedTabs;
        }

        public ValidationResultOB IncreaseViewCount(int tabId)
        {
            if (_tabRepo.IncreaseViewCount(tabId).isException)
            {
                return new ValidationResultOB(true, "Error with setting viewcount");
            }
            else
            {
                return new ValidationResultOB(false, "Succeeded");
            }       

        }

    }
}
