using gTopology;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineManipulator : GraphLineManipulator
    {
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            if (Start.X >= 0 && Start.Y >= 0 && End.X >= 0 && End.Y >= 0)
                editor.LinAddLine(Start, End, Tolerance);
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
