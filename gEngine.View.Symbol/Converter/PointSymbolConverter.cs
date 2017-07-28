using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.Symbol
{

    public class PointSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointOptionSetting option = value as PointOptionSetting;
            return Registry.CreatePoint(option);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
