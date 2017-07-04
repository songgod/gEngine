using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class RectStyle
    {
        public enum RectType
        {
            NormalRect = 0,
            ComplexRect,
            Unkown
        }

        public virtual RectType RecType { get { return RectType.Unkown; } }
    }
    public class NormalRectStyle : RectStyle
    {
        public NormalRectStyle()
        {
            //Width = 4;
            Color = Colors.Red;
            Stroke = Brushes.Red;
            Fill = Brushes.Red;

        }
        public override RectType RecType { get { return RectType.NormalRect; } }

        public Color Color { get; set; }

        //public double Width { get; set; }

        public Brush Stroke { get; set; }
        public Brush Fill { get; set; }

    }

    public class ComplexRectStyle : RectStyle
    {
        public override RectType RecType { get { return RectType.ComplexRect; } }
        public string Symbol { get; set; }
        public string SymbolLib { get; set; }

        public Color Color { get; set; }

        public double Width { get; set; }
    }
}
