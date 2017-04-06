using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCloseCurveManipulator : CurveManipulator
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
            editor.LinAddCurve(new PointList(TrackAdorner.Points.ToList()), graphutil.Tolerance, true);
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
