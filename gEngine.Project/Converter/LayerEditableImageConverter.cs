using gEngine.Project.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace gEngine.Project.Converter
{
    public class LayerEditableImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            if(b)
            {
                return ImageHelper.EditableIcon;
            }
            else
            {
                return ImageHelper.UnEditableIcon;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bmp = (BitmapImage)value;
            if (bmp == ImageHelper.EditableIcon)
                return true;
            else
                return false;
        }
    }
}
