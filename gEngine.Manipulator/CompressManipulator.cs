using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class CompressManipulator : LayerManipulator
    {
        //public Rectangle TrackAdorner { get; set; }

        //protected override void OnAttached()
        //{
        //    base.OnAttached();
        //    if (this.AssociatedObject == null)
        //        return;

        //    MapControl mc = this.AssociatedObject.Owner;
        //    if (mc == null)
        //        return;


        //    Style style = new Style();
        //    style.Setters.Add(new Setter() { Property = Polyline.StrokeProperty, Value = new SolidColorBrush() { Color = Colors.Red } });
        //    style.Setters.Add(new Setter() { Property = Polyline.StrokeThicknessProperty, Value = 1.0 });
        //    style.Setters.Add(new Setter() { Property = Polyline.StrokeDashArrayProperty, Value = new DoubleCollection() { 2, 3 } });
        //    TrackAdorner = new Rectangle() { Style = style };
        //    mc.EditLayer.Children.Add(TrackAdorner);

        //    mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
        //    mc.MouseLeftButtonDown += Mc_MouseLeftButtonDown;
        //    mc.MouseRightButtonUp += Mc_MouseRightButtonUp;
        //    mc.MouseMove += Mc_MouseMove;
        //}

        //private void Mc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    MouseLeftButtonDown(sender, e);
        //}

        //protected virtual void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void Mc_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    MouseRightButtonUp(sender, e);
        //}

        //protected virtual void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    this.TrackAdorner.Points.Clear();
        //}

        //private void Mc_MouseMove(object sender, MouseEventArgs e)
        //{
        //    MouseMove(sender, e);
        //}

        //protected virtual void MouseMove(object sender, MouseEventArgs e)
        //{
        //    MapControl mc = this.AssociatedObject.Owner;
        //    int count = this.TrackAdorner.Points.Count;
        //    if (count > 1)
        //    {
        //        Point p = mc.Dp2LP(e.GetPosition(mc));
        //        this.TrackAdorner.Points[count - 1] = p;
        //    }
        //}

        //private void Mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    MouseLeftButtonUp(sender, e);
        //}

        //protected virtual void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    MapControl mc = this.AssociatedObject.Owner;

        //    if (this.TrackAdorner.Points.Count > 1)
        //    {
        //        this.TrackAdorner.Points.RemoveAt(this.TrackAdorner.Points.Count - 1);
        //    }
        //    Point p = mc.Dp2LP(e.GetPosition(mc));
        //    this.TrackAdorner.Points.Add(p);
        //    this.TrackAdorner.Points.Add(p);
        //}

        //protected override void OnDetaching()
        //{
        //    base.OnDetaching();
        //    MapControl mc = this.AssociatedObject.Owner;
        //    if (mc == null)
        //        return;
        //    mc.EditLayer.Children.Remove(TrackAdorner);
        //    mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
        //    mc.MouseLeftButtonDown -= Mc_MouseLeftButtonDown;
        //    mc.MouseRightButtonUp -= Mc_MouseRightButtonUp;
        //    mc.MouseMove -= Mc_MouseMove;
        //}
    }
}
