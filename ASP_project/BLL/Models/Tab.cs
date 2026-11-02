using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Tab
    {
        public int TabID { get; private set; }

        public string TabContent { get; private set; }

        public bool Publicity { get; private set; }

        public Category Category { get; private set; }

        public User user { get; private set; }

        public string TabName { get; private set; }

        public int viewCount { get; private set; }

        public Tab(int tabId, string tabContent, bool publicity, Category category, User User, string tabName, int viewcount)
        {
            TabID = tabId;
            TabContent = tabContent;
            Publicity = publicity;
            Category = category;
            TabName = tabName;
            user = User;
            viewCount = viewcount;
        }
    }
}
