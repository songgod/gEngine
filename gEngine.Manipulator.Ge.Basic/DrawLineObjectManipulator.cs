using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawLineObjectManipulator : LineManipulator
    {
        public DrawLineObjectManipulator()
        {
            LineStyle = new NormalLineStyle();
        }
        public LineStyle LineStyle { get; set; }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Line line = new Line()
            {
                Start = new Point(this.TrackAdorner.X1, this.TrackAdorner.Y1),
                End = new Point(this.TrackAdorner.X2, this.TrackAdorner.Y2),
                LinStyle = this.LineStyle
            };
            this.AssociatedObject.LayerContext.Objects.Add(line);
            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class DLMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawLineObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;

            DrawLineObjectManipulator dm = new DrawLineObjectManipulator();
            dm.LineStyle = style;
            return dm;
        }
    }
}
