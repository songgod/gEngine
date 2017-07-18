using gEngine.Graph.Ge.Section;
using gTopology;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveManipulator : CurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            SectionLayer = this.AssociatedObject.LayerContext as SectionLayer;
            if (SectionLayer != null)
            {
                Graph = SectionLayer.SectionInfo.TopGraph;
            }
        }

        public SectionLayer SectionLayer { get; set; }

        public gTopology.Graph Graph { get; set; }

        public double Tolerance
        {
            get
            {
                return CalcTolerance.GetTolerance(this.AssociatedObject);
            }
        }
    }
}
