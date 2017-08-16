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
