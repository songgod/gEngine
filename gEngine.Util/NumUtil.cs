using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public class NumUtil
    {
        static NumUtil()
        {
            InvalidDouble = -9999;
            InvalidDecimal = -9999;
            DefaultBool = false;
        }
        static public double InvalidDouble { get; set; }
        static public decimal InvalidDecimal { get; set; }
        static public bool DefaultBool { get; set; }
        static public double ToDouble(string numstr, bool usedefault=false, double defaultvalue = -9999)
        {
            try
            {
                double db;
                db = Convert.ToDouble(numstr);
                return db;
            }
            catch
            {
                if(usedefault)
                    return defaultvalue;
                return InvalidDouble;
            }
        }

        static public decimal ToDecimal(string numstr, bool usedefault = false, decimal defaultvalue = -9999)
        {
            try
            {
                decimal dt;
                dt = Convert.ToDecimal(numstr);
                return dt;
            }
            catch
            {
                if(usedefault)
                    return defaultvalue;
                return InvalidDecimal;
            }
        }

        static public bool ToBoolean(string numstr, bool usedefault = false, bool defaultvalue = false)
        {
            try
            {
                bool v;
                v = Convert.ToBoolean(numstr);
                return v;
            }
            catch
            {
                if(usedefault)
                    return defaultvalue;
                return DefaultBool;
            }
        }
    }
}
