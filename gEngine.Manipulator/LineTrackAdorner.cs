using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class LineTrackAdorner : Adorner
    {
        public System.Windows.Shapes.Line Track
        {
            get; set;
        }

        public LineTrackAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Track = new System.Windows.Shapes.Line { Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 1, StrokeDashArray = new DoubleCollection() { 2, 3 } };
            this.Track.MouseLeftButtonUp += Track_MouseLeftButtonUp;
            this.AddVisualChild(this.Track);
        }

        private void Track_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        public Point Start
        {
            get
            {
                return new Point() { X = this.Track.X1, Y = this.Track.Y1 };
            }
            set
            {
                this.Track.X1 = value.X;
                this.Track.Y1 = value.Y;
            }
        }

        public Point End
        {
            get
            {
                return new Point() { X = this.Track.X2, Y = this.Track.Y2 };
            }
            set
            {
                this.Track.X2 = value.X;
                this.Track.Y2 = value.Y;
            }
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
