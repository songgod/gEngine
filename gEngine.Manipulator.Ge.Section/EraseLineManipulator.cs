using gEngine.Graph.Ge.Section;
using gEngine.View;
using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Section
{
    public class EraseLineManipulator : CurveManipulator
    {
        private gTopology.Line SelectLine { get; set; }
        private Style OldTrackStyle { get; set; }
        protected PointList TrackPoints;
        public bool UseErasePart { get; set; }
        public GraphUtil GraphUtil { get; set; }
        public EraseLineManipulator()
        {
            UseErasePart = true;
            TrackPoints = new PointList();
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            GraphUtil = new GraphUtil(this.AssociatedObject);
        }

        protected override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(UseErasePart)
            {
                gTopology.Graph graph = GraphUtil.Graph;
                if (graph == null)
                    return;

                Topology editor = new Topology(graph);
                MapControl mc = this.AssociatedObject.Owner;
                Point pos = mc.Dp2LP(e.GetPosition(mc));
                gTopology.Line line = editor.LinHit(pos, GraphUtil.Tolerance);
                if(line!=null)
                {
                    SelectLine = line;
                    this.TrackAdorner.Points.Clear();
                    Point np = editor.LinNearestPoint(line, pos);
                    TrackPoints.Add(np);
                    this.TrackAdorner.Points.Add(np);
                    OldTrackStyle = TrackAdorner.Style;
                    Style newstyle = new Style();
                    newstyle.Setters.Add(new Setter() { Property=Polyline.StrokeProperty, Value=new SolidColorBrush() { Color = Colors.LightBlue } });
                    newstyle.Setters.Add(new Setter() { Property = Polyline.StrokeThicknessProperty, Value = 3.0 });
                    TrackAdorner.Style = newstyle;
                    return;
                }
            }
            base.MouseLeftButtonDown(sender, e);
        }
        protected override void MouseMove(object sender, MouseEventArgs e)
        {
            if (UseErasePart && SelectLine!=null && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Topology editor = new Topology(GraphUtil.Graph);
                MapControl mc = this.AssociatedObject.Owner;
                Point pos = mc.Dp2LP(e.GetPosition(mc));
                Point np = editor.LinNearestPoint(SelectLine, pos);
                TrackPoints.Add(np);
                TrackAdorner.Points.Add(np);
            }
            else
                base.MouseMove(sender, e);
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (UseErasePart && SelectLine!= null && TrackPoints.Count!=0)
            {
                Topology editor = new Topology(GraphUtil.Graph);
                editor.LinEraseSubLine(SelectLine, TrackPoints, GraphUtil.Tolerance);
                TrackAdorner.Style = OldTrackStyle;
                SelectLine = null;
            }
            else
            {
                gTopology.Graph graph = GraphUtil.Graph;
                if (graph == null)
                    return;
                Topology editor = new Topology(graph);
                editor.LinRemoveLine(new PointList(TrackAdorner.Points.ToList()));
            }
            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class ERLFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EraseLineManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new EraseLineManipulator();
        }
    }
}
