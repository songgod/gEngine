using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class PolyTrackAdorner : CurveTrackAdorner
    {
        public PolyTrackAdorner(UIElement adornedElement) : base(adornedElement)
        {

        }

        public void MoveLastPoint(Point p)
        {
            if (this.Track.Points.Count == 0)
                return;

            this.Track.Points[this.Track.Points.Count - 1] = p;
        }

        public void RemoveLastPoint()
        {
            if (this.Track.Points.Count == 0)
                return;
            this.Track.Points.RemoveAt(this.Track.Points.Count - 1);
        }
    }
}
