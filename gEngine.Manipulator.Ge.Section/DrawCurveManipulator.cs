using gEngine.Graph.Ge.Section;
using gTopology;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveManipulator : DrawCurveManipulatorBase
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            GraphUtil = new GraphUtil(this.AssociatedObject);
        }

        public GraphUtil GraphUtil { get; set; }
    }
}
