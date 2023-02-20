using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{

    public class NullValueException : Exception
    {
        public NullValueException(string ex = "input is empty") { cusmessage = ex; }

        public string cusmessage;
        public override string Message =>
                        cusmessage;

    }
}
