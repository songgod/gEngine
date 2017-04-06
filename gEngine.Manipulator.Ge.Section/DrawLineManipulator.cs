using gTopology;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineManipulator : LineManipulator
    {
        protected GraphUtil graphutil = null;
        protected override void OnAttached()
        {
            base.OnAttached();
            graphutil = new GraphUtil(this.AssociatedObject);
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = graphutil.Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            Point start = new Point() { X = this.TrackAdorner.X1, Y = this.TrackAdorner.Y1 };
            Point end = new Point() { X = this.TrackAdorner.X2, Y = this.TrackAdorner.Y2 };
            editor.LinAddLine(start, end, graphutil.Tolerance);

            base.MouseLeftButtonUp(sender, e);
        }
    }
}
