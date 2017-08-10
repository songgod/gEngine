using gEngine.View;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class DrawCurveManipulatorBase : DrawPolyLineManipulatorBase
    {
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.Points.Clear();
        }
        protected override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.MouseLeftButtonDown(sender,e);
            this.TrackAdorner.Points.Clear();
            MapControl mc = this.AssociatedObject.Owner;
            Point p = mc.Dp2LP(e.GetPosition(mc));
            this.TrackAdorner.Points.Add(p);
        }

        protected override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                MapControl mc = this.AssociatedObject.Owner;
                Point p = mc.Dp2LP(e.GetPosition(mc));
                this.TrackAdorner.Points.Add(p);
            }
        }
    }
}
