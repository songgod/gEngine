using gEngine.View;
using GraphAlgo;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class DrawBezierManipulatorBase : DrawCurveManipulatorBase
    {
        public DrawBezierManipulatorBase()
        {
        }
        public double GetTolerance()
        {
            int pixelsize = 5;
            MapControl mc = this.AssociatedObject.Owner;
            double x = mc.Dp2LP(pixelsize);
            double t = Math.Max(2.0, x);
            return t;
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.TrackAdorner.Points.Count>0)
            {
                PointCollection ps = BezierFitCubic.FitCubic(this.TrackAdorner.Points, GetTolerance());
                if (ps!=null && ps.Count>0)
                {
                    ProcessBeizer(ps);
                }
            }
            base.MouseLeftButtonUp(sender, e);
        }

        virtual public void ProcessBeizer(PointCollection ps)
        {
            throw new NotImplementedException();
        }
    }
}
