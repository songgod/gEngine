using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCloseCurveManipulator : GraphCurveManipulator
    {
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Track.Points.Count == 0)
                return;

            gTopology.Graph graph = Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            editor.LinAddCurve(TrackPoints, Tolerance, true);
            this.TrackAdorner.ClearPoint();
        }
    }
}
