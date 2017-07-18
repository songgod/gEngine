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
            DicLineStyle[(int)SectionLineType.Fault] = new NormalLineStyle() { Color = Colors.Blue, Width = 3.0 };
            DicLineStyle[(int)SectionLineType.Sand] = new NormalLineStyle() { Color = Colors.Red, Width = 1.0 };
            DicLineStyle[(int)SectionLineType.Stratum] = new NormalLineStyle() { Color = Colors.Green, Width = 1.0 };
            DicFillStyle = new Dictionary<int, FillStyle>();
            DicFillStyle[(int)SectionLineType.Fault] = new SolidFillStyle() { Color = Colors.Blue };
            DicFillStyle[(int)SectionLineType.Sand] = new SolidFillStyle() { Color = Colors.Red };
            DicFillStyle[(int)SectionLineType.Stratum] = new SolidFillStyle() { Color = Colors.Green };
            NodeStyle = new PointStyle();
        }

        public gTopology.Graph TopGraph { get; set; }

        public Dictionary<int,LineStyle> DicLineStyle { get; set; }

        public Dictionary<int, FillStyle> DicFillStyle { get; set; }

        public PointStyle NodeStyle { get; set; }
    }
}
