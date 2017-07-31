using gEngine.Graph.Ge;
using gEngine.Symbol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace gEngine.View.Ge
{
    public class FillStyle2BrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FillStyle fs = value as FillStyle;
            return ConverterFromFillStyle(fs);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static Brush ConverterFromFillStyle(FillStyle fs)
        {
            if (fs == null)
                return null;

            OptionSetting setting = new OptionSetting();
            setting.Factory = fs.SymbolLib;
            setting.Symbol = fs.Symbol;
            return Registry.CreateFillBrush(setting);
        } 
    }
}
