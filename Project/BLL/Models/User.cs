using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    internal class User
    {
        public string id {  get; private set; }
        public string userName { get; private set; }
        public string preferences { get; private set; }
    }
}
