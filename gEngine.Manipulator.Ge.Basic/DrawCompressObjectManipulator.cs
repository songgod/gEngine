using gEngine.Graph.Ge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawCompressObjectManipulator: CompressManipulator
    {
        public DrawCompressObjectManipulator()
        {
            LineStyle = new NormalLineStyle();
        }
        public LineStyle LineStyle { get; set; }

        protected override void MouseMove(object sender, MouseEventArgs e)
        {

            //if (this.TrackAdorner.Points.Count > 0)
            //{
            //    PointCollection ps = this.TrackAdorner.Points;
            //    if (ps != null && ps.Count > 0)
            //    {
            //        PolyLine polyline = new PolyLine()
            //        {
            //            Points = new PointCollection(ps),
            //            LinStyle = this.LineStyle
            //        };
            //        this.AssociatedObject.LayerContext.Objects.Add(polyline);
            //    }
            //}

        }
    }
    public class DCMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCompressObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;

            DrawCompressObjectManipulator dm = new DrawCompressObjectManipulator();
            if (style != null) dm.LineStyle = style;
            return dm;
        }
    }
}
