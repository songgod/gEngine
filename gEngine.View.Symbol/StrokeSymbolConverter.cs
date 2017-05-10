using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Symbol
{
    public class StrokeSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PathGeometry path = parameter as PathGeometry;
            if (path == null)
                return null;

            string symbol = value as string;

            if (string.IsNullOrEmpty(symbol))
                return Registry.DefaultStrokeSymbol.Create(path);

            string symbolname = symbol.Substring(symbol.LastIndexOf('@') + 1);
            string factoryname = symbol.Substring(0, symbol.LastIndexOf('@'));
            StrokeSymbol ssym = Registry.GetStrokeSymbol(factoryname, symbolname);
            return ssym.Create(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
