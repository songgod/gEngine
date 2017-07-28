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
            Layer layer = new Layer() { Type = "Fill" };
            PointCollection pc = new PointCollection();
            pc.Add(new System.Windows.Point(10, 10));
            pc.Add(new System.Windows.Point(100, 19));
            pc.Add(new System.Windows.Point(20, 30));
            pc.Add(new System.Windows.Point(40, 200));
            Boundary boun = new Boundary()
            {
                StrokeThickness = 3,
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                Points = pc

            };
            layer.Objects.Add(boun);

            Rect rect = new Rect()
            {
                Top = 0,
                Left = 150,
                Width = 100,
                Height = 100,
                Fill = Brushes.Black,
                Stroke = Brushes.Red

            };
            layer.Objects.Add(rect);

            return layer;
        }
    }
}
