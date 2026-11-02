using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ValidationResultOB
    {
        public bool isException { get; private set; }

        public string exception { get; private set; }

        public ValidationResultOB(bool argIsException, string argException)
        {
            isException = argIsException;
            exception = argException;
        }
    }
}
