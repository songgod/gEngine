using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Util.Ge.Basic
{
    public class LineLayerCreator
    {
        public Layer Create()
        {
            NormalLineStyle ls = new NormalLineStyle();
            ls.Width = 2;
            ls.Stroke = Colors.Red;
            DoubleCollection dc = new DoubleCollection();
            dc.Add(1);
            dc.Add(0);
            ls.StrokeDashArray = dc;
            Layer layer = new Layer() { Type = "Line" };
            Line line = new Line()
            {
                Start = new System.Windows.Point(0, 0),
                End = new System.Windows.Point(200,200),
                LinStyle = ls
            };
            layer.Objects.Add(line);

            NormalLineStyle ls1 = new NormalLineStyle();
            ls1.Width = 1;
            ls1.Stroke = Colors.Black;
            ls1.StrokeDashArray = dc;
            Line line1 = new Line()
            {
                Start = new System.Windows.Point(10, 210),
                End = new System.Windows.Point(280, 229),
                LinStyle = ls1
            };
            layer.Objects.Add(line1);
            return layer;
        }
    }
}
