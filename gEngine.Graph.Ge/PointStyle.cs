using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class PointStyle
    {
        public PointStyle()
        {
            Color = Colors.Black;
        }

        public string SymbolLib { get; set; }

        public string Symbol { get; set; }

        public Color Color { get; set; }
    }
}
