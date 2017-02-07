using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.View.Symbol
{
    public class DefaultPointSymbol : PointSymbol
    {
        private EllipseGeometry geom;
        public DefaultPointSymbol()
        {
            geom = new EllipseGeometry() { RadiusX = 10, RadiusY = 10 };
        }

        private static readonly string name = "DefaultPointSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override Geometry Create()
        {
            return geom;
        }
    }
}
