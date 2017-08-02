using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawBoundaryObjectManipulator: PolyLineManipulator
    {

        public DrawBoundaryObjectManipulator()
        {
            LineStyle = new LineStyle();
        }
        public LineStyle LineStyle { get; set; }

        protected override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.TrackAdorner.Points.Count > 0)
            {
                PointCollection ps = this.TrackAdorner.Points;

                if (ps != null && ps.Count > 0)
                {
                    
                    Graph.Ge.Basic.Boundary bou = new Graph.Ge.Basic.Boundary()
                    {
                        Points = new PointCollection(ps),
                        Stroke = Colors.Black,
                        StrokeThickness = 1
                    };
                    this.AssociatedObject.LayerContext.Objects.Add(bou);
                    
                }
            }
            base.MouseRightButtonUp(sender, e);
        }
    }
    public class DBOMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawBoundaryObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;

            DrawBoundaryObjectManipulator dm = new DrawBoundaryObjectManipulator();
            if (style != null) dm.LineStyle = style;
            return dm;
        }
    }
}
