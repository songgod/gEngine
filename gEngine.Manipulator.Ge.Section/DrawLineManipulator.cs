using gEngine.Graph.Ge.Section;
using gTopology;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineManipulator : LineManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            GraphUtil = new GraphUtil(this.AssociatedObject);
        }

        public GraphUtil GraphUtil { get; set; }

        public Point Start
        {
            get
            {
                return new Point() { X = this.TrackAdorner.X1, Y = this.TrackAdorner.Y1 };
            }
        }

        public Point End
        {
            get
            {
                return new Point() { X = this.TrackAdorner.X2, Y = this.TrackAdorner.Y2 };
            }
        }
    }
}
