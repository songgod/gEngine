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
    public class DrawCurveSandManipulator : DrawCurveManipulator
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
            editor.AddSand(TrackAdorner.Points.ToList(), GraphUtil.Tolerance);
            base.MouseLeftButtonUp(sender, e);
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
