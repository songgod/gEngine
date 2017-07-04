using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class GeEllipsePointSymbol : PointSymbol
    {
        public GeEllipsePointSymbol()
        {

        }

        private static readonly string name = "GeEllipsePointSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override object Create(OptionSetting param)
        {
            if (param == null)
                return null;

            double w = param.GetValue<double>("Width");
            double h = param.GetValue<double>("Height");
            if (w <= 0)
                w = 20.0;
            if (h <= 0)
                h = 20.0;

            Brush stroke = param.GetValue<Brush>("Stroke");
            if (stroke == null)
                stroke = new SolidColorBrush(Colors.Black);

            Brush fill = param.GetValue<Brush>("Fill");
            if (fill == null)
                fill = new SolidColorBrush(Colors.White);

            return new Path() { Fill = fill, Stroke = stroke, Data = new EllipseGeometry() { RadiusX = w / 2, RadiusY = h / 2 }, Height = h, Width = w, Stretch = Stretch.Fill };
        }
    }
}
