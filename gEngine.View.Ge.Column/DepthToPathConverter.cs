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
            if (dbs == null || dbs.Count <= 1)
                return null;

            double mindepth = dbs[0];//顶深
            double maxdepth = dbs[dbs.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(dbs[0] / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = int.Parse(values[1].ToString()); //纵向比例

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

    /// <summary>
    /// 通过曲线模板画曲线道边框
    /// </summary>
    public class WellModelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            WellColumns cols = values[0] as WellColumns;//曲线数据
            ObsDoubles depths = values[1] as ObsDoubles;//深度
            WellLayerDatas layerDatas = values[3] as WellLayerDatas;
            if (depths == null || depths.Count <= 1)
                return null;

            double mindepth = depths[0];//顶深
            double maxdepth = depths[depths.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(depths[0] / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = int.Parse(values[2].ToString()); //纵向比例

            int colsCount = cols.Count + 1 + 1 + layerDatas.Count;//曲线条数（包括深度曲线、层号曲线和分层数据曲线）
            int colsWidth = 60;//曲线宽度，目前深度曲线宽度、层号曲线和分层数据曲线宽度也为60

            PathGeometry geom = new PathGeometry();
            {
                // 画曲线底边框线
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 0, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion };
                LineSegment ls = new LineSegment() { Point = new Point() { X = colsCount * colsWidth, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }

            // 画各曲线道边框竖线，由于曲线名称占60高度，需增加
            for (double i = 0; i <= colsCount * colsWidth; i = i + colsWidth)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = i, Y = 0 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = i, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }

            // 画曲线名称上下边框线，曲线名称定义的高度是50
            for (double y = 0; y <= 50; y = y + 50)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 0, Y = y };
                LineSegment ls = new LineSegment() { Point = new Point() { X = colsCount * colsWidth, Y = y } };
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
