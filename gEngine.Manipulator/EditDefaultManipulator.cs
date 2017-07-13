using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
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
    public class EditDefaultManipulator : ObjectManipulator
    {
        public Rectangle TrackAdorner { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;

            MapControl mc = lc.Owner;
            if (mc == null)
                return;

            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush() { Color = Colors.Navy } });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeDashArrayProperty, Value = new DoubleCollection() { 2.5, 2.5 } });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.Transparent });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            TrackAdorner = new Rectangle() { Style = style };

            Rect rect = VisualTreeHelper.GetDescendantBounds(oc);
            Canvas.SetLeft(TrackAdorner, rect.X);
            Canvas.SetTop(TrackAdorner, rect.Y);
            TrackAdorner.Height = rect.Height;
            TrackAdorner.Width = rect.Width;

            mc.EditLayer.Children.Add(TrackAdorner);
            //mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
            //mc.MouseLeftButtonDown += Mc_MouseLeftButtonDown;
            //mc.MouseRightButtonUp += Mc_MouseRightButtonUp;
            //mc.MouseMove += Mc_MouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Remove(TrackAdorner);
            //mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
            //mc.MouseLeftButtonDown -= Mc_MouseLeftButtonDown;
            //mc.MouseRightButtonUp -= Mc_MouseRightButtonUp;
            //mc.MouseMove -= Mc_MouseMove;
        }
    }

    
}
