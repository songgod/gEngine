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
    /// <summary>
    /// 通过曲线模板画曲线道边框
    /// </summary>
    public class WellToModelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<LstWellColumns> lstWellColumns = values[0] as List<LstWellColumns>;
            if (lstWellColumns == null)
                return null;

            ObsDoubles depths = ((LstWellColumns) lstWellColumns[0]).Columns[0].Owner.Depths as ObsDoubles;

            if (depths == null || depths.Count <= 1)
                return null;

            double mindepth = depths[0];//顶深
            double maxdepth = depths[depths.Count - 1];//底深
            double depth = Math.Ceiling((maxdepth - mindepth) / 10) * 10;//取底深减顶深差值，向上取整
            double top = Math.Ceiling(depths[0] / 10) * 10;//顶深向上取整
            double firstScale = top - mindepth == 0 ? 10 : top - mindepth;//第一个刻度点
            int LongitudinalProportion = ((LstWellColumns) lstWellColumns[0]).Columns[0].Owner.LongitudinalProportion; //纵向比例
            int WellWidth = 0; //每口井宽度

            PathGeometry geom = new PathGeometry();

            // 画各曲线道边框竖线，由于曲线名称占60高度，需增加
            int i = 0;
            foreach (var item in lstWellColumns)
            {
                WellWidth += item.Columns[0].Width;
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = i * item.Columns[0].Width, Y = 0 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = i * item.Columns[0].Width, Y = 60 + depth * Enums.PerMilePx / LongitudinalProportion } };
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
}
