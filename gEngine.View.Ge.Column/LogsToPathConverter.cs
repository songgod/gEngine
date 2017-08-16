using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Utility;
using gEngine.View;
using System;
using System.Collections.Generic;
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
            InvalidValue = -9999;
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

            double mindepth = owner.TopDepth; //顶部层位-顶部延伸 深度
            double maxdepth = owner.BottomDepth; //底部层位+底部延伸 深度

            if (mindepth < 0)
                mindepth = owner.Depths[0];//如果顶部深度小于0，那么从数据深度起始点开始
            if (maxdepth > owner.Depths[owner.Depths.Count - 1])
                maxdepth = owner.Depths[owner.Depths.Count - 1];//如果底部深度大于数据深度，那么从数据深度终止点结束

            var start = owner.Depths.Where(x => x >= mindepth).First();//找出大于等于顶部深度的第一个数值
            var end = owner.Depths.Where(x => x >= maxdepth).First();//找出大于等于底部深度的第一个数值

            int idxMin = owner.Depths.IndexOf(start);//起始点位置
            int idxMax = owner.Depths.IndexOf(end);//终止点位置

            ObsDoubles Rvls = new ObsDoubles(vls.Where((item, index) => (index >= idxMin && index <= idxMax)).ToArray());//从曲线中提取出指定区间段的曲线数据
            double[] validVls = Rvls.Where(item => item != InvalidValue).ToArray();//从指定区间段的曲线数组中去除无效值，形成有效值数组

            double xMin = validVls.Min();
            double xMax = validVls.Max();

            MathType mathType = (MathType) values[2];
            if (mathType == MathType.DEFAULT)
            {
                //if (xMax - xMin > 60)
                //    mathType = MathType.ARITHM;
                //else
                mathType = MathType.LINER;
            }

            PathGeometry geom = new PathGeometry();

            double StartY = (owner.Depths[idxMin + Rvls.ToList().IndexOf(validVls[0])] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;// 计算StartPoint的Y值，Y值为第一个有效值的深度，X值为最小值，保证闭合时连线为直线
            double EndY = (owner.Depths[idxMin + Rvls.ToList().LastIndexOf(validVls[validVls.Length - 1])] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion; // 增加一个结束点，Y值为最后一个有效值的深度，X值为最小值，保证闭合时连线为直线

            PointCollection pointlist = new PointCollection();
            pointlist.Add(new Point() { X = 0, Y = StartY });

            for (int i = idxMin; i <= idxMax; ++i)
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
                    pointlist.Add(new Point() { X = x, Y = yValue });
                }
            }

            pointlist.Add(new Point() { X = 0, Y = EndY });

            PointCollection plist = GraphAlgo.SimpleLine.Simplifier(pointlist, 1);
            //PathFigure figure = new PathFigure() { StartPoint = plist[0], IsClosed = true };
            PathFigure figure = new PathFigure() { StartPoint = plist[0], IsClosed = false };
            PointCollection pc = new PointCollection(plist.ToList().GetRange(1, plist.Count - 1));
            PolyLineSegment pls = new PolyLineSegment() { Points = pc };
            figure.Segments.Add(pls);
            geom.Figures.Add(figure);
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
            InvalidValue = -9999;
        }

        public double InvalidValue { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles vls = values[0] as ObsDoubles;
            if (vls == null)
                return null;

            if (string.IsNullOrEmpty(values[1].ToString()))
                return null;

            string dataStr = values[1].ToString().Substring(1, values[1].ToString().Count() - 2).Replace("L", " ");

            PointCollectionConverter pcconverter = new PointCollectionConverter();
            PointCollection pc = new PointCollection();
            pc = (PointCollection) pcconverter.ConvertFromString(dataStr);
            List<Point> pointList = pc.ToList();

            double xMin_color = 0; // 色标最小值
            double xMax_color = 60; // 色标最大值

            // 定义色标颜色值（彩虹色）
            Color[] colors = new Color[7] { Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Indigo, Colors.Blue, Colors.Violet };

            // 定义色标与数值范围字典表
            System.Collections.Generic.Dictionary<string, string> colorDic = new System.Collections.Generic.Dictionary<string, string>();

            double interval = Math.Ceiling((xMax_color - xMin_color) / 6); //处理最大值与最小值间隔，保证彩虹色都使用

            for (int skip = 0; skip < 7; skip++)
            {
                double xRange = xMin_color + (interval * skip);
                double xNextRange = xMin_color + (interval * (skip + 1));
                colorDic.Add(string.Format(@"{0}_{1}", xRange, xNextRange), string.Format(@"{0}_{1}", skip, skip + 1));
                if (xNextRange >= xMax_color)
                    break;
            }

            //colorDic 目前参数没有使用，使用的是静态的，待设置功能开发完，更换 2017-3-22 sjm
            LinearGradientBrush lineGradientBrush = new LinearGradientBrush();

            // 垂直线性渐变
            lineGradientBrush.StartPoint = new Point(0.5, 0);
            lineGradientBrush.EndPoint = new Point(0.5, 1);

            lineGradientBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0 });

            char split = '_';
            for (int i = 0; i < pointList.Count; i++)
            {
                double xPoint = pointList[i].X > xMax_color ? xMax_color : pointList[i].X;
                var colorDicValue = colorDic.FirstOrDefault(x =>
                             double.Parse(x.Key.Split(split)[0]) <= xPoint &&
                             double.Parse(x.Key.Split(split)[1]) >= xPoint
                    ).Value;
                var colorDicKey = colorDic.FirstOrDefault(q => q.Value == colorDicValue).Key;

                string[] colorStrs = colorDicValue.Split(split);
                string[] keyStrs = colorDicKey.Split(split);

                Color s_color = colors[int.Parse(colorStrs[0])];
                Color d_color = colors[int.Parse(colorStrs[1])];

                Color vColor = GetGradientColor(s_color, d_color, xPoint, double.Parse(keyStrs[0]), double.Parse(keyStrs[1]));
                lineGradientBrush.GradientStops.Add(new GradientStop(vColor, (double) i / pointList.Count));
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
                                            (byte) (sourceColor.R + ((xValue - xMin) / (xMax - xMin) * redSpace)),
                                            (byte) (sourceColor.G + ((xValue - xMin) / (xMax - xMin) * greenSpace)),
                                            (byte) (sourceColor.B + ((xValue - xMin) / (xMax - xMin) * blueSpace))
                                        );

            return vColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
