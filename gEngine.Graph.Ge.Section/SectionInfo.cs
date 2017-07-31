using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge.Section
{
    public class SectionInfo
    {
        public SectionInfo()
        {
            TopGraph = new gTopology.Graph();
            DicLineStyle = new Dictionary<int, LineStyle>();
            DicLineStyle[(int)SectionLineType.Fault] = new LineStyle() { Stroke = Colors.Blue, Width = 3.0 };
            DicLineStyle[(int)SectionLineType.Sand] = new LineStyle() { Stroke = Colors.Red, Width = 1.0 };
            DicLineStyle[(int)SectionLineType.Stratum] = new LineStyle() { Stroke = Colors.Green, Width = 1.0 };
            DicFillStyle = new Dictionary<int, FillStyle>();
            DicFillStyle[(int)SectionLineType.Fault] = new FillStyle() { SymbolLib="Normal", Symbol="Blue"};
            DicFillStyle[(int)SectionLineType.Sand] = new FillStyle() { SymbolLib = "Normal", Symbol = "Red" };
            DicFillStyle[(int)SectionLineType.Stratum] = new FillStyle() { SymbolLib = "Normal", Symbol = "Green" };
            NodeStyle = new PointStyle();
        }

        public gTopology.Graph TopGraph { get; set; }

        public Dictionary<int,LineStyle> DicLineStyle { get; set; }

        public Dictionary<int, FillStyle> DicFillStyle { get; set; }

        public PointStyle NodeStyle { get; set; }
    }
}
