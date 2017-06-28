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
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Graph == null)
                return;

            Topology editor = new Topology(Graph);
            Point start = new Point() { X = this.TrackAdorner.X1, Y = this.TrackAdorner.Y1 };
            Point end = new Point() { X = this.TrackAdorner.X2, Y = this.TrackAdorner.Y2 };
            editor.LinAddLine(start, end, Tolerance, LineType);

            base.MouseLeftButtonUp(sender, e);
        }

        public int LineType { get; set; }

        public gTopology.Graph Graph { get; set; }

        public double Tolerance
        {
            get
            {
                return CalcTolerance.GetTolerance(this.AssociatedObject);
            }
        }
    }
}
