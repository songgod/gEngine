using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol.normal
{
    public class DotStrokeSymbol : StrokeSymbol
    {
        public string DotSymbolName { get; set; }
        public override string Name
        {
            get
            {
                return DotSymbolName;
            }
        }

        public DoubleCollection Dot { get; set; }

        public override Path Stroke(LineOptionSetting param)
        {
            System.Windows.Shapes.Path res = new System.Windows.Shapes.Path() { Stroke = new SolidColorBrush(param.Stroke), StrokeThickness = param.Width, StrokeDashArray= Dot };
            res.Data = param.Path;
            return res;
        }
    }
}
