using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class ZoomPan : Behavior<FrameworkElement>
    {
        private bool bmousedown;
        private Point mousedown;
        UIElement parent;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            if (this.AssociatedObject.Parent == null)
                throw new Exception("AssociatedObject's parent is null");
            parent = this.AssociatedObject.Parent as UIElement;
            if (parent == null)
                throw new Exception("AssociatedObject's parent is not UIElement");

            parent.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            parent.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            parent.MouseMove += AssociatedObject_MouseMove;
            parent.MouseWheel += AssociatedObject_MouseWheel;
            bmousedown = false;
            mousedown = new Point(0, 0);
        }

        private void Zoom(UIElement elm, Point center, double delta)
        {
            Transform t = elm.RenderTransform;
            Point cp = center;
            double sx = 1;
            double sy = 1;
            if (delta > 0)
            {
                sx = 1.1;
                sy = 1.1;
            }
            else
            {
                sx = 0.9;
                sy = 0.9;
            }

            ScaleTransform st = new ScaleTransform() { CenterX = cp.X, CenterY = cp.Y, ScaleX = sx, ScaleY = sy };
            Matrix mt = t.Value;
            Matrix mst = st.Value;
            Matrix m = mst* mt;
            MatrixTransform ft = new MatrixTransform(m);
            elm.RenderTransform = ft;
        }

        private void AssociatedObject_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Point cp = e.GetPosition(this.AssociatedObject);
            Zoom(AssociatedObject, cp, e.Delta);
        }

        private void Pan(UIElement elm, Point cp)
        {
            Transform t = elm.RenderTransform;

            TranslateTransform tt = new TranslateTransform() { X = cp.X - mousedown.X, Y = cp.Y - mousedown.Y };
            Matrix mt = t.Value;
            Matrix mtt = tt.Value;
            Matrix m = mtt * mt;
            MatrixTransform ft = new MatrixTransform(m);
            elm.RenderTransform = ft;
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (bmousedown==true)
            {
                Point cp = e.GetPosition(this.AssociatedObject);
                Pan(AssociatedObject, cp);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            parent.ReleaseMouseCapture();
            bmousedown = false;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mousedown = e.GetPosition(AssociatedObject);
            parent.CaptureMouse();
            bmousedown = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            parent.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            parent.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
            parent.MouseMove -= AssociatedObject_MouseMove;
            parent.MouseWheel -= AssociatedObject_MouseWheel;
        }
    }
}
