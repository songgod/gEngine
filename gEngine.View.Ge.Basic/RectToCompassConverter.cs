using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Basic
{
    public class RectToCompassConverter : IMultiValueConverter
    {
        /* 
         * object[] values                          : 所绑定的源的值 
         * Type targetType                          : 目标的类型 
         * object parameter                         : 绑定时所传递的参数 
         * System.Globalization.CultureInfo culture : 系统语言等信息 
         */
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double x = (double)values[0];
            double y = (double)values[1];
            double w = (double)values[2];
            double h = (double)values[3];

            PointCollection points = new PointCollection();
            Point p1 = new Point(x + w / 2, y);
            Point p2 = new Point(x, y + h);
            Point p3 = new Point(x + w / 2, y + h / 2 + h / 10);
            Point p4 = new Point(x + w, y + h);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);

            return points;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #region CreateInstance

        private volatile static RectToCompassConverter _instance = null;
        private static readonly object lockHelper = new object();
        public static RectToCompassConverter CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new RectToCompassConverter();
                }
            }
            return _instance;
        }

        #endregion
    }
}
