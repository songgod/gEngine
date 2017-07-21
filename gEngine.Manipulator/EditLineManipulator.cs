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
    public class EditLineManipulator : ObjectManipulator
    {
      
        public Rectangle TrackAdorner1 { get; set; }
        public Rectangle TrackAdorner2 { get; set; }
      


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
            Point p1 = ((gEngine.Graph.Ge.Basic.Line)oc.DataContext).Start;
            Point p2 = ((gEngine.Graph.Ge.Basic.Line)oc.DataContext).End;



            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush(Color.FromRgb(120, 162, 239)) });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.White });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });
          
            TrackAdorner1 = new Rectangle() { Style = style };
            TrackAdorner2 = new Rectangle() { Style = style };
        
            Canvas.SetLeft(TrackAdorner1, p1.X - TrackAdorner1.Width/2);
            Canvas.SetTop(TrackAdorner1, p1.Y - TrackAdorner1.Height/2);
            Canvas.SetLeft(TrackAdorner2, p2.X - TrackAdorner2.Width/2);
            Canvas.SetTop(TrackAdorner2, p2.Y - TrackAdorner2.Height/2);

            mc.EditLayer.Children.Add(TrackAdorner1);
            mc.EditLayer.Children.Add(TrackAdorner2);
            
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();

        }
    }
}
