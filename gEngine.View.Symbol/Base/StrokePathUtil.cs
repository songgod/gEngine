using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol
{
    public static class StrokePathUtil
    {
        public static object GetAfterConverterGeom(PathGeometry symbolGeom, LineOptionSetting param)
        {
            Brush stroke = param.GetValue<Brush>("Stroke");
            if (stroke == null)
                stroke = new SolidColorBrush(Colors.Black);
            double strokeThickness = param.GetValue<double>("Width");
            if (strokeThickness <= 0)
                strokeThickness = 1;
            Path res = new Path() { Stroke = stroke, StrokeThickness = strokeThickness };

            Rect symbolRect = symbolGeom.Bounds;
            double symbolLen = symbolRect.Width; // 250
            double symbolWidth = symbolRect.Height; // 180

            Rect trajectRect = param.Path.Bounds;
            double trajectLen = trajectRect.Width; // 370
            double trajectWidth = trajectRect.Height; // 319.71269989013672

            // 0 <= n <= Lline / Ls (1.48)

            //PathGeometry destPath = new PathGeometry();
            //PathFigure destPf = new PathFigure() {StartPoint = param.Path.Figures[0].StartPoint };
            //PolyLineSegment ply = new PolyLineSegment();

            //PointCollection pc = PathGeometryToPoints.GetPointCollection(symbolGeom);
            //for (int i = 0; i < pc.Count; i++)
            //{
            //    Point point;
            //    Point tangent;
            //    double t = (pc[i].X + symbolLen) / trajectLen;
            //    param.Path.GetPointAtFractionLength(t, out point, out tangent);

            //    ply.Points.Add(point);
            //    double arc =   Math.Atan2(tangent.Y, tangent.X);
            //}

            //PointCollection pc = PathGeometryToPoints.GetPointCollection(param.Path);
            //res.Data = ConverterToBeizer(pc);
            //res.Data = param.Path;

            //TransformGroup transformGroup = new TransformGroup();

            //transformGroup.Children.Add(new TranslateTransform(100, 200));

            //res.RenderTransform = transformGroup;

            symbolGeom.AddGeometry(param.Path);

            res.Data = symbolGeom;
            return res;
        }
    }
}
