using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.gesym
{
    public class GeStrokeSymbol : StrokeSymbol
    {
        private static readonly string name = "GeStrokeSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override object Create(LineOptionSetting param)
        {
            Brush stroke = param.GetValue<Brush>("Stroke");
            if (stroke == null)
                stroke = new SolidColorBrush(Colors.Black);

            Path res = new Path() { Stroke = stroke };
            res.Data = param.Path;
            return res;
        }
    }
}
