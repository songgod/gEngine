using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.normal
{
    public class EllpisePointSymbol : PointSymbol
    {
        public EllpisePointSymbol()
        {

        }

        private static readonly string name = "EllpisePointSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override object Create(PointOptionSetting param)
        {
            Path path = new Path() { Fill = new SolidColorBrush(Colors.Red), Stroke = new SolidColorBrush(Colors.Black)};
            path.Data = new EllipseGeometry() { RadiusX = 2, RadiusY = 2 };
            return path;
        }
    }
}