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
    public class SolidStrokeSymbol : StrokeSymbol
    {
        public SolidStrokeSymbol()
        {

        }

        private static readonly string name = "SolidStrokeSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override Path Stroke(LineOptionSetting param)
        {
            System.Windows.Shapes.Path res = new System.Windows.Shapes.Path() { Stroke = new SolidColorBrush(param.Stroke), StrokeThickness = param.Width };
            res.Data = param.Path;
            return res;
        }
    }
}