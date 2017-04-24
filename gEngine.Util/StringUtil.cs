using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public class StringUtil
    {
        static StringUtil()
        {
            InvalidString = "InvalidString";
        }

        static public string InvalidString { get; set; }

        static public string ValidString(string str, string defaultstring= "InvalidString")
        {
            if (string.IsNullOrEmpty(str))
            {
                if (string.IsNullOrEmpty(defaultstring))
                    return InvalidString;
                else
                    return defaultstring;
            }
            return str;
        }
    }
}
