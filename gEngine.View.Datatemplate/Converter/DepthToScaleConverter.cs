using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.View.Datatemplate.Converter
{
    public class DepthToScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = value as ObsDoubles;
            if (dbs == null || dbs.Count <= 1)
                return null;

            double mindepth = dbs[0];//顶深
            double maxdepth = dbs[dbs.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dbs[0] / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点

            StringBuilder scaleSb = new StringBuilder();
            for (double i = firstScale; i <= depth; i = i + 20)
            {
                scaleSb.Append(i + mindepth);
                scaleSb.Append("\n");
            }
            return scaleSb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
