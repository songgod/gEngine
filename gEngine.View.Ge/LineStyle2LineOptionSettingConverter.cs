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

namespace gEngine.View.Ge
{
    public class LineStyle2LineOptionSettingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return null;

            LineStyle ls = values[0] as LineStyle;
            PathGeometry pg = values[1] as PathGeometry;
            if (ls == null || pg == null)
                return null;

            return ConvertFromLineStyle(ls, pg);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static LineOptionSetting ConvertFromLineStyle(LineStyle style, PathGeometry path)
        {
            if (style == null || path == null)
                return null;

            LineOptionSetting setting = new LineOptionSetting();
            setting.Factory = style.SymbolLib;
            setting.Symbol = style.Symbol;
            setting.Stroke = style.Stroke;
            setting.Width = style.Width;
            setting.Path = path;
            return setting;
        }
    }
}
