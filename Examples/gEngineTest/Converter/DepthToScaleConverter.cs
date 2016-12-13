using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngineTest.Converter
{
    public class DepthToScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = value as ObsDoubles;
            if (dbs == null || dbs.Count <= 1)
                return null;

            double top = Math.Floor(dbs[0] / 10) * 10;
            double bottom = Math.Ceiling(dbs[dbs.Count - 1] / 10) * 10;

            StringBuilder scaleSb = new StringBuilder();
            for (double i = top; i <= bottom; i = i + 20)
            {
                scaleSb.Append(i);
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
