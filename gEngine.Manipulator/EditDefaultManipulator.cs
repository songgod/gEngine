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
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush() { Color = Colors.Blue } });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeDashArrayProperty, Value = new DoubleCollection() { 2, 3 } });


            TrackAdorner = new Rectangle() { Style = style };

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
