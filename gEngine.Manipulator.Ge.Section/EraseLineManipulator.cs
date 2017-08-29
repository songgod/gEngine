using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using gEngine.View;
using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Section
{
    public class EraseLineManipulator : DrawCurveManipulator
    {
        private gTopology.Line SelectLine { get; set; }
        private Style OldTrackStyle { get; set; }
        protected PointList TrackPoints;
        public bool UseErasePart { get; set; }
        public EraseLineManipulator()
        {
            UseErasePart = true;
            TrackPoints = new PointList();
        }
        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(UseErasePart)
            {
                MapControl mc = this.AssociatedObject.Owner;
                Point pos = mc.Dp2LP(e.GetPosition(mc));
                SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
                gTopology.Line line = editor.HitLine(pos, GraphUtil.Tolerance);
                if(line!=null)
                {
                    SelectLine = line;
                    this.TrackAdorner.Points.Clear();
                    Point np = editor.NearestPoint(line, pos);
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
                SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
                MapControl mc = this.AssociatedObject.Owner;
                Point pos = mc.Dp2LP(e.GetPosition(mc));
                Point np = editor.NearestPoint(SelectLine, pos);
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
                SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
                editor.EraseSubLine(SelectLine, TrackPoints, GraphUtil.Tolerance);
                TrackAdorner.Style = OldTrackStyle;
                SelectLine = null;
            }
            else
            {
                SectionLayerEdit editor = new SectionLayerEdit(GraphUtil.SectionLayer);
                editor.EraseLine(new PointList(TrackAdorner.Points.ToList()),GraphUtil.Tolerance);
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
