using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using GraphAlgo;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawPolyLineObjectManipulator : PolyLineManipulator
    {
        public DrawPolyLineObjectManipulator()
        {
            LineStyle = new NormalLineStyle();
        }
        public LineStyle LineStyle { get; set; }

        protected override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.TrackAdorner.Points.Count > 0)
            {
                PointCollection ps = this.TrackAdorner.Points;
                if (ps != null && ps.Count > 0)
                {
                    PolyLine polyline = new PolyLine()
                    {
                        Points = new PointCollection(ps),
                        LinStyle = this.LineStyle
                    };
                    this.AssociatedObject.LayerContext.Objects.Add(polyline);
                }
            }
            base.MouseRightButtonUp(sender, e);
        }
    }

    public class DPMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawPolyLineObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;

            DrawPolyLineObjectManipulator dm = new DrawPolyLineObjectManipulator();
            if (style != null) dm.LineStyle = style;
            return dm;
        }
    }

}
