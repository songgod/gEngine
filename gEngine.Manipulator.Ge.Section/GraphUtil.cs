using gEngine.Graph.Ge.Section;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphUtil
    {
        public GraphUtil(LayerControl layercontrol)
        {
            LayerControl = layercontrol;
            SectionLayer = layercontrol.LayerContext as SectionLayer;
            if (SectionLayer != null)
            {
                Graph = SectionLayer.SectionInfo.TopGraph;
            }
        }

        public LayerControl LayerControl { get; set; }

        public SectionLayer SectionLayer { get; set; }

        public gTopology.Graph Graph { get; set; }

        public double Tolerance
        {
            get
            {
                return CalcTolerance.GetTolerance(LayerControl.Owner);
            }
        }
    }
}
