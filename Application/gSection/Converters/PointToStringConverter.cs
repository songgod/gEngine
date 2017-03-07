using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GPTDxWPFRibbonApplication1.Converters
{
    public class PointToStringConverter: IValueConverter
    {
        public string FormatString { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Point))
                return null;
            Point p = (Point)value;
            return String.Format(FormatString ?? "{0},{1}", p.X != -1 ? Math.Round(p.X).ToString() : "", p.X != -1 ? Math.Round(p.Y).ToString() : "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
