using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol
{
    public class DefaultLineSymbol : LineSymbol
    {
        public DefaultLineSymbol()
        {

        }

        private static readonly string name = "DefaultLineSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override object Create(OptionSetting param)
        {
            Path path = new Path() { Fill = new SolidColorBrush(Colors.Red), Stroke = new SolidColorBrush(Colors.Black) };
            path.Data = new EllipseGeometry() { RadiusX = 2, RadiusY = 2 };
            return path;
        }
    }
}
