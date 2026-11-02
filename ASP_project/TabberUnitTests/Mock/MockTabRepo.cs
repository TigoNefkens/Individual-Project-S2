using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabberUnitTests.Mock
{
    public class MockTabRepo : ITabRepo
    {
        public List<TabDTO> GetAllTabs()
        {
            List<TabDTO> tabs = new List<TabDTO>();

            var category1 = new CategoryDTO(4, "Rock");
            var category2 = new CategoryDTO(5, "Metal");
            var category3 = new CategoryDTO(1, "Acoustic");

            var user = new UserDTO(1, "Tigo", 1, new List<CategoryDTO> { category1 });

            tabs.Add(new TabDTO(1, category1, "01297192", true, user, "Tab1", 10));
            tabs.Add(new TabDTO(1, category2, "01297192", true, user, "Tab2", 10));
            tabs.Add(new TabDTO(1, category3, "01297192", true, user, "Tab3", 10));

            return tabs;
        }

        public TabDTO GetTabById(int id)
        {
            var category1 = new CategoryDTO(1, "Rock");

            var user = new UserDTO(1, "Tigo", 1, new List<CategoryDTO> { category1 });

            TabDTO tab = new TabDTO(1, category1,"99229128", true, user, "Tab1", 10);

            return tab;
        }

        public List<TabDTO> GetTabsBySearch(string searchterms)
        {
            return new List<TabDTO>();
        }

        public List<TabDTO> GetTabsFromUser(int id)
        {
            return new List<TabDTO>();
        }

        public DBValidator IncreaseViewCount(int id)
        {
            return new DBValidator(false, "Succeeded");
        }
    }
}
