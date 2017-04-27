using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class ReplaceLineManipulator : CurveManipulator
    {
        protected GraphUtil graphutil = null;
        protected override void OnAttached()
        {
            base.OnAttached();
            graphutil = new GraphUtil(this.AssociatedObject);
        }
        private gTopology.Line SelectLine { get; set; }
        protected override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = graphutil.Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            Point pos = e.GetPosition(graphutil.GraphContainer);
            gTopology.Line line = editor.LinHit(pos, graphutil.Tolerance);
            if (line != null)
            {
                SelectLine = line;
            }
            base.MouseLeftButtonDown(sender, e);
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(SelectLine!=null)
            {
                Topology editor = new Topology(graphutil.Graph);
                editor.LinReplaceSubLine(SelectLine, new PointList(this.TrackAdorner.Points.ToList()), graphutil.Tolerance);
            }
            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class RLFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "ReplaceLineManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new ReplaceLineManipulator();
        }
    }
}
