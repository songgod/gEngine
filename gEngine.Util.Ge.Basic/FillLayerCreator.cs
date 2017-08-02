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
    public class FillLayerCreator
    {
        public Layer Create()
        {
            FillStyle fs = new FillStyle();
            //fs.Stroke = Brushes.Aqua;
            //fs.StrokeThickness = 2;
            Layer layer = new Layer() { Type = "Fill" };

            PointCollection pc = new PointCollection();
            pc.Add(new System.Windows.Point(10, 10));
            pc.Add(new System.Windows.Point(100, 19));
            pc.Add(new System.Windows.Point(20, 30));
            pc.Add(new System.Windows.Point(40, 200));

            Boundary boun = new Boundary()
            {
                Stroke = Colors.Black,
                StrokeThickness=1.0,
                Points = pc,
                FillStyle = fs

            };
            layer.Objects.Add(boun);

            return layer;
        }
    }
}
