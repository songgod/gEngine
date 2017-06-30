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
            Stroke = new SolidColorBrush(Colors.Red);
            Fill = new SolidColorBrush(Colors.White);
        }

        public string SymbolLib { get; set; }

        public string Symbol { get; set; }

        public Brush Stroke { get; set; }

        public Brush Fill { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
