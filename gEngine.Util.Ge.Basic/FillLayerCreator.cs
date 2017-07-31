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
           
            Layer layer = new Layer() { Type = "Fill" };

            PointCollection pc = new PointCollection();
            pc.Add(new System.Windows.Point(10, 10));
            pc.Add(new System.Windows.Point(100, 19));
            pc.Add(new System.Windows.Point(20, 30));
            pc.Add(new System.Windows.Point(40, 200));
            Boundary boun = new Boundary()
            {
                StrokeThickness = 2,
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                Points = pc,
                

            };
            layer.Objects.Add(boun);


            PointCollection pc1 = new PointCollection();
            pc1.Add(new System.Windows.Point(100, 100));
            pc1.Add(new System.Windows.Point(500, 190));
            pc1.Add(new System.Windows.Point(200, 300));
            pc1.Add(new System.Windows.Point(90, 300));
            Boundary boun1 = new Boundary()
            {
                StrokeThickness = 2,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                Points = pc1,
               

            };
            layer.Objects.Add(boun1);


            return layer;
        }
    }
}
