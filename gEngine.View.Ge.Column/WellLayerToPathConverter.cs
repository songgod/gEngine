using gEngine.Graph.Ge.Column;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Column
{
    class WellLayerToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Well owner = values[0] as Well;
            ObsDoubles topDepths = values[1] as ObsDoubles;
            ObsDoubles thickness = values[2] as ObsDoubles;
            if (topDepths == null || thickness == null || owner == null)
                return null;

            if (topDepths.Count != thickness.Count)
                return null;

            double mindepth = owner.Depths[0];
            PathGeometry geom = new PathGeometry();
            for (int i = 0; i < topDepths.Count; i++)
            {
                double bottomDepth = topDepths[i] + thickness[i];
                double yTop = (topDepths[i] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                double yBottom = (bottomDepth - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值

                PathFigure fg_1 = new PathFigure();
                fg_1.StartPoint = new Point() { X = 0, Y = yTop };
                LineSegment ls_1 = new LineSegment() { Point = new Point() { X = 60, Y = yTop } };
                fg_1.Segments.Add(ls_1);
                geom.Figures.Add(fg_1);

                PathFigure fg_2 = new PathFigure();
                fg_2.StartPoint = new Point() { X = 0, Y = yBottom };
                LineSegment ls_2 = new LineSegment() { Point = new Point() { X = 60, Y = yBottom } };
                fg_2.Segments.Add(ls_2);
                geom.Figures.Add(fg_2);
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class BoundaryNameToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Well owner = values[0] as Well;
            ObsDoubles topDepths = values[1] as ObsDoubles;
            ObsDoubles thickness = values[2] as ObsDoubles;
            List<string> BoundaryNames = values[3] as List<string>;

            if (topDepths == null || thickness == null || owner == null)
                return null;

            if (topDepths.Count != thickness.Count)
                return null;

            double mindepth = owner.Depths[0];
            PathGeometry geom = new PathGeometry();

            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            int i = 0;
            foreach (string boundaryName in BoundaryNames)
            {
                double bottomDepth = topDepths[i] + thickness[i];
                double yTop = (topDepths[i] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                double yBottom = (bottomDepth - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                //double middle = yTop + (yBottom - yTop) / 2.5;
                double middle = yTop + ((yBottom - yTop - font.Height) / 2 < 0 ? 0 : (yBottom - yTop - font.Height) / 2);
                PathGeometry path = GetTextPath(boundaryName, "微软雅黑", 12, middle);
                geom.AddGeometry(path);
                i++;
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
