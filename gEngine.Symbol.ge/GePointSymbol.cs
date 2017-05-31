using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class GePointSymbol : PointSymbol
    {
        public GePointSymbol()
        {
            
        }

        private static readonly string name = "GePointSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override object Create(OptionSetting param)
        {
            if (param==null)
                return null;

            string shape = param.GetValue<string>("Shape");
            if (string.IsNullOrEmpty(shape))
                return null;

            double w = param.GetValue<double>("Width");
            double h = param.GetValue<double>("Height");
            if (w <= 0)
                w = 10.0;
            if (h <= 0)
                h = 10.0;

            Brush stroke = param.GetValue<Brush>("Stroke");
            if (stroke == null)
                stroke = new SolidColorBrush(Colors.Black);

            Brush fill = param.GetValue<Brush>("Fill");
            if (fill == null)
                fill = new SolidColorBrush(Colors.White);

            if (shape == "Ellipse")
            {
                return new Path() { Fill = fill, Stroke = stroke,Data= new EllipseGeometry() { RadiusX = w, RadiusY = h } };
            }
            else if(shape == "Rectangle")
            {
                return new Path() { Fill = fill, Stroke = stroke, Data = new RectangleGeometry() { RadiusX = w, RadiusY = h } };
            }
            return null;
        }
    }
}
