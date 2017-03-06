using gEngine.Graph.Ge.Section;
using gEngine.View;
using gTopology;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System;
using System.Windows.Documents;
using System.Windows;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphCurveManipulator : GraphManipulatorBase
    {
        protected CurveTrackAdorner TrackAdorner { get; private set; }
        public PointList TrackPoints { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            TrackPoints = new PointList();
            TrackAdorner = new CurveTrackAdorner(this.AssociatedObject);
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            adornerLayer.Add(TrackAdorner);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            adornerLayer.Remove(this.TrackAdorner);
        }

        public override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.MouseLeftButtonDown(sender, e);
            this.TrackAdorner.ClearPoint();
            Point p = e.GetPosition(this.AssociatedObject);
            this.TrackAdorner.AddPoint(p);
            TrackPoints.Clear();
            TrackPoints.Add(e.GetPosition(GraphContainer)); 
        }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.ClearPoint();
            TrackPoints.Clear();
            base.MouseLeftButtonUp(sender, e);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                TrackPoints.Add(e.GetPosition(GraphContainer));
                Point p = e.GetPosition(this.AssociatedObject);
                this.TrackAdorner.AddPoint(p);
            }
        }
    }
}
