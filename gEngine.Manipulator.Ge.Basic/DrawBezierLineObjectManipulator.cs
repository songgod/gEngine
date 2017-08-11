using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.Util;
using gEngine.View;
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
    public class DrawBezierLineObjectManipulator : DrawBezierManipulatorBase
    {
        public DrawBezierLineObjectManipulator()
        {
            LineStyle = new LineStyle();
        }
        public LineStyle LineStyle { get; set; }

        public override void ProcessBeizer(PointCollection ps)
        {
            BeizerLine beizerline = new BeizerLine()
            {
                Points = new PointCollection(ps),
                LinStyle = this.LineStyle.DeepClone()
            };
            this.AssociatedObject.LayerContext.Objects.Add(beizerline);
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
