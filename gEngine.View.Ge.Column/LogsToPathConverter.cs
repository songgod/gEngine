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
            double[] xMinList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();
            double[] xMaxList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();
            double xMin = xMinList.Min();
            double xMax = xMaxList.Max();

            MathType mathType = (MathType)values[2];
            if (mathType == MathType.DEFAULT)
            {
                if (xMax - xMin > 60)
                    mathType = MathType.ARITHM;
                else
                    mathType = MathType.LINER;
            }

            PathGeometry geom = new PathGeometry();
            PathFigure fg = null;
            PolyLineSegment pls = null;
            for (int i = 0; i < vls.Count; ++i)
            {
                if (vls[i] == InvalidValue)
                {
                    fg = null;
                    pls = null;
                }
                else
                {
                    //double x = vls[i] - xMin;
                    //if (mathType.Equals(Enums.MathType.ARITHM))
                    //{
                    //    x = Math.Log10(vls[i]) - Math.Log10(xMin);
                    //}

                    double x = (vls[i] - xMin) * 60 / (xMax-xMin); // 横向比例默认为 （曲线最小值-曲线最大值），60代表道宽
                    if (mathType.Equals(Enums.MathType.ARITHM))
                    {
                        x = Math.Log10(vls[i]) - Math.Log10(xMin);
                    }

                    double yValue = (owner.Depths[i] - mindepth) * Enums.PerMilePx  / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
                    if (fg == null)
                    {
                        fg = new PathFigure();
                        fg.StartPoint = new Point() { X = x, Y = yValue };
                        pls = new PolyLineSegment();
                        fg.Segments.Add(pls);
                        geom.Figures.Add(fg);
                    }
                    pls.Points.Add(new Point() { X = x, Y = yValue });
                }
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
