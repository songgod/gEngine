using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class MapControlZoomPan : MapManipulator
    {
        private Point mousedown;
        private Point center;

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
            int num = e.Manipulators.Count<IManipulator>();
            if (num > 2)
            {
                return;
            }
            MapControl associatedObject = base.AssociatedObject;
            if (num == 1)
            {
                Vector v = this.AssociatedObject.Dp2LP(e.DeltaManipulation.Translation);
                this.AssociatedObject.Move(v);
            }
            else
            {
                this.AssociatedObject.Zoom(center, e.DeltaManipulation.Scale);
            }
            e.Handled = true;
        }

        private void AssociatedObject_ManipulationStarting(object sender, System.Windows.Input.ManipulationStartingEventArgs e)
        {
            FrameworkElement fw = VisualTreeHelper.GetParent(this.AssociatedObject) as FrameworkElement;
            if(fw==null)
            {
                throw new Exception("null");
            }
            e.ManipulationContainer = fw;
            e.Mode = (ManipulationModes.TranslateX | ManipulationModes.TranslateY | ManipulationModes.Scale);
            Rect empty = Rect.Empty;
            foreach (IManipulator current in e.Manipulators)
            {
                Point p = current.GetPosition(this.AssociatedObject);
                p = this.AssociatedObject.Dp2LP(p);
                empty.Union(p);
            }
            this.center = new Point((empty.Left + empty.Right) / 2.0, (empty.Top + empty.Bottom) / 2.0);
            e.Handled = true;
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
