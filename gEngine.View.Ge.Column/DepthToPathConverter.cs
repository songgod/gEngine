using gEngine.Graph.Ge.Column;
using gEngine.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Column
{
    public class DepthToPathConverter : IMultiValueConverter
    {
        public DepthToPathConverter()
        {
            
        }

        /// <summary>
        /// 加纵向比例
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = values[0] as ObsDoubles;
            if (dbs == null || dbs.Count <= 1)
                return null;

            double mindepth = dbs[0];//顶深
            double maxdepth = dbs[dbs.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dbs[0] / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = int.Parse(values[1].ToString()); //纵向比例
            if (LongitudinalProportion == 0)
                LongitudinalProportion = 1;
            PathGeometry geom = new PathGeometry();

            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = 0 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 1, Y = depth * Enums.PerMilePx / LongitudinalProportion } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }

            for (double i = firstScale; i <= depth; i = i + DepthToScaleConverter.MainScaleInterval)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = i * Enums.PerMilePx / LongitudinalProportion };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 15, Y = i * Enums.PerMilePx / LongitudinalProportion } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
