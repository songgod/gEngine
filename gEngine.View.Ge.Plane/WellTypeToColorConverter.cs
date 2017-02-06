using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static gEngine.Graph.Ge.Plane.Enums;

namespace gEngine.View.Ge.Plane
{

    public class WellTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WellType wt = (WellType)value;
            switch (wt)
            {
                case WellType.W:
                    return Colors.Blue.ToString();
                case WellType.Y:
                    return Colors.Red.ToString();
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
