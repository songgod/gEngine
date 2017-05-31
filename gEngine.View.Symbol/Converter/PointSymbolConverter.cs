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
            OptionSetting option = value as OptionSetting;
            return Registry.CreatePoint(option);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
