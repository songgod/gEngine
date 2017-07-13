using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class WavyLineSymbol : StrokeSymbol
    {
        private static readonly string name = "WavyLineSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override PathGeometry SymbolGeometry
        {
            get
            {
                PathGeometry geom = new PathGeometry();
                PathFigure figure = new PathFigure() { StartPoint = new Point(50, 120) };
                PointCollection pc = new PointCollection();
                pc.Add(new Point(60, 0));
                pc.Add(new Point(100,0));
                pc.Add(new Point(150, 120));
                pc.Add(new Point(170, 240));
                pc.Add(new Point(240, 240));
                pc.Add(new Point(300, 120));
                PolyBezierSegment pbs = new PolyBezierSegment() { Points = pc };
                figure.Segments.Add(pbs);
                geom.Figures.Add(figure);
                return geom;
            }
        }

        public Geometry ConverterToBeizer(PointCollection pc)
        {
            PathFigure pf = new PathFigure();
            pf.StartPoint = pc[0];

            List<Point> controls = new List<Point>();
            for (int i = 0; i < pc.Count; i++)
            {
                controls.AddRange(Control1(pc, i));
            }

            for (int i = 1; i < pc.Count; i++)
            {
                BezierSegment bs = new BezierSegment(controls[i * 2 - 1], controls[i * 2], pc[i], true);
                bs.IsSmoothJoin = true;
                pf.Segments.Add(bs);
            }

            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);
            PathGeometry pg = new PathGeometry(pfc);
            return pg;
        }

        public static List<Point> Control1(PointCollection pc, int n)
        {
            List<Point> point = new List<Point>();
            point.Add(new Point());
            point.Add(new Point());
            if (n == 0)
            {
                point[0] = pc[0];
            }
            else
            {
                point[0] = Average(pc[n - 1], pc[n]);
            }

            if (n == pc.Count - 1)
            {
                point[1] = pc[pc.Count - 1];
            }

            else
            {
                point[1] = Average(pc[n], pc[n + 1]);
            }

            Point ave = Average(point[0], point[1]);
            Point sh = Sub(pc[n], ave);
            point[0] = Mul(Add(point[0], sh), pc[n], 0.6);
            point[1] = Mul(Add(point[1], sh), pc[n], 0.6);
            return point;
        }

        public static Point Average(Point x, Point y)
        {
            return new Point((x.X + y.X) / 2, (x.Y + y.Y) / 2);
        }

        public static Point Add(Point x, Point y)
        {
            return new Point(x.X + y.X, x.Y + y.Y);
        }

        public static Point Sub(Point x, Point y)
        {
            return new Point(x.X - y.X, x.Y - y.Y);
        }

        public static Point Mul(Point x, Point y, double d)
        {
            Point temp = Sub(x, y);
            temp = new Point(temp.X * d, temp.Y * d);
            temp = Add(y, temp);
            return temp;
        }
    }
}
