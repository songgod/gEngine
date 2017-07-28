using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class GeRectanglePointSymbol : PointSymbol
    {
        public GeRectanglePointSymbol()
        {

        }

        private static readonly string name = "GeRectanglePointSymbol";
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
                w = 10.0;
            if (h <= 0)
                h = 10.0;

            Color stroke = param.Stroke;

            Brush fill = param.Fill;
            if (fill == null)
                fill = new SolidColorBrush(Colors.LightGray);

            //return new Rectangle() { Fill = fill, Stroke = stroke, Width=w,Height=h };
            return new Path { Fill = fill, Stroke = new SolidColorBrush(stroke), Data = new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, w, h) } };
        }
    }
}
