using gEngine.Graph.Ge.Section;
using gTopology;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveManipulator : CurveManipulator
    {
        protected GraphUtil graphutil = null;
        protected override void OnAttached()
        {
            base.OnAttached();
            graphutil = new GraphUtil(this.AssociatedObject);
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Points.Count == 0)
                return;

            gTopology.Graph graph = graphutil.Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            editor.LinAddCurve(new PointList(TrackAdorner.Points.ToList()), graphutil.Tolerance, false, LineType);
            base.MouseLeftButtonUp(sender, e);
        }
        public int LineType { get; set; }
    }

    public class DCMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCurveManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawCurveManipulator() { LineType = (int)param };
        }
    }
}
