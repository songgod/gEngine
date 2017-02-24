using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class CurveTrackAdorner : Adorner
    {
        public Polyline Track
        {
            get; set;
        }

        public CurveTrackAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Polyline.StrokeProperty, Value = new SolidColorBrush() { Color = Colors.Red } });
            style.Setters.Add(new Setter() { Property = Polyline.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Polyline.StrokeDashArrayProperty, Value = new DoubleCollection() { 2, 3 } });
            this.Track = new Polyline() { Style = style};
            this.Track.MouseLeftButtonUp += Track_MouseLeftButtonUp;
            this.AddVisualChild(this.Track);
        }

        private void Track_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        public void AddPoint(Point p)
        {
            this.Track.Points.Add(p);
        }

        public void ClearPoint()
        {
            this.Track.Points.Clear();
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return Track;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Track.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);
        }
    }
}
