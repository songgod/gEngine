using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawTrendLineManipulator : DrawBezierManipulatorBase
    {
        public DrawTrendLineManipulator()
        {
            LineStyle = new LineStyle();
        }
        public LineStyle LineStyle { get; set; }

        public override void ProcessBeizer(PointCollection ps)
        {
            TrendLine trendLine = new TrendLine()
            {
                Points = new PointCollection(ps),
                LineStyle = this.LineStyle.DeepClone()
            };
            this.AssociatedObject.LayerContext.Objects.Add(trendLine);
        }
    }

    public class DTLMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawTrendLineManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            LineStyle style = param as LineStyle;
            DrawTrendLineManipulator dm = new DrawTrendLineManipulator();
            if (style != null) dm.LineStyle = style;
            return dm;
        }
    }
}
