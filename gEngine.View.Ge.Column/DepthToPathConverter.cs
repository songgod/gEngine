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
            Well owner = values[1] as Well;

            if (dbs == null || dbs.Count <= 1)
                return null;
            if (owner == null)
                return null;

            double mindepth = owner.TopDepth; //顶部层位-顶部延伸 深度
            double maxdepth = owner.BottomDepth; //底部层位+底部延伸 深度

            if (mindepth < 0)
                mindepth = owner.Depths[0];//如果顶部深度小于0，那么从数据深度起始点开始
            if (maxdepth > owner.Depths[owner.Depths.Count - 1])
                maxdepth = owner.Depths[owner.Depths.Count - 1];//如果底部深度大于数据深度，那么从数据深度终止点结束

            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(mindepth / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = owner.LongitudinalProportion; //纵向比例

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
