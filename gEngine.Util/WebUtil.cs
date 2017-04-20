using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public class WebUtil
    {
        static WebUtil()
        {
            Single = new WebUtil();
        }

        #region Property

        public static WebUtil Single { get; }

        public double NullValue
        {
            get { return -9999; }
        }

        public Regex RegString
        {
            get { return new Regex(@"^[\u4e00-\u9fa5a-zA-Z]+$"); }
        }

        #endregion

        #region Method

        public bool IsDecimal(string numstr, out double result)
        {
            try
            {
                double dt;
                dt = Convert.ToDouble(numstr);
                result = dt;
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        #endregion
    }
}
