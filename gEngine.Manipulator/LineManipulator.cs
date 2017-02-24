using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace gEngine.Manipulator
{
    public class LineManipulator : ManipulatorBase
    {
        protected LineTrackAdorner TrackAdorner { get; private set; }
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
            Point p = e.GetPosition(this.AssociatedObject);
            this.TrackAdorner.Start = p;
            this.TrackAdorner.End = p;
        }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.Start = this.TrackAdorner.End;
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this.AssociatedObject);
                this.TrackAdorner.End = p;
            }
        }
    }
}
