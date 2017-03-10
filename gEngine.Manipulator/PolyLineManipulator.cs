using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace gEngine.Manipulator
{
    public class PolyLineManipulator : ManipulatorBase
    {
        protected PolyTrackAdorner TrackAdorner { get; set; }
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            TrackAdorner = new PolyTrackAdorner(this.AssociatedObject);
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            adornerLayer.Add(TrackAdorner);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            adornerLayer.Remove(this.TrackAdorner);
        }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.TrackAdorner.RemoveLastPoint();
            this.TrackAdorner.AddPoint(e.GetPosition(this.AssociatedObject));
            this.TrackAdorner.AddPoint(e.GetPosition(this.AssociatedObject));
            base.MouseLeftButtonUp(sender, e);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            this.TrackAdorner.MoveLastPoint(e.GetPosition(this.AssociatedObject));
        }
    }
}
