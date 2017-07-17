using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Reflection;

namespace gEngine.Symbol
{
    public static class StrokePathUtil
    {
        static double GetSymbolWidth(PathGeometry symbolGeom)
        {
            return symbolGeom.Bounds.Width;
        }

        static double GetTraceLen(PathGeometry traceGeom)
        {
            PathGeometry pg = traceGeom.GetFlattenedPathGeometry();
            double l = 0.0;
            Point lstp;
            foreach (var f in pg.Figures)
            {
                lstp = f.StartPoint;
                foreach (var s in f.Segments)

                {
                    PolyLineSegment pls = s as PolyLineSegment;
                    foreach (var p in pls.Points)
                    {
                        Vector v = p - lstp;
                        l += v.Length;
                        lstp = p;
                    }

                }
                break;
            }
            return l;
        }

        static Point TransPoint(Point p, Point basep, Point tangent)
        {
            Matrix rm = new Matrix();
            double ta = Math.Atan2(tangent.Y, tangent.X) * 180 / Math.PI;
            rm.Rotate(ta);
            rm.Rotate(90);
            Matrix tm = new Matrix();
            tm.Translate(basep.X, basep.Y);
            Point pw = new Point(p.Y, 0);

            Point tp = pw * rm * tm;
            return tp;
        }

        public static object GetAfterConverterGeom(PathGeometry symbolGeom, LineOptionSetting param)
        {
            PathGeometry cpg = new PathGeometry();
            Brush stroke = param.GetValue<Brush>("Stroke");
            if (stroke == null)
                stroke = new SolidColorBrush(Colors.Black);
            double strokeThickness = param.GetValue<double>("Width");
            if (strokeThickness <= 0)
                strokeThickness = 1;
            Path res = new Path() { Stroke = stroke, StrokeThickness = strokeThickness };

            PathGeometry tracepg = param.Path;

            double symbolLen = GetSymbolWidth(symbolGeom);
            double traceLen = GetTraceLen(tracepg);

            double t_traceLen = traceLen;
            double n = 0;

            while (t_traceLen > 0)
            {
                foreach (var pf in symbolGeom.Figures)
                {
                    Point StartPoint = pf.StartPoint;
                    Point point, tangent;
                    double progress = (StartPoint.X + n * symbolLen) / traceLen;
                    tracepg.GetPointAtFractionLength(progress, out point, out tangent);
                    Point cStartPoint = TransPoint(StartPoint, point, tangent);
                    PathFigure cpf = new PathFigure() { StartPoint = cStartPoint };
                    cpg.Figures.Add(cpf);
                    foreach (var s in pf.Segments)
                    {
                        if (s is PolyBezierSegment)
                        {
                            PolyBezierSegment pbs = s as PolyBezierSegment;
                            PolyBezierSegment cpbs = new PolyBezierSegment();
                            foreach (var pt in pbs.Points)
                            {
                                progress = (pt.X + n * symbolLen) / traceLen;
                                Point outpt, outtangent;
                                tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                                Point cpt = TransPoint(pt, outpt, outtangent);
                                cpbs.Points.Add(cpt);
                            }
                            cpf.Segments.Add(cpbs);
                        }
                        else if (s is PolyQuadraticBezierSegment)
                        {
                            PolyQuadraticBezierSegment pqbs = s as PolyQuadraticBezierSegment;
                            PolyQuadraticBezierSegment cpqbs = new PolyQuadraticBezierSegment();
                            foreach (var pt in pqbs.Points)
                            {
                                progress = (pt.X + n * symbolLen) / traceLen;
                                Point outpt, outtangent;
                                tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                                Point cpt = TransPoint(pt, outpt, outtangent);
                                cpqbs.Points.Add(cpt);
                            }
                            cpf.Segments.Add(cpqbs);
                        }
                        else if (s is BezierSegment)
                        {
                            BezierSegment bes = s as BezierSegment;
                            BezierSegment cbes = new BezierSegment();
                            PointCollection pc = new PointCollection();
                            for (int i = 1; i <= 3; i++)
                            {
                                PropertyInfo proInfo = bes.GetType().GetRuntimeProperty("Point" + i);
                                Point pt = (Point) proInfo.GetValue(bes);
                                progress = (pt.X + n * symbolLen) / traceLen;
                                Point outpt, outtangent;
                                tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                                Point cpt = TransPoint(pt, outpt, outtangent);
                                pc.Add(cpt);
                            }
                            cbes.Point1 = pc[0];
                            cbes.Point2 = pc[1];
                            cbes.Point3 = pc[2];
                            cpf.Segments.Add(cbes);
                        }
                        else if (s is QuadraticBezierSegment)
                        {
                            QuadraticBezierSegment qbes = s as QuadraticBezierSegment;
                            QuadraticBezierSegment cqbes = new QuadraticBezierSegment();
                            PointCollection pc = new PointCollection();
                            for (int i = 1; i <= 2; i++)
                            {
                                PropertyInfo proInfo = qbes.GetType().GetRuntimeProperty("Point" + i);
                                Point pt = (Point) proInfo.GetValue(qbes);
                                progress = (pt.X + n * symbolLen) / traceLen;
                                Point outpt, outtangent;
                                tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                                Point cpt = TransPoint(pt, outpt, outtangent);
                                pc.Add(cpt);
                            }
                            cqbes.Point1 = pc[0];
                            cqbes.Point2 = pc[1];
                            cpf.Segments.Add(cqbes);
                        }
                        else if (s is ArcSegment)
                        {
                            ArcSegment asm = s as ArcSegment;
                            ArcSegment casm = new ArcSegment();
                            progress = (asm.Point.X + n * symbolLen) / traceLen;
                            Point outpt, outtangent;
                            tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                            Point cpt = TransPoint(asm.Point, outpt, outtangent);
                            casm.Point = cpt;
                            cpf.Segments.Add(casm);
                        }
                        else if (s is PolyLineSegment)
                        {
                            PolyLineSegment pls = s as PolyLineSegment;
                            PolyLineSegment cpls = new PolyLineSegment();
                            foreach (var pt in pls.Points)
                            {
                                progress = (pt.X + n * symbolLen) / traceLen;
                                Point outpt, outtangent;
                                tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                                Point cpt = TransPoint(pt, outpt, outtangent);
                                cpls.Points.Add(cpt);
                            }
                            cpf.Segments.Add(cpls);
                        }
                        else if (s is LineSegment)
                        {
                            LineSegment ls = s as LineSegment;
                            LineSegment cls = new LineSegment();
                            progress = (ls.Point.X + n * symbolLen) / traceLen;
                            Point outpt, outtangent;
                            tracepg.GetPointAtFractionLength(progress, out outpt, out outtangent);
                            Point cpt = TransPoint(ls.Point, outpt, outtangent);
                            cls.Point = cpt;
                            cpf.Segments.Add(cls);
                        }
                        else { }
                    }
                }
                t_traceLen -= symbolLen;
                n++;
            }
            cpg.AddGeometry(tracepg);
            res.Data = cpg;
            return res;
        }
    }
}
