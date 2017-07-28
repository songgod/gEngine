using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol
{
    public abstract class StrokeSymbol : ISymbol
    {
        public static readonly string type = "Stroke";
        public string Type
        {
            get
            {
                return type;
            }
        }
        
        public abstract string Name { get; }

        public abstract Path Stroke(LineOptionSetting param);
    }

    public abstract class ComplexStrokeSymbol : StrokeSymbol
    {
        public abstract PathGeometry SymbolGeometry { get; }

        public override Path Stroke(LineOptionSetting param)
        {
            if (SymbolGeometry == null)
                return null;

            PathGeometry pg = StrokePathUtil.GetAfterConverterGeom(SymbolGeometry, param.Path,param.Width);

            System.Windows.Shapes.Path res = new System.Windows.Shapes.Path() { Stroke = new SolidColorBrush(param.Stroke), StrokeThickness = 1.0 };
            res.Data = pg;
            return res;
        }
    }
}
