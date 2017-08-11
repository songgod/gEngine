using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Section;
using gEngine.Symbol;
using gEngine.View.Ge;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Section
{
    public class Type2FillConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool valid = (bool)values[0];
            if (valid==false)
                return null;

            int type = (int)values[1];
            
            SectionInfo secobj = (SectionInfo)values[2];

            Brush fillb = new SolidColorBrush() { Color = Colors.Gray };
            if (secobj.DicFillStyle.ContainsKey(type) == false)
                return fillb;

            FillStyle fillstyle = secobj.DicFillStyle[type];
            Brush stylebrush = FillStyle2BrushConverter.ConverterFromFillStyle(fillstyle);
            if (stylebrush == null)
                return fillb;

            return stylebrush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Region2PathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() != 2)
                return null;

            gTopology.PointList pointlist = values[0] as gTopology.PointList;
            List<gTopology.PointList> insidepointlist = values[1] as List<gTopology.PointList>;
            if (pointlist == null || pointlist.Count == 0)
                return null;

            PathGeometry geom = new PathGeometry();

            {
                PathFigure figure = new PathFigure() { StartPoint = pointlist[0], IsClosed = true };
                PointCollection pc = new PointCollection(pointlist.GetRange(1, pointlist.Count - 1));
                PolyBezierSegment pbseg = new PolyBezierSegment() { Points = pc };
                figure.Segments.Add(pbseg);
                geom.Figures.Add(figure);
            }

            if (insidepointlist != null)
            {
                for (int i = 0; i < insidepointlist.Count; i++)
                {
                    gTopology.PointList templist = insidepointlist[i];
                    if (templist.Count == 0)
                        continue;
                    PathFigure figure = new PathFigure() { StartPoint = templist[0], IsClosed = true };
                    PointCollection pc = new PointCollection(templist.GetRange(1, templist.Count - 1));
                    PolyBezierSegment pbseg = new PolyBezierSegment() { Points = pc };
                    figure.Segments.Add(pbseg);
                    geom.Figures.Add(figure);
                }
            }

            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Type2LineStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)values[0];
            SectionInfo secobj = (SectionInfo)values[1];
            LineStyle linestyle = new LineStyle() { Stroke = Colors.Black, Width = 1.0 };
            if (secobj.DicLineStyle.ContainsKey(type) == false)
                return linestyle;
            return secobj.DicLineStyle[type];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Line2PathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Point start = (Point)values[0];
            gTopology.PointList plist = (gTopology.PointList)(values[1]);
            PointCollection points = new PointCollection(plist.ToArray());
            PathFigure pf = new PathFigure() { StartPoint = start };
            pf.Segments.Add(new PolyBezierSegment() { Points = points });
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            return pg;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Points2PathDataConverter : IValueConverter
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
