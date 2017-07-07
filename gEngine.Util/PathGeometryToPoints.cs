using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Util
{
    public class PathGeometryToPoints
    {
        public static PointCollection GetPointCollection(PathGeometry geom)
        {
            PointCollection pc = new PointCollection();
            IEnumerator<PathFigure> Epathfigure = geom.Figures.GetEnumerator();
            while (Epathfigure.MoveNext())
            {
                pc.Add(Epathfigure.Current.StartPoint);
                IEnumerator<PathSegment> Esegment = Epathfigure.Current.Segments.GetEnumerator();
                while (Esegment.MoveNext())
                {
                    PathSegment p = Esegment.Current;
                    Type t = Esegment.Current.GetType();
                    PropertyInfo info = null;
                    PointCollection ps = new PointCollection();
                    if (t.Name == "ArcSegment" || t.Name == "LineSegment")
                    {
                        info = t.GetRuntimeProperty("Point");
                        Point pt = (Point) info.GetValue(p);
                        pc.Add(pt);
                    }
                    else if (t.Name.Equals("BezierSegment"))
                    {
                        info = t.GetRuntimeProperty("Point3");
                        Point pt = (Point) info.GetValue(p);
                        pc.Add(pt);
                    }
                    else if (t.Name.Equals("QuadraticBezierSegment"))
                    {
                        info = t.GetRuntimeProperty("Point2");
                        Point pt = (Point) info.GetValue(p);
                        pc.Add(pt);
                    }
                    else
                    {
                        info = t.GetRuntimeProperty("Points");
                        ps = (PointCollection) info.GetValue(p);
                    }

                    for (int i = 0; i < ps.Count; i++)
                    {
                        if (t.Name.Equals("PolyBezierSegment"))
                        {
                            if ((i + 1) % 3 == 0)
                                pc.Add(ps[i]);
                            continue;
                        }
                        if (t.Name.Equals("PolyQuadraticBezierSegment"))
                        {
                            if ((i + 1) % 2 == 0)
                                pc.Add(ps[i]);
                            continue;
                        }

                        pc.Add(ps[i]);
                    }
                }
            }

            return pc;
        }
    }
}
