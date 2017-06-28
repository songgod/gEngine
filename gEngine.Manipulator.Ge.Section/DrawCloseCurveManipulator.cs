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
        }

        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Points.Count == 0 || Graph==null)
                return;
            
            Topology editor = new Topology(Graph);
            editor.LinAddCurve(new PointList(TrackAdorner.Points.ToList()), Tolerance, true, LineType);
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
