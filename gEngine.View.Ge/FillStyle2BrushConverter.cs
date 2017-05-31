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
            if (fs.BrushType == FillStyle.FillType.SolidBrush)
            {
                SolidFillStyle sfs = fs as SolidFillStyle;
                SolidColorBrush sb = new SolidColorBrush() { Color = sfs.Color };
                return sb;
            }
            else if (fs.BrushType == FillStyle.FillType.GradientBrush)
            {
                GradientFillStyle gfs = fs as GradientFillStyle;
                LinearGradientBrush lgb = new LinearGradientBrush() { StartPoint = gfs.Start, EndPoint = gfs.End, GradientStops = gfs.Stops };
                return lgb;
            }
            else if (fs.BrushType == FillStyle.FillType.ImageBrush)
            {
                ImageFillStyle ifs = fs as ImageFillStyle;
                ImageSource imgsrc = new BitmapImage(new Uri(ifs.Image));
                return new ImageBrush(imgsrc);
            }
            else if (fs.BrushType == FillStyle.FillType.SymbolBrush)
            {
                SymbolFillStyle sfs = fs as SymbolFillStyle;
                OptionSetting setting = new OptionSetting();
                setting.Factory = sfs.SymbolLib;
                setting.Symbol = sfs.Symbol;
                setting.Properties["Color"] = sfs.Color;
                return Registry.CreateFillBrush(setting);
            }

            return null;
        } 
    }
}
