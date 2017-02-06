using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Symbol
{

    public class PointSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string symbol = (string)value;
            PointSymbol psym = SymbolMgr.GetSymbol(symbol, PointSymbol.type) as PointSymbol;
            return psym.Create();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
