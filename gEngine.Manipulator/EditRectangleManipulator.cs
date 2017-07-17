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
    public class EditRectangleManipulator : ObjectManipulator
    {
        public Rectangle TrackAdorner { get; set; }
        public Rectangle TrackAdorner1 { get; set; }
        public Rectangle TrackAdorner2 { get; set; }
        public Rectangle TrackAdorner3 { get; set; }
        public Rectangle TrackAdorner4 { get; set; }


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
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush(Color.FromRgb(120, 162, 239)) });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.White });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });
            TrackAdorner = new Rectangle();
            TrackAdorner1 = new Rectangle() { Style = style };
            TrackAdorner2 = new Rectangle() { Style = style };
            TrackAdorner3 = new Rectangle() { Style = style };
            TrackAdorner4 = new Rectangle() { Style = style };

            Path path = FindChild.FindVisualChild<Path>(oc, "pathLogsBorder");       
            Rect rect = VisualTreeHelper.GetDescendantBounds(path);

            rect.X = rect.X +((gEngine.Graph.Ge.Column.Well)oc.DataContext).Location;
            rect.Y = rect.Y ;
            Canvas.SetLeft(TrackAdorner1, rect.Left - TrackAdorner1.Width);
            Canvas.SetTop(TrackAdorner1, rect.Top - TrackAdorner1.Height);
            Canvas.SetLeft(TrackAdorner2, rect.Right);
            Canvas.SetTop(TrackAdorner2, rect.Top - TrackAdorner2.Height);
            Canvas.SetLeft(TrackAdorner3, rect.Left - TrackAdorner3.Width );
            Canvas.SetTop(TrackAdorner3, rect.Bottom);
            Canvas.SetLeft(TrackAdorner4, rect.Right );
            Canvas.SetTop(TrackAdorner4, rect.Bottom );

            Canvas.SetLeft(TrackAdorner, rect.Left);
            Canvas.SetTop(TrackAdorner, rect.Top);
            TrackAdorner.StrokeThickness = 1;
            TrackAdorner.Stroke = new SolidColorBrush(Color.FromRgb(120, 162, 239));
            TrackAdorner.Width = rect.Width;
            TrackAdorner.Height = rect.Height;
            
            mc.EditLayer.Children.Add(TrackAdorner);
            mc.EditLayer.Children.Add(TrackAdorner1);
            mc.EditLayer.Children.Add(TrackAdorner2);
            mc.EditLayer.Children.Add(TrackAdorner3);
            mc.EditLayer.Children.Add(TrackAdorner4);
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
