using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveFaultManipulator : CurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Points.Count == 0)
                return;
            SectionLayer layer = this.AssociatedObject.LayerContext as SectionLayer;
            if(layer.StratumObject!=null)
            {
                Topology editor = new Topology(layer.StratumObject.TopGraph);
                editor.LinAddCurve(new PointList(TrackAdorner.Points.ToList()), Tolerance, false, (int)SectionLayerEdit.SectionLineType.Stratum);
            }
            if (layer.SandObject != null)
            {
                Topology editor = new Topology(layer.SandObject.TopGraph);
                editor.LinAddCurve(new PointList(TrackAdorner.Points.ToList()), Tolerance, false, (int)SectionLayerEdit.SectionLineType.Sand);
            }
            base.MouseLeftButtonUp(sender, e);
        }
        public double Tolerance
        {
            get
            {
                return CalcTolerance.GetTolerance(this.AssociatedObject);
            }
        }
    }

    public class DrawCurveFaultManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCurveFaultManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawCurveFaultManipulator();
        }
    }
}
