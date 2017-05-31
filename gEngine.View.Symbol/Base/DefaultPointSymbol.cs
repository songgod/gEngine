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
    public class DefaultPointSymbol : PointSymbol
    {
        public DefaultPointSymbol()
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

        public override object Create(OptionSetting param)
        {
            Path path = new Path() { Fill = new SolidColorBrush(Colors.White), Stroke = new SolidColorBrush(Colors.Black) };
            path.Data = new EllipseGeometry() { RadiusX = 10, RadiusY = 10 };
            return path;
        }
    }
}