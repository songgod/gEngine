using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static gEngine.Graph.Ge.Column.Enums;

namespace gEngine.View.Ge.Column
{
    public class LogsToPathConverter : IMultiValueConverter
    {
        public LogsToPathConverter()
        {
            InvalidValue = -99999;
        }

        public double InvalidValue { get; set; }

        /// <summary>
        /// 加纵向比例
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles vls = values[0] as ObsDoubles;
            Well owner = values[1] as Well;
            if (vls == null || owner == null)
                return null;

            if (owner.Depths.Count != vls.Count)
                return null;

            if (vls.Count <= 1)
                return null;

            double mindepth = owner.Depths[0];
            double maxdepth = owner.Depths[owner.Depths.Count - 1];
            double minusDepths = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整

            double[] validValueList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();//从曲线数组中去除无效值，形成有效值数组
            double xMin = validValueList.Min();
            double xMax = validValueList.Max();

            MathType mathType = (MathType)values[2];
            if (mathType == MathType.DEFAULT)
            {
                if (xMax - xMin > 60)
                    mathType = MathType.ARITHM;
                else
                    mathType = MathType.LINER;
            }

            PathGeometry geom = new PathGeometry();
            PathFigure fg = new PathFigure();
            PolyLineSegment pls = new PolyLineSegment();
            double StartY = (owner.Depths[vls.ToList().IndexOf(validValueList[0])] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;// 计算StartPoint的Y值，Y值为第一个有效值的深度，X值为最小值，保证闭合时连线为直线
            double EndY = (owner.Depths[vls.ToList().LastIndexOf(validValueList[validValueList.Length - 1])] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;// 增加一个结束点，Y值为最后一个有效值的深度，X值为最小值，保证闭合时连线为直线
            fg.StartPoint = new Point() { X = 0, Y = StartY };

            for (int i = 0; i < vls.Count; ++i)
            {
                if (vls[i] == InvalidValue)
                {
                    continue;
                }
                else
                {
                    double x = (vls[i] - xMin) * 60 / (xMax - xMin); // 横向比例默认为 （曲线最小值-曲线最大值），60代表道宽
                    if (mathType.Equals(Enums.MathType.ARITHM))
                    {
                        x = Math.Log10(vls[i]) - Math.Log10(xMin);
                    }
                    double yValue = (owner.Depths[i] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                    pls.Points.Add(new Point() { X = x, Y = yValue });
                }
            }

            pls.Points.Add(new Point(0, EndY));
            fg.Segments.Add(pls);
            fg.IsClosed = true;
            geom.Figures.Add(fg);
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 曲线线性渐变转换类
    /// </summary>
    public class LogsToFillConverter : IMultiValueConverter
    {
        public LogsToFillConverter()
        {
            InvalidValue = -99999;
        }

        public double InvalidValue { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles vls = values[0] as ObsDoubles;
            if (vls == null)
                return null;

            if (vls.Count <= 1)
                return null;

            double[] validValueList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();

            double xMin = Math.Floor(validValueList.Min()); // 色标最小值
            double xMax = Math.Ceiling(validValueList.Max()); // 色标最大值

            // 定义色标颜色值（彩虹色）
            Color[] colors = new Color[7] { Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Indigo, Colors.Blue, Colors.Violet };

            // 定义色标与数值范围字典表
            System.Collections.Generic.Dictionary<string, string> colorDic = new System.Collections.Generic.Dictionary<string, string>();

            double interval = Math.Ceiling((xMax - xMin) / 6); //处理最大值与最小值间隔，保证彩虹色都使用

            for (int skip = 0; skip < 7; skip++)
            {
                double xRange = xMin + (interval * skip);
                double xNextRange = xMin + (interval * (skip + 1));
                colorDic.Add(string.Format(@"{0}_{1}", xRange, xNextRange), string.Format(@"{0}_{1}", colors[skip].ToString(), colors[skip + 1].ToString()));
                if (xNextRange >= xMax)
                    break;
            }

            //colorDic 目前参数没有使用，使用的是静态的，待设置功能开发完，更换 2017-3-22 sjm
            LinearGradientBrush lineGradientBrush = new LinearGradientBrush();

            // 垂直线性渐变
            lineGradientBrush.StartPoint = new Point(0.5, 0);
            lineGradientBrush.EndPoint = new Point(0.5, 1);

            lineGradientBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0 });

            char split = '_';

            for (int i = 0; i < validValueList.Length; i++)
            {
                double xPoint = validValueList[i];

                var colorDicValue = colorDic.FirstOrDefault(x =>
                             double.Parse(x.Key.Split(split)[0]) <= xPoint &&
                             double.Parse(x.Key.Split(split)[1]) > xPoint
                    ).Value;
                var colorDicKey = colorDic.FirstOrDefault(q => q.Value == colorDicValue).Key;

                string[] colorStrs = colorDicValue.Split(split);
                string[] keyStrs = colorDicKey.Split(split);

                // 字符串颜色转换成Drawing.Color
                System.Drawing.Color s_color = System.Drawing.ColorTranslator.FromHtml(colorStrs[0]);
                System.Drawing.Color d_color = System.Drawing.ColorTranslator.FromHtml(colorStrs[1]);

                Color vColor = GetGradientColor(Color.FromRgb((byte)s_color.R, (byte)s_color.G, (byte)s_color.B), Color.FromRgb((byte)d_color.R, (byte)d_color.G, (byte)d_color.B), xPoint, double.Parse(keyStrs[0]), double.Parse(keyStrs[1]));
                lineGradientBrush.GradientStops.Add(new GradientStop(vColor, (double)i / validValueList.Length));
            }

            lineGradientBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 1 });
            return lineGradientBrush;
        }

        private Color GetGradientColor(Color sourceColor, Color destColor, double xValue, double xMin, double xMax)
        {
            int redSpace = destColor.R - sourceColor.R;
            int greenSpace = destColor.G - sourceColor.G;
            int blueSpace = destColor.B - sourceColor.B;

            Color vColor = Color.FromRgb(
                                            (byte)(sourceColor.R + ((xValue - xMin) / (xMax - xMin) * redSpace)),
                                            (byte)(sourceColor.G + ((xValue - xMin) / (xMax - xMin) * greenSpace)),
                                            (byte)(sourceColor.B + ((xValue - xMin) / (xMax - xMin) * blueSpace))
                                        );

            return vColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
