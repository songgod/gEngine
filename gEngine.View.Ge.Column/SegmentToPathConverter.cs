using gEngine.Graph.Ge.Column;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static gEngine.Graph.Ge.Column.WellSegmentColumn;

namespace gEngine.View.Ge.Column
{
    public class SegmentToPathConverter : IMultiValueConverter
    {
        public SegmentToPathConverter()
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
            List<Segment> Segment = values[0] as List<Segment>;
            Well owner = values[1] as Well;
            int Width = Int32.Parse(values[2].ToString());
            if (Segment == null)
                return null;
            if (owner == null)
                return null;

            double mindepth = owner.TopDepth; //顶部层位-顶部延伸 深度
            double maxdepth = owner.BottomDepth; //底部层位+底部延伸 深度

            if (mindepth < 0)
                mindepth = owner.Depths[0];//如果顶部深度小于0，那么从数据深度起始点开始
            if (maxdepth > owner.Depths[owner.Depths.Count - 1])
                maxdepth = owner.Depths[owner.Depths.Count - 1];//如果底部深度大于数据深度，那么从数据深度终止点结束

            PathGeometry geom = new PathGeometry();
            foreach (var item in Segment)
            {
                double bottomDepth = item.Top + item.Bottom;

                if (item.Top >= mindepth && bottomDepth <= maxdepth)
                {
                    double yTop = (item.Top - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                    double yBottom = (bottomDepth - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                    PathFigure fg_1 = new PathFigure();
                    fg_1.StartPoint = new Point() { X = 0, Y = yTop };
                    PolyLineSegment ply = new PolyLineSegment();
                    ply.Points.Add(new Point() { X = Width, Y = yTop });
                    ply.Points.Add(new Point() { X = Width, Y = yBottom });
                    ply.Points.Add(new Point() { X = 0, Y = yBottom });
                    fg_1.Segments.Add(ply);
                    fg_1.IsClosed = true;
                    geom.Figures.Add(fg_1);
                }
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SegmentToFillConverter : IMultiValueConverter
    {
        public SegmentToFillConverter()
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
            List<Segment> Segment = values[0] as List<Segment>;
            if (Segment == null)
                return null;

            LinearGradientBrush lineGradientBrush = new LinearGradientBrush();
            foreach (var item in Segment)
            {
                if (!string.IsNullOrEmpty(item.Color.ToString()))
                {
                    lineGradientBrush.StartPoint = new Point(0.5, 0);
                    lineGradientBrush.EndPoint = new Point(0.5, 1);

                    lineGradientBrush.GradientStops.Add(new GradientStop() { Color = item.Color, Offset = 0 });
                    lineGradientBrush.GradientStops.Add(new GradientStop() { Color = item.Color, Offset = 1 });
                }

            }
            return lineGradientBrush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SegmentToNameConverter : IMultiValueConverter
    {
        public SegmentToNameConverter()
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
            List<Segment> Segment = values[0] as List<Segment>;
            Well owner = values[1] as Well;
            if (Segment == null)
                return null;
            if (owner == null)
                return null;

            double mindepth = owner.TopDepth; //顶部层位-顶部延伸 深度
            double maxdepth = owner.BottomDepth; //底部层位+底部延伸 深度

            if (mindepth < 0)
                mindepth = owner.Depths[0];//如果顶部深度小于0，那么从数据深度起始点开始
            if (maxdepth > owner.Depths[owner.Depths.Count - 1])
                maxdepth = owner.Depths[owner.Depths.Count - 1];//如果底部深度大于数据深度，那么从数据深度终止点结束

            PathGeometry geom = new PathGeometry();
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            foreach (var item in Segment)
            {
                double bottomDepth = item.Top + item.Bottom;
                if (item.Top >= mindepth && bottomDepth <= maxdepth)
                {
                    if (string.IsNullOrEmpty(item.Name))
                        continue;
                    double yTop = (item.Top - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                    double yBottom = (bottomDepth - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                    double middle = yTop + ((yBottom - yTop - font.Height) / 2 < 0 ? 0 : (yBottom - yTop - font.Height) / 2);
                    PathGeometry path = GetTextPath(item.Name, "微软雅黑", 12, middle);
                    geom.AddGeometry(path);
                }
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public PathGeometry GetTextPath(string word, string fontFamily, int fontSize, double yPoint)
        {
            Typeface typeface = new Typeface(new FontFamily(fontFamily), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            FormattedText text = new FormattedText(word,
                new System.Globalization.CultureInfo("zh-cn"),
                FlowDirection.LeftToRight, typeface, fontSize,
                Brushes.Black);
            Geometry geo = text.BuildGeometry(new Point(20, yPoint));
            PathGeometry path = geo.GetFlattenedPathGeometry();
            return path;
        }
    }
}
