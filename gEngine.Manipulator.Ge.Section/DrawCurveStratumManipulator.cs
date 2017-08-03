using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveStratumManipulator : DrawCurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Points.Count <= 4 || GraphUtil.Graph == null)
            {
                e.Handled = false;
                return;
            }

            SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
            editor.AddStratum(TrackAdorner.Points.ToList(), GraphUtil.Tolerance);
            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class DrawCurveStratumManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCurveStratumManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawCurveStratumManipulator();
        }
    }
}
