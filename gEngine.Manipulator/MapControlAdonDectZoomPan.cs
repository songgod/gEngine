using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class MapControlAdonDectZoomPan : Behavior<MapControl>
    {
        private Point mousedown;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            MapControl mc = this.AssociatedObject as MapControl;
            if (mc == null)
                throw new Exception("AssociatedObject is not MapControl");

            mc.MouseDown += Mc_MouseDown;
            mc.MouseUp += Mc_MouseUp;
            mc.MouseMove += AssociatedObject_MouseMove;
            mc.MouseWheel += AssociatedObject_MouseWheel;
            mousedown = new Point(0, 0);
        }

        private void Mc_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                MapControl mc = this.AssociatedObject as MapControl;
                if (mc == null)
                    return;
            }
        }

        private void Mc_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                MapControl mc = this.AssociatedObject as MapControl;
                if (mc == null || mc.Items.IsEmpty)
                    return;
                LayerControl lc = mc.GetLayerControl(0);
                if (lc == null)
                    return;
                AdornerDecorator ad = mc.GetLayerControlAdornerDecorator(0);
                mousedown = e.GetPosition(ad);
            }
        }

        private void Zoom(MapControl elm, Point center, double delta)
        {
            for (int i = 0; i < elm.Items.Count; i++)
            {
                AdornerDecorator lc = elm.GetLayerControlAdornerDecorator(0);
                if (lc != null)
                {
                    Transform t = lc.RenderTransform;
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
                    Matrix m = mst * mt;
                    MatrixTransform ft = new MatrixTransform(m);
                    lc.RenderTransform = ft;
                }
            }
        }

        private void AssociatedObject_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            MapControl mc = this.AssociatedObject as MapControl;
            if (mc == null || mc.Items.IsEmpty)
                return;

            LayerControl lc = mc.GetLayerControl(0);
            if (lc == null)
                return;

            AdornerDecorator ad = mc.GetLayerControlAdornerDecorator(0);
            Point cp = e.GetPosition(ad);
            Zoom(mc, cp, e.Delta);
        }

        private void Pan(MapControl elm, Point cp)
        {
            for (int i = 0; i < elm.Items.Count; i++)
            {
                AdornerDecorator lc = elm.GetLayerControlAdornerDecorator(i);
                if (lc != null)
                {
                    Transform t = lc.RenderTransform;

                    TranslateTransform tt = new TranslateTransform() { X = cp.X - mousedown.X, Y = cp.Y - mousedown.Y };
                    Matrix mt = t.Value;
                    Matrix mtt = tt.Value;
                    Matrix m = mtt * mt;
                    MatrixTransform ft = new MatrixTransform(m);
                    lc.RenderTransform = ft;
                }
            }
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                MapControl mc = this.AssociatedObject as MapControl;
                if (mc == null || mc.Items.IsEmpty)
                    return;

                LayerControl lc = mc.GetLayerControl(0);
                if (lc == null)
                    return;
                AdornerDecorator ad = mc.GetLayerControlAdornerDecorator(0);
                Point cp = e.GetPosition(ad);
                Pan(mc, cp);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            MapControl mc = this.AssociatedObject as MapControl;
            if (mc == null)
                return;
            mc.MouseDown += Mc_MouseDown;
            mc.MouseUp += Mc_MouseUp;
            mc.MouseMove -= AssociatedObject_MouseMove;
            mc.MouseWheel -= AssociatedObject_MouseWheel;
        }
    }
}
