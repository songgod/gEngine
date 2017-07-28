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
                Properties["Factory"] = value;
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

    public class PointOptionSetting : OptionSetting
    {
        public PointOptionSetting()
        {
            Properties["Stroke"] = Colors.Black;
            Properties["Fill"] = new SolidColorBrush(Colors.White);
            Properties["Width"] = 20.0;
            Properties["Height"] = 20.0;
        }

        public Color Stroke
        {
            get
            {
                return GetValue<Color>("Stroke");
            }
            set
            {
                Properties["Stroke"] = value;
            }
        }

        public Brush Fill
        {
            get
            {
                return GetValue<Brush>("Fill");
            }
            set
            {
                Properties["Fill"] = value;
            }
        }

        public double Width
        {
            get
            {
                return GetValue<double>("Width");
            }
            set
            {
                Properties["Width"] = value;
            }
        }

        public double Height
        {
            get
            {
                return GetValue<double>("Height");
            }
            set
            {
                Properties["Height"] = value;
            }
        }
    }

    public class LineOptionSetting : OptionSetting
    {
        public LineOptionSetting()
        {
            Properties["Path"] = null;
            Properties["Width"] = 1.0;
            Properties["Stroke"] = Colors.Black;
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

        public double Width
        {
            get
            {
                return GetValue<double>("Width");
            }
            set
            {
                Properties["Width"] = value;
            }
        }

        public Color Stroke
        {
            get
            {
                return GetValue<Color>("Stroke");
            }
            set
            {
                Properties["Stroke"] = value;
            }
        }
    }
}
