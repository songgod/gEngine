using gEngine.Graph.Ge;
using gEngine.Symbol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.View.Ge
{
    public class LineStyle2OptionSettingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return LineStyle2OptionSettingConverter.CreateFromLineStyle(value as ComplexLineStyle);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static OptionSetting CreateFromLineStyle(ComplexLineStyle style)
        {
            if (style == null)
                return null;
            OptionSetting setting = new OptionSetting();
            setting.Factory = style.SymbolLib;
            setting.Symbol = style.Symbol;
            setting.Properties["Stroke"] = style.Stroke; 
            
            setting.Properties["Width"] = style.Width;
            
            return setting;
        }
    }
}
