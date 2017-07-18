using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveFaultManipulator : DrawCurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Points.Count == 0 || Graph == null)
                return;

            SectionLayerEdit editor = new SectionLayerEdit(SectionLayer);
            editor.AddFault(TrackAdorner.Points.ToList(), Tolerance);
            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class DrawCurveFaultManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCurveFaultManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawCurveFaultManipulator();
        }
    }
}
