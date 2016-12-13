using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gEngineTest.Converter
{
    public class DepthToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = value as ObsDoubles;
            if (dbs == null || dbs.Count <= 1)
                return null;

            double top = Math.Floor(dbs[0] / 10) * 10;
            double bottom = Math.Ceiling(dbs[dbs.Count - 1] / 10) * 10;

            PathGeometry geom = new PathGeometry();

            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = top };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 1, Y = bottom } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }


            for (double i = top; i <= bottom; i = i + 20)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = i };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 15, Y = i } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }

            
            return geom;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
