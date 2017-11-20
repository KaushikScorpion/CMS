using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace DataLayer.Utilities
{
    public class CMSLogger
    {

        public static ILog log;
        public static void SetProperties( string tokenName, string tokenValue, string methodName, string className)
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.ThreadContext.Properties["Token"] = tokenName;
            log4net.ThreadContext.Properties["TokenValue"] = tokenValue;
            log4net.ThreadContext.Properties["MethodName"] = methodName;
            log4net.ThreadContext.Properties["ClassName"] = className;
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        }
    }
}
