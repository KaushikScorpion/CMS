using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSException
{
    public class UnknownFilterException:Exception
    {
        public string Name = "Unknown Filter Exception";
        public UnknownFilterException(string message) :base(message)
        {

        }
    }
}
