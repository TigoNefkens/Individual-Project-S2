using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DBValidator
    {
        public bool isException { get; private set; }

        public string exception { get; private set; }

        public DBValidator(bool argIsException, string argException)
        {
            isException = argIsException;
            exception = argException;
        }
    }
}
