using gEngine.Graph.Ge.Column;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.View.Ge.Column
{
    public class DepthToScaleConverter : IValueConverter
    {
        public DepthToScaleConverter()
        {
            MainScaleInterval = 20;
        }

        /// <summary>
        /// 主刻度数值步长
        /// </summary>
        public static double MainScaleInterval
        {
            get;
            set;
        }

        public static double FirstScale
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = value as ObsDoubles;
            if (dbs == null || dbs.Count <= 1)
                return null;

            double mindepth = dbs[0];//顶深
            double maxdepth = dbs[dbs.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dbs[0] / 10) * 10;//顶深向上取整
            FirstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点

            StringBuilder scaleSb = new StringBuilder();
            for (double i = FirstScale; i <= depth; i = i + MainScaleInterval)
            {
                scaleSb.Append((i + mindepth));
                scaleSb.Append("\n");
            }
            return scaleSb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 深度道两个刻度值距离转换类
    /// </summary>
    public class DepthDistanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int LongitudinalProportion = int.Parse(value.ToString()); //纵向比例

            double lineHight = DepthToScaleConverter.MainScaleInterval * Enums.PerMilePx / LongitudinalProportion;
            return lineHight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 为便于刻度值使用方便，对刻度进行了取整后展示，所以需要先取得相对于第一个刻度值的向上偏移量
    /// </summary>
    public class DepthFirstScaleTopOffset : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int LongitudinalProportion = int.Parse(value.ToString()); //纵向比例
            double firstScaleTopOffset = DepthToScaleConverter.FirstScale * Enums.PerMilePx / LongitudinalProportion - 8; //保持刻度值在刻度线中间
            return firstScaleTopOffset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
