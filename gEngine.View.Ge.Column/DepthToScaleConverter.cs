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
    public class DepthToScaleConverter : IMultiValueConverter
    {
        public DepthToScaleConverter()
        {
        }

        public static double MainScaleInterval
        {
            get { return 20; }
        }

        public static double FirstScale
        {
            get;
            set;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = values[0] as ObsDoubles;
            Well owner = values[1] as Well;
            if (dbs == null || dbs.Count <= 1)
                return null;
            if (owner == null)
                return null;

            double mindepth = dbs[0];//顶深
            double maxdepth = dbs[dbs.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dbs[0] / 10) * 10;//顶深向上取整
            double FirstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = owner.LongitudinalProportion; //纵向比例

            PathGeometry geom = new PathGeometry();
            for (double i = FirstScale; i <= depth; i = i + DepthToScaleConverter.MainScaleInterval)
            {
                PathGeometry path = GetTextPath((i + mindepth).ToString(), "微软雅黑", 12, (i - 3) * Enums.PerMilePx / LongitudinalProportion);
                geom.AddGeometry(path);
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
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
