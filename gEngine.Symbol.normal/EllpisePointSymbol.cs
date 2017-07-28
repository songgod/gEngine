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
            if (param == null)
                return null;

            double w = param.Width;
            double h = param.Height;
            if (w <= 0)
                w = 20.0;
            if (h <= 0)
                h = 20.0;

            Color stroke = param.Stroke;

            Brush fill = param.Fill;
            if (fill == null)
                fill = new SolidColorBrush(Colors.LightGray);

            return new Path() { Fill = fill, Stroke = new SolidColorBrush(stroke), Data = new EllipseGeometry() { RadiusX = w / 2, RadiusY = h / 2 }, Stretch = Stretch.Uniform };
        }
    }
}