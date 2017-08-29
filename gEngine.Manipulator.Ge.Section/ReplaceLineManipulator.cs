using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using gEngine.View;
using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class ReplaceLineManipulator : DrawCurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }

        private gTopology.Line SelectLine { get; set; }
        protected override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = GraphUtil.Graph;
            if (graph == null)
                return;

            SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
            MapControl mc = this.AssociatedObject.Owner;
            Point pos = mc.Dp2LP(e.GetPosition(mc));
            gTopology.Line line = editor.HitLine(pos, GraphUtil.Tolerance);
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
                SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
                editor.ReplaceLine(SelectLine, new PointList(this.TrackAdorner.Points.ToList()), GraphUtil.Tolerance);
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
