using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TabDTO
    {
        public int TabID { get;  private set; }

        public CategoryDTO Category { get; private set; }

        public string TabContent { get; private set; }

        public bool Publicity { get; private set; }

        public UserDTO User { get; private set; }

        public string TabName { get; private set; }

        public int viewCount { get; private set; }

        public TabDTO(int tabId, CategoryDTO category, string tabContent, bool publicity, UserDTO user, string tabName, int viewCount)
        {
            TabID = tabId;
            TabContent = tabContent;
            Publicity = publicity;
            TabName = tabName;
            Category = category;
            User = user;
            this.viewCount = viewCount; 
        }

    }
}
