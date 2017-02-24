using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace gEngine.Manipulator
{
    public class CurveManipulator : ManipulatorBase
    {
        protected CurveTrackAdorner TrackAdorner{ get; private set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

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
            this.TrackAdorner.ClearPoint();
            Point p = e.GetPosition(this.AssociatedObject);
            this.TrackAdorner.AddPoint(p);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this.AssociatedObject);
                this.TrackAdorner.AddPoint(p);
            }
        }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.ClearPoint();
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
