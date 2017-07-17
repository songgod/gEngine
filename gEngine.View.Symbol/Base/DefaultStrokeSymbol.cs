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
    public class DefaultStrokeSymbol : StrokeSymbol
    {
        public DefaultStrokeSymbol()
        {

        }

        private static readonly string name = "DefaultPointSymbol";
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
                pc.Add(new Point(250, 0));
                pc.Add(new Point(50, 200));
                pc.Add(new Point(300, 200));
                PolyBezierSegment pbs = new PolyBezierSegment() { Points = pc };
                figure.Segments.Add(pbs);
                geom.Figures.Add(figure);
                return geom;
            }
        }
    }
}