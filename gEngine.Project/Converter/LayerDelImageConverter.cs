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
    class LayerDelImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            //if (b)
            //{
            //    return ImageHelper.EditableIcon;
            //}
            //else
            //{
            //    return ImageHelper.UnEditableIcon;
            //}
            return ImageHelper.DelIcon;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bmp = (BitmapImage)value;
            if (bmp == ImageHelper.DelIcon)
                return true;
            else
                return true;
        }
    }
}
