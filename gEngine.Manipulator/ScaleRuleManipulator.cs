using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class ScaleRuleManipulator: LayerManipulator
    {
        public Rectangle TrackAdorner { get; set; }
        public Point location;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;


            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush() { Color = Colors.Red } });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeDashArrayProperty, Value = new DoubleCollection() { 2, 3 } });
            TrackAdorner = new Rectangle() { Style = style };
            //TrackAdorner = new Rectangle { Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 1, StrokeDashArray = new DoubleCollection() { 2, 3 } };
            mc.EditLayer.Children.Add(TrackAdorner);

            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
            mc.MouseLeftButtonDown += Mc_MouseLeftButtonDown;
            mc.MouseRightButtonUp += Mc_MouseRightButtonUp;
            mc.MouseMove += Mc_MouseMove;
        }

        private void Mc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseLeftButtonDown(sender, e);
        }

        protected virtual void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MapControl mc = this.AssociatedObject.Owner;
            this.location = e.GetPosition(mc);
        }

        private void Mc_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseRightButtonUp(sender, e);
        }

        protected virtual void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner = null;
        }

        private void Mc_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(sender, e);
        }

        protected virtual void MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseLeftButtonUp(sender, e);
        }

        protected virtual void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Remove(TrackAdorner);
            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
            mc.MouseLeftButtonDown -= Mc_MouseLeftButtonDown;
            mc.MouseRightButtonUp -= Mc_MouseRightButtonUp;
            mc.MouseMove -= Mc_MouseMove;
        }
    }
}
