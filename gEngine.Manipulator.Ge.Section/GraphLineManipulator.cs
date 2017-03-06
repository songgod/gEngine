using gEngine.Graph.Ge.Section;
using gEngine.View;
using gTopology;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphLineManipulator : GraphManipulatorBase
    {
        protected LineTrackAdorner TrackAdorner { get; private set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            TrackAdorner = new LineTrackAdorner(this.AssociatedObject);
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
            Point p = e.GetPosition(this.AssociatedObject);
            this.TrackAdorner.Start = p;
            this.TrackAdorner.End = p;
            Start = e.GetPosition(GraphContainer);
            End = e.GetPosition(GraphContainer);
        }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.Start = this.TrackAdorner.End;
            Start = End;
            base.MouseLeftButtonUp(sender, e);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this.AssociatedObject);
                this.TrackAdorner.End = p;
                End = e.GetPosition(GraphContainer);
            }
        }
    }
}
