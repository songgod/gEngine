using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveSandManipulator : DrawCurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            SectionLayer sectionlayer = this.AssociatedObject.LayerContext as SectionLayer;
            if (sectionlayer != null)
            {
                Graph = sectionlayer.SandObject.TopGraph;
                LineType = (int)SectionLayerEdit.SectionLineType.Sand;
            }
        }
    }

    public class DrawCurveSandManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCurveSandManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawCurveSandManipulator();
        }
    }
}
