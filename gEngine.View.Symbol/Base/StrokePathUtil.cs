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
            PathGeometry traceGeom = param.Path;
            PathGeometry cGeom = new PathGeometry();
            Brush stroke = param.GetValue<Brush>("Stroke");
            stroke = new SolidColorBrush(Colors.Red);
            if (stroke == null)
                stroke = new SolidColorBrush(Colors.Black);
            double strokeThickness = param.GetValue<double>("Width");
            if (strokeThickness <= 0)
                strokeThickness = 1;
            Path res = new Path() { Stroke = stroke, StrokeThickness = strokeThickness };

            double symbolLen = symbolGeom.Bounds.Width;

            double traceLen = 0;
            PointCollection pc_result = PathGeometryToPoints.GetPointCollection(traceGeom.GetFlattenedPathGeometry());
            for (int i = 0; i < pc_result.Count - 1; i++)
            {
                traceLen += Distance(pc_result[i], pc_result[i + 1]);
            }


            double n = traceLen / symbolLen;
            double progress = 0;
            for (int i = 0; i < n; i++)
            {
                PathFigureCollection symbolpfc = symbolGeom.Figures;
                foreach (var pf in symbolpfc)
                {
                    Point StartPoint = pf.StartPoint;


                    Point point, tangent;

                    traceGeom.GetPointAtFractionLength(progress, out point, out tangent);

                    Matrix mt = new Matrix();

                    mt.RotateAt(Math.Atan2(tangent.Y, tangent.X) * 180 / Math.PI, StartPoint.X, StartPoint.Y);
                    mt.Translate(point.X - StartPoint.X, point.Y - StartPoint.Y);
                    Point cStartPoint = mt.Transform(StartPoint);

                    PathFigure cPathFigure = new PathFigure() { StartPoint = cStartPoint };

                    PathSegmentCollection symbolpsc = pf.Segments;

                    foreach (var ps in symbolpsc)
                    {
                        if (ps.GetType().Name == "PolyBezierSegment")
                        {
                            PolyBezierSegment pbs = ps as PolyBezierSegment;
                            PointCollection pc = new PointCollection();
                            for (int j = 0; j < pbs.Points.Count; j++)
                            {
                                progress = (pbs.Points[j].X - StartPoint.X + i * symbolLen) / traceLen;
                                traceGeom.GetPointAtFractionLength(progress, out point, out tangent);

                                Matrix mt_1 = new Matrix();
                                mt_1.RotateAt(Math.Atan2(tangent.Y, tangent.X) * 180 / Math.PI, pbs.Points[j].X, pbs.Points[j].Y);
                                mt_1.Translate(point.X - pbs.Points[j].X, point.Y - pbs.Points[j].Y);
                                Point pt = mt_1.Transform(pbs.Points[j]);
                                pc.Add(pt);
                            }
                            PolyBezierSegment cpbs = new PolyBezierSegment() { Points = pc };
                            cPathFigure.Segments.Add(cpbs);
                        }
                    }
                    cGeom.Figures.Add(cPathFigure);
                }
            }

            //symbolGeom.AddGeometry(param.Path);
            //cGeom.AddGeometry(traceGeom);



            res.Data = cGeom;
            return res;
        }

        static double Distance(Point p0, Point p1)
        {
            return Math.Sqrt((Math.Pow((p1.X - p0.X), 2) + Math.Pow((p1.Y - p0.Y), 2)));
        }
    }
}
