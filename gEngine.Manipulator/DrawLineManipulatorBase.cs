using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class DrawLineManipulatorBase : LayerManipulator
    {
        protected Line TrackAdorner { get; private set; }
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;

            TrackAdorner = new Line { Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 1, StrokeDashArray = new DoubleCollection() { 2, 3 } };
            mc.EditLayer.Children.Add(TrackAdorner);

            mc.MouseLeftButtonDown += Mc_MouseLeftButtonDown;
            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
            mc.MouseMove += Mc_MouseMove;
        }

        private void Mc_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(sender, e);
        }

        protected virtual void MouseMove(object sender, MouseEventArgs e)
        {
            MapControl mc = this.AssociatedObject.Owner;
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point p = mc.Dp2LP(e.GetPosition(mc));
                this.TrackAdorner.X2 = p.X;
                this.TrackAdorner.Y2 = p.Y;
            }
        }

        private void Mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseLeftButtonUp(sender,e);
        }

        protected virtual void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.X1 = this.TrackAdorner.X2;
            this.TrackAdorner.Y1 = this.TrackAdorner.Y2;
        }

        private void Mc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            MouseLeftButtonDown(sender, e);
        }

        protected virtual void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MapControl mc = this.AssociatedObject.Owner;
            Point p = mc.Dp2LP(e.GetPosition(mc));
            this.TrackAdorner.X1 = p.X;
            this.TrackAdorner.Y1 = p.Y;
            this.TrackAdorner.X2 = p.X;
            this.TrackAdorner.Y2 = p.Y;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Remove(TrackAdorner);
            mc.MouseLeftButtonDown -= Mc_MouseLeftButtonDown;
            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
            mc.MouseMove -= Mc_MouseMove;
        }
    }
}
