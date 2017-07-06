using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class LineStyle
    {
        public enum LineType
        {
            NormalLine=0,
            ComplexLine,
            Unkown
        }

        public virtual LineType LinType { get { return LineType.Unkown; } }
    }

    public class NormalLineStyle : LineStyle
    {
        public NormalLineStyle()
        {
            Width = 1;
            Color = Colors.Black;
        }
        public override LineType LinType { get { return LineType.NormalLine; } }

        public Color Color { get; set; }

        public double Width { get; set; }
        
    }

    public class ComplexLineStyle : LineStyle
    {
        public ComplexLineStyle()
        {
            Stroke = new SolidColorBrush(Colors.Black);
        }
        public override LineType LinType { get { return LineType.ComplexLine; } }
        public string Symbol { get; set; }
        public string SymbolLib { get; set; }

        public Brush Stroke { get; set; }

        public double Width { get; set; }
    }
}
