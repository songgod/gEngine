using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineFaultManipulator : DrawLineManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            SectionLayer sectionlayer = this.AssociatedObject.LayerContext as SectionLayer;
            if (sectionlayer != null)
            {
                Graph = sectionlayer.SandObject.TopGraph;
                LineType = (int)SectionLayerEdit.SectionLineType.Fault;
            }
        }
    }

    public class DrawLineFaultManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawLineFaultManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawLineFaultManipulator();
        }
    }
}
