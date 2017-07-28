using gEngine.Graph.Ge;
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
    public class DrawScaleRuleObjectManipulator: RectManipulator
    {
        public DrawScaleRuleObjectManipulator()
        {
            rectStyle = new LineStyle();
        }
        public LineStyle rectStyle { get; set; }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Graph.Ge.Basic.ScaleRule sca = new Graph.Ge.Basic.ScaleRule()
            {
                Unit = "米",
                ScaleNumber = 2,
                ScaleSpace = 10,
                ScaleHeight = 2,
                Top = this.location.Y,
                Left = this.location.X
            };
            this.AssociatedObject.LayerContext.Objects.Add(sca);
            base.MouseLeftButtonUp(sender, e);
            //Width = this.TrackAdorner.Width,
            //Height = this.TrackAdorner.Height,
        }
        protected override void MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
    public class DSMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawScaleRuleObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;

            DrawScaleRuleObjectManipulator dm = new DrawScaleRuleObjectManipulator();
            if (style != null) dm.rectStyle = style;
            return dm;
        }
    }
}
