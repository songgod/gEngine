using gTopology;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineManipulator : GraphLineManipulator
    {
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Graph graph = Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            editor.LinAddLine(TrackAdorner.Start, TrackAdorner.End, Tolerance);
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
