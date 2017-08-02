using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Basic
{
    public class TwoPoints2PathGeometryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Point start = (Point)values[0];
            Point end = (Point)values[1];

            return ConvertFrom(start, end);
        }

        public static PathGeometry ConvertFrom(Point start, Point end)
        {
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure() { StartPoint = start };

            LineSegment lseg = new LineSegment() { Point = end, IsStroked = true };
            pf.Segments.Add(lseg);
            pg.Figures.Add(pf);
            return pg;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Ponits2PolylinePathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection points = (PointCollection)value;
            return ConvertFrom(points);
        }

        public static PathGeometry ConvertFrom(PointCollection ps)
        {
            if (ps.Count <= 1)
                return null;
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure() { StartPoint = ps[0] };
            PolyLineSegment pls = new PolyLineSegment() { Points = new PointCollection(ps.Skip(1).Take(ps.Count - 1).ToList()) };
            pf.Segments.Add(pls);
            pg.Figures.Add(pf);
            return pg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Ponits2FillPathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection points = (PointCollection)value;
            return ConvertFrom(points);
        }

        public static PathGeometry ConvertFrom(PointCollection ps)
        {

            //PathGeometry geom = new PathGeometry();

            //{
            //    PathFigure figure = new PathFigure() { StartPoint = ps[0], IsClosed = true };
            //    PointCollection pc = ps;
            //    PolyBezierSegment pbseg = new PolyBezierSegment() { Points = pc };
            //    figure.Segments.Add(pbseg);
            //    geom.Figures.Add(figure);
            //}

            //if (ps != null)
            //{
            //    for (int i = 0; i < ps.Count; i++)
            //    {

            //        PathFigure figure = new PathFigure() { StartPoint = ps[0], IsClosed = true };
            //        PointCollection pc = ps;
            //        PolyBezierSegment pbseg = new PolyBezierSegment() { Points = pc };
            //        figure.Segments.Add(pbseg);
            //        geom.Figures.Add(figure);
            //    }
            //}

            //return geom;
            if (ps.Count <= 1)
                return null;
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure() { StartPoint = ps[0], IsClosed = true };
            PolyLineSegment pls = new PolyLineSegment() { Points = new PointCollection(ps.Skip(1).Take(ps.Count - 1).ToList()) };
            pf.Segments.Add(pls);
            pg.Figures.Add(pf);
            return pg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Points2PolyBezierPathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection points = (PointCollection)value;
            return ConvertFrom(points);
        }

        public static PathGeometry ConvertFrom(PointCollection ps)
        {
            if (ps.Count <= 1)
                return null;
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure() { StartPoint = ps[0] };
            PolyBezierSegment pls = new PolyBezierSegment() { Points = new PointCollection(ps.Skip(1).Take(ps.Count - 1).ToList()) };
            pf.Segments.Add(pls);
            pg.Figures.Add(pf);
            return pg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
