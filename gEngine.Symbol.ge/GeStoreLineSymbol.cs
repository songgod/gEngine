using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class GeStoreLineSymbol : ComplexStrokeSymbol
    {
        public GeStoreLineSymbol()
        {

        }

        private static readonly string name = "GeStoreLineSymbol";
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
                PathGeometry pg = new PathGeometry();
                PathFigure pf = new PathFigure() { StartPoint = new System.Windows.Point(0, 0) };
                LineSegment ls = new LineSegment() { Point = new System.Windows.Point(20, 0) };
                pf.Segments.Add(ls);
                pg.Figures.Add(pf);
                return pg;
            }
        }
        
    }
}
