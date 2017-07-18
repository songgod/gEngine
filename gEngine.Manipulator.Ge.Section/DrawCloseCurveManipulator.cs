using gEngine.Graph.Ge.Section;
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
        protected override void OnAttached()
        {
            base.OnAttached();
            GraphUtil = new GraphUtil(this.AssociatedObject);
        }

        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Points.Count == 0 || GraphUtil.Graph == null)
                return;
            
            Topology editor = new Topology(GraphUtil.Graph);
            editor.LinAddCurve(new PointList(TrackAdorner.Points.ToList()), GraphUtil.Tolerance, true, LineType);
            base.MouseLeftButtonUp(sender, e);
        }

        public int LineType { get; set; }
        public GraphUtil GraphUtil { get; set; }
    }
}
