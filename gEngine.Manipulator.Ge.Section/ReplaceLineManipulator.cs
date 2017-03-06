using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class ReplaceLineManipulator : GraphCurveManipulator
    {
        private gTopology.Line SelectLine { get; set; }
        public override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            Point pos = e.GetPosition(GraphContainer);
            gTopology.Line line = editor.LinHit(pos, Tolerance);
            if (line != null)
            {
                SelectLine = line;
            }
            base.MouseLeftButtonDown(sender, e);
        }
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(SelectLine!=null)
            {
                Topology editor = new Topology(Graph);
                editor.LinReplaceSubLine(SelectLine, TrackPoints, Tolerance);
            }
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
