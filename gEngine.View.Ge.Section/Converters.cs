using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Section
{
    public class Type2FillConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool valid = (bool)values[1];
            if (valid==false)
                return null;

            return new SolidColorBrush() { Color = Colors.Yellow };
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

            if(insidepointlist!=null)
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
}
