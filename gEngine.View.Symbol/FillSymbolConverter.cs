using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Symbol
{

    public class FillSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string symbol = value as string;
            return SymbolMgr.GetFillSymbol(symbol).Create();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
