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
            string symbol = value as string;
            if (string.IsNullOrEmpty(symbol))
                return Registry.DefaultPointSymbol.Create();

            string symbolname = symbol.Substring(symbol.LastIndexOf('@') + 1);
            string factoryname = symbol.Substring(0,symbol.LastIndexOf('@'));
            PointSymbol psym = Registry.GetPointSymbol(factoryname, symbolname);
            return psym.Create();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
