using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.Symbol
{

    public class FillSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string symbol = value as string;

            if (string.IsNullOrEmpty(symbol))
                return Registry.DefaultFillSymbol.Create();

            string symbolname = symbol.Substring(symbol.LastIndexOf('@') + 1);
            string factoryname = symbol.Substring(0, symbol.LastIndexOf('@'));
            FillSymbol fsym = Registry.GetFillSymbol(factoryname, symbolname);
            return fsym.Create();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
