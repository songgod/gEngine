using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class GeStoreLineSymbol : LineSymbol
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

            return new Path() { Width = 8, Stroke = stroke, Data = new LineGeometry() { StartPoint=new System.Windows.Point(0, h),EndPoint=new System.Windows.Point(100, h) }};
        }
    }
}
