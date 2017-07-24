using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Basic
{
    public class LineLayerCreator
    {
        public Layer Create()
        {
            Layer layer = new Layer() { Type = "Line" };
            Line line = new Line()
            {
                Start = new System.Windows.Point(0, 0),
                End = new System.Windows.Point(200,200)
                //LinStyle = this.LineStyle
            };
            layer.Objects.Add(line);
            return layer;
        }
    }
}
