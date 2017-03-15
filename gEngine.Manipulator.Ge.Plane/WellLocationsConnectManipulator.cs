using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Plane;
using gEngine.Manipulator;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Plane
{
    public class WellLocationsConnectManipulator : PolyLineManipulator
    {
        public HashSet<string> SelectWellLocations { get; set; }
        public delegate void FinishSelectWellLocations(HashSet<string> names);
        public event FinishSelectWellLocations OnFinishSelect;

        public bool IsStopMove { get; set; }
        public HashSet<Point> wellPointList { get; set; }


        public WellLocationsConnectManipulator()
        {
            SelectWellLocations = new HashSet<string>();
            wellPointList = new HashSet<Point>();
        }


        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsStopMove)
                return;
            LayerControl lc = this.AssociatedObject as LayerControl;

            Point p = e.GetPosition(lc);
            HitTestResult hr = VisualTreeHelper.HitTest(lc, p);
            if (hr == null || hr.VisualHit == null)
                return;

            Shape sp = hr.VisualHit as Shape;
            if (sp == null)
                return;
            WellLocation wl = sp.DataContext as WellLocation;

            if (wl == null)
                return;

            SelectWellLocations.Add(wl.WellNum);

            WellPosition = new Point(wl.X, wl.Y);

            wellPointList.Add(p);


            base.MouseLeftButtonUp(sender, e);
        }

        public override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnFinishSelect != null && SelectWellLocations.Count != 0)
            {
                OnFinishSelect.Invoke(SelectWellLocations);

                Point lastPoint = this.TrackAdorner.Track.Points[this.TrackAdorner.Track.Points.Count - 1];
                if (!wellPointList.Contains(lastPoint))
                {
                    this.TrackAdorner.Track.Points.Remove(lastPoint);
                }
                IsStopMove = true;
            }

            base.MouseRightButtonUp(sender, e);
        }


        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (IsStopMove)
                return;
            base.MouseMove(sender, e);

        }
    }
}
