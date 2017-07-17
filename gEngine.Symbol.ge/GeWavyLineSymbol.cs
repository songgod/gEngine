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
    public class GeWavyLineSymbol : StrokeSymbol
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
                PathFigure figure = new PathFigure() { StartPoint = new Point(0, 0) };
                PointCollection pc = new PointCollection();
                pc.Add(new Point(5, 5));
                pc.Add(new Point(13, 5));
                pc.Add(new Point(20, 0));
                pc.Add(new Point(25, -5));
                pc.Add(new Point(31, -5));
                pc.Add(new Point(40, 0));
                PolyBezierSegment pbs = new PolyBezierSegment() { Points = pc };
                figure.Segments.Add(pbs);
                geom.Figures.Add(figure);
                return geom;
            }
        }
    }
}
