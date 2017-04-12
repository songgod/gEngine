using gEngine.Graph.Ge.Column;
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
    class WellLayerDataToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Well owner = values[0] as Well;
            List<string> DatasList = values[1] as List<string>;
            List<string> topDepths = owner.WellLayerDatas[0].WellLayerDatas;
            List<string> thickness = owner.WellLayerDatas[1].WellLayerDatas;
            double mindepth = owner.Depths[0];
            PathGeometry geom = new PathGeometry();
            for (int i = 0; i < owner.WellLayerDatas[0].WellLayerDatas.Count; i++)
            {
                double bottomDepth = double.Parse(topDepths[i]) + double.Parse(thickness[i]);
                double yTop = (double.Parse(topDepths[i]) - mindepth) * Enums.PerMilePx / owner.LongitudinalProportion;//根据纵向比例，计算出Y值
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
}
