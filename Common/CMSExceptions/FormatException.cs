using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSException
{
    public class FormatException:Exception
    {
        public string Name = "FormatException";
       public FormatException(string Message):base(Message){

    }
    }
}
