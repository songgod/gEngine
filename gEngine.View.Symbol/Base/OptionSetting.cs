using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Symbol
{
    public class OptionSetting
    {
        public Dictionary<string, object> Properties;
        public OptionSetting()
        {
            Properties = new Dictionary<string, object>();
            Properties["Factory"] = "";
            Properties["Symbol"] = "";
        }

        public T GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default(T);
            if (Properties.ContainsKey(key) == false)
                return default(T);

            object obj = Properties[key];
            if(obj==null || !(obj is T))
                return default(T);

            return (T)obj;
        }

        public string Factory
        {
            get
            {
                return GetValue<string>("Factory");
            }
            set
            {
                Properties["Properties"] = value;
            }
        }

        public string Symbol
        {
            get
            {
                return GetValue<string>("Symbol");
            }
            set
            {
                Properties["Symbol"] = value;
            }
        }
    }

    public class LineOptionSetting : OptionSetting
    {
        public LineOptionSetting()
        {
            Properties["Path"] = null;
        }

        public PathGeometry Path
        {
            get
            {
                return GetValue<PathGeometry>("Path");
            }
            set
            {
                Properties["Path"] = value;
            }
        }
    }
}
