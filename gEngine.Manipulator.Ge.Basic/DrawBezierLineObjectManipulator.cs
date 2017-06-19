using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.Util;
using GraphAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawBezierLineObjectManipulator : CurveManipulator
    {
        public DrawBezierLineObjectManipulator()
        {
            LineStyle = new NormalLineStyle();
        }
        public LineStyle LineStyle { get; set; }
        public double GetTolerance()
        {
            int pixelsize = 5;
            Vector v = new Vector(pixelsize, pixelsize);
            Transform tf = this.AssociatedObject.RenderTransform;
            Matrix m = Matrix.Multiply(tf.Value, Matrix.Identity);
            m.Invert();
            Vector tv = m.Transform(v);
            double t = Math.Max(2.0, Math.Max(tv.X, tv.Y));
            return t;
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.TrackAdorner.Points.Count>0)
            {
                PointCollection ps = BezierFitCubic.FitCubic(this.TrackAdorner.Points, GetTolerance());
                if (ps!=null && ps.Count>0)
                {
                    BeizerLine beizerline = new BeizerLine()
                    {
                        Points = new PointCollection(ps),
                        LinStyle = this.LineStyle
                    };
                    this.AssociatedObject.LayerContext.Objects.Add(beizerline);
                }
            }
            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class DBMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawBezierLineObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;
            DrawBezierLineObjectManipulator dm = new DrawBezierLineObjectManipulator();
            if(style!=null) dm.LineStyle = style;
            return dm;
        }
    }
}
