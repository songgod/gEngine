using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class MapControlZoomPan : MapManipulator
    {
        private Point mousedown;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            this.AssociatedObject.MouseRightButtonDown += AssociatedObject_MouseRightButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseWheel += AssociatedObject_MouseWheel;
            this.AssociatedObject.ManipulationStarting += AssociatedObject_ManipulationStarting;
            this.AssociatedObject.ManipulationDelta += AssociatedObject_ManipulationDelta;

            mousedown = new Point(0, 0);
        }

        private void AssociatedObject_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AssociatedObject_ManipulationStarting(object sender, System.Windows.Input.ManipulationStartingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AssociatedObject_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            double sx = 1;
            double sy = 1;
            if (e.Delta > 0)
            {
                sx = 1.1;
                sy = 1.1;
            }
            else
            {
                sx = 0.9;
                sy = 0.9;
            }
            Point center = this.AssociatedObject.Dp2LP(e.GetPosition(this.AssociatedObject));
            this.AssociatedObject.Zoom(center, new Vector(sx, sy));
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point cp = this.AssociatedObject.Dp2LP(e.GetPosition(this.AssociatedObject));
                this.AssociatedObject.Move(cp - mousedown);
            }
        }

        private void AssociatedObject_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mousedown = this.AssociatedObject.Dp2LP(e.GetPosition(this.AssociatedObject));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseRightButtonDown -= AssociatedObject_MouseRightButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseWheel -= AssociatedObject_MouseWheel;
            this.AssociatedObject.ManipulationStarting -= AssociatedObject_ManipulationStarting;
            this.AssociatedObject.ManipulationDelta -= AssociatedObject_ManipulationDelta;
        }
    }
}
