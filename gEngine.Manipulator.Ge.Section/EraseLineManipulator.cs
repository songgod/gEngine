using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Section
{
    public class EraseLineManipulator : GraphCurveManipulator
    {
        private gTopology.Line SelectLine { get; set; }
        private Style OldTrackStyle { get; set; }

        public EraseLineManipulator()
        {
            UseErasePart = true;
        }
        public bool UseErasePart { get; set; }
        public override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(UseErasePart)
            {
                Graph graph = Graph;
                if (graph == null)
                    return;

                Topology editor = new Topology(graph);
                Point pos = e.GetPosition(this.AssociatedObject);
                gTopology.Line line = editor.LinHit(pos, Tolerance);
                if(line!=null)
                {
                    SelectLine = line;
                    this.TrackAdorner.ClearPoint();
                    Point np = editor.LinNearestPoint(line, pos);
                    TrackAdorner.Track.Points.Add(np);
                    OldTrackStyle = TrackAdorner.Track.Style;
                    Style newstyle = new Style();
                    newstyle.Setters.Add(new Setter() { Property=Polyline.StrokeProperty, Value=new SolidColorBrush() { Color = Colors.LightBlue } });
                    newstyle.Setters.Add(new Setter() { Property = Polyline.StrokeThicknessProperty, Value = 3.0 });
                    TrackAdorner.Track.Style = newstyle;
                    return;
                }
            }
            base.MouseLeftButtonDown(sender, e);
        }
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (UseErasePart && SelectLine!=null && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Topology editor = new Topology(Graph);
                Point pos = e.GetPosition(this.AssociatedObject);
                Point np = editor.LinNearestPoint(SelectLine, pos);
                TrackAdorner.Track.Points.Add(np);
            }
            else
                base.MouseMove(sender, e);
        }
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Track.Points.Count == 0)
                return;

            if (UseErasePart && SelectLine!= null)
            {
                Topology editor = new Topology(Graph);
                Point pos = e.GetPosition(this.AssociatedObject);
                editor.LinEraseSubLine(SelectLine, new PointList(this.TrackAdorner.Track.Points.ToList()),Tolerance);
                TrackAdorner.Track.Style = OldTrackStyle;
                SelectLine = null;
            }
            else
            {
                Graph graph = Graph;
                if (graph == null)
                    return;
                Topology editor = new Topology(graph);
                editor.LinRemoveLine(new PointList(this.TrackAdorner.Track.Points.ToList()));
            }
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
