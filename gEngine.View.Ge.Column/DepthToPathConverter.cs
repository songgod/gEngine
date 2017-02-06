using gEngine.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Column
{
    public class DepthToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = value as ObsDoubles;
            if (dbs == null || dbs.Count <= 1)
                return null;


            double mindepth = dbs[0];//顶深
            double maxdepth = dbs[dbs.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dbs[0] / 10) * 10;//顶深向上取整
            double firstScale  = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点

            PathGeometry geom = new PathGeometry();

            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = 0 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 1, Y = depth } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }


            for (double i = firstScale; i <= depth; i = i + 20)
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
