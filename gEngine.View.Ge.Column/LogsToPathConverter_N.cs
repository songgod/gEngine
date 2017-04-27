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
using static gEngine.Graph.Ge.Column.Enums;
using static gEngine.Graph.Ge.Column.WellSegmentColumn;

namespace gEngine.View.Ge.Column
{
    class LogsToPathConverter_N : IMultiValueConverter
    {
        public LogsToPathConverter_N()
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
            Well owner = values[0] as Well;
            ObsDoubles dps = values[1] as ObsDoubles;
            ObsDoubles vls = values[2] as ObsDoubles;
            List<Segment> segs = values[3] as List<Segment>;

            //if (vls == null || owner == null)
            //    return null;

            //if (owner.Depths.Count != vls.Count)
            //    return null;

            //if (vls.Count <= 1)
            //    return null;

            PathGeometry geom = null;

            if (dps != null)
            {
                geom = GetDepths(owner, dps);
            }

            if (vls != null)
            {
                MathType mathType = (MathType) values[4];
                geom = GetLogs(owner, vls, mathType);
            }

            if (segs != null)
            {
                geom = GetSegments(owner, segs);
            }

            return geom;
        }

        public PathGeometry GetDepths(Well owner, ObsDoubles dps)
        {
            if (dps == null || dps.Count <= 1)
                return null;

            double mindepth = dps[0];//顶深
            double maxdepth = dps[dps.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dps[0] / 10) * 10;//顶深向上取整
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

        public PathGeometry GetLogs(Well owner, ObsDoubles vls, MathType mathType)
        {
            double mindepth = owner.Depths[0];
            double[] validValueList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();//从曲线数组中去除无效值，形成有效值数组
            double xMin = validValueList.Min();
            double xMax = validValueList.Max();

            if (mathType == MathType.DEFAULT)
            {
                //if (xMax - xMin > 60)
                //    mathType = MathType.ARITHM;
                //else
                mathType = MathType.LINER;
            }

            PathGeometry geom = new PathGeometry();
            double StartY = (owner.Depths[vls.ToList().IndexOf(validValueList[0])] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;// 计算StartPoint的Y值，Y值为第一个有效值的深度，X值为最小值，保证闭合时连线为直线
            double EndY = (owner.Depths[vls.ToList().LastIndexOf(validValueList[validValueList.Length - 1])] - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;// 增加一个结束点，Y值为最后一个有效值的深度，X值为最小值，保证闭合时连线为直线

            gTopology.PointList pointlist = new gTopology.PointList();
            pointlist.Add(new Point() { X = 0, Y = StartY });

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
                    pointlist.Add(new Point() { X = x, Y = yValue });
                }
            }

            pointlist.Add(new Point() { X = 0, Y = EndY });

            gTopology.PointList plist = gTopology.SimpleLine.Simplifier(pointlist, 1);

            PathFigure figure = new PathFigure() { StartPoint = plist[0], IsClosed = true };
            PointCollection pc = new PointCollection(plist.GetRange(1, plist.Count - 1));
            PolyLineSegment pls = new PolyLineSegment() { Points = pc };
            figure.Segments.Add(pls);
            geom.Figures.Add(figure);
            return geom;
        }

        public PathGeometry GetSegments(Well owner, List<Segment> segs)
        {
            double mindepth = owner.Depths[0];
            PathGeometry geom = new PathGeometry();
            foreach (var seg in segs)
            {
                double bottomDepth = seg.Top + seg.Bottom;
                double yTop = (seg.Top - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                double yBottom = (bottomDepth - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值

                PathFigure fg_1 = new PathFigure();
                fg_1.StartPoint = new Point() { X = 0, Y = yTop };
                PolyLineSegment ply = new PolyLineSegment();
                ply.Points.Add(new Point() { X = 60, Y = yTop });
                ply.Points.Add(new Point() { X = 60, Y = yBottom });
                ply.Points.Add(new Point() { X = 0, Y = yBottom });
                fg_1.Segments.Add(ply);
                fg_1.IsClosed = true;
                geom.Figures.Add(fg_1);
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WellModelConverter_N : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            WellColumn_Ns wellcols = values[0] as WellColumn_Ns;
            if (wellcols == null)
                return null;
            ObsDoubles depths = ((WellDepth) wellcols[0]).Depths as ObsDoubles;//深度
            if (depths == null || depths.Count <= 1)
                return null;

            double mindepth = depths[0];//顶深
            double maxdepth = depths[depths.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(depths[0] / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = ((WellDepth) wellcols[0]).Owner.LongitudinalProportion; //纵向比例
            int WellWidth = 0; //每口井宽度

            PathGeometry geom = new PathGeometry();

            // 画各曲线道边框竖线，由于曲线名称占60高度，需增加
            int i = 0;
            foreach (var item in wellcols)
            {
                WellWidth += item.Width;
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = i * item.Width, Y = 0 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = i * item.Width, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
                i++;
            }

            PathFigure fg2 = new PathFigure();
            fg2.StartPoint = new Point() { X = WellWidth, Y = 0 };
            LineSegment ls2 = new LineSegment() { Point = new Point() { X = WellWidth, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion } };
            fg2.Segments.Add(ls2);
            geom.Figures.Add(fg2);

            // 画曲线名称上下边框线，曲线名称定义的高度是50
            for (double y = 0; y <= 50; y = y + 50)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 0, Y = y };
                LineSegment ls = new LineSegment() { Point = new Point() { X = WellWidth, Y = y } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }


            // 画曲线底边框线
            PathFigure fg1 = new PathFigure();
            fg1.StartPoint = new Point() { X = 0, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion };
            LineSegment ls1 = new LineSegment() { Point = new Point() { X = WellWidth, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion } };
            fg1.Segments.Add(ls1);
            geom.Figures.Add(fg1);
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class NameToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Well owner = values[0] as Well;
            ObsDoubles dps = values[1] as ObsDoubles;
            List<Segment> segs = values[2] as List<Segment>;

            PathGeometry geom = null;

            if (dps != null)
            {
                geom = GetDepthNameGeo(owner, dps);
            }

            if (segs != null)
            {
                geom = GetSegmentGeo(owner, segs);
            }


            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public PathGeometry GetDepthNameGeo(Well owner, ObsDoubles dps)
        {
            double mindepth = dps[0];//顶深
            double maxdepth = dps[dps.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dps[0] / 10) * 10;//顶深向上取整
            double FirstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = owner.LongitudinalProportion; //纵向比例

            PathGeometry geom = new PathGeometry();

            StringBuilder scaleSb = new StringBuilder();
            for (double i = FirstScale; i <= depth; i = i + DepthToScaleConverter.MainScaleInterval)
            {
                PathGeometry path = GetTextPath((i + mindepth).ToString(), "微软雅黑", 12, (i - 3) * Enums.PerMilePx / LongitudinalProportion);
                geom.AddGeometry(path);
            }
            return geom;
        }

        public PathGeometry GetSegmentGeo(Well owner, List<Segment> segs)
        {
            double mindepth = owner.Depths[0];
            PathGeometry geom = new PathGeometry();
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            int i = 0;
            foreach (var item in segs)
            {
                double bottomDepth = item.Top + item.Bottom;
                double yTop = (item.Top - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                double yBottom = (bottomDepth - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值

                double middle = yTop + ((yBottom - yTop - font.Height) / 2 < 0 ? 0 : (yBottom - yTop - font.Height) / 2);
                if (string.IsNullOrEmpty(item.Name))
                    continue;
                PathGeometry path = GetTextPath(item.Name, "微软雅黑", 12, middle);
                geom.AddGeometry(path);
                i++;
            }
            return geom;
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

    public class LogsToFillConverter_N : IMultiValueConverter
    {
        public LogsToFillConverter_N()
        {
            InvalidValue = -9999;
        }

        public double InvalidValue { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles vls = values[0] as ObsDoubles;
            List<Segment> segs = values[1] as List<Segment>;
            string logsdata = values[2].ToString();

            LinearGradientBrush lineGradientBrush = null;

            if (vls != null)
            {
                lineGradientBrush = GetLogsFill(logsdata) as LinearGradientBrush;
            }

            if (segs != null)
            {
                lineGradientBrush = GetSegsFill(segs) as LinearGradientBrush;
            }

            return lineGradientBrush;
        }

        public object GetLogsFill(string logsdata)
        {
            if (string.IsNullOrEmpty(logsdata.ToString()))
                return null;

            string dataStr = logsdata.ToString().Substring(1, logsdata.ToString().Count() - 2).Replace("L", " ");

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

        public object GetSegsFill(List<Segment> segs)
        {
            LinearGradientBrush lineGradientBrush = new LinearGradientBrush();
            foreach (var item in segs)
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
