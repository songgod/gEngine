using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge.Section
{
    public class SectionObject : Object
    {
        public SectionObject()
        {
            TopGraph = new gTopology.Graph();
            DicLineStyle = new Dictionary<int, LineStyle>();
            DicLineStyle[(int)LineType.FaultLine] = new NormalLineStyle() { Color = Colors.Green };
            DicLineStyle[(int)LineType.StratumLine] = new NormalLineStyle() { Color = Colors.Black };
            DicFillStyle = new Dictionary<int, FillStyle>();
        }

        public gTopology.Graph TopGraph { get; set; }

        public enum LineType
        {
            UnKonwnLine=0,
            FaultLine,
            StratumLine
        }

        public Dictionary<int,LineStyle> DicLineStyle { get; set; }

        public Dictionary<int, FillStyle> DicFillStyle { get; set; }

        public PointStyle NodeStyle { get; set; }
    }
}
