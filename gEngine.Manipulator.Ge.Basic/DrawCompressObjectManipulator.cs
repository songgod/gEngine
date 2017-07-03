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
    public class DrawCompressObjectManipulator: CompressManipulator
    {
        public DrawCompressObjectManipulator()
        {
            rectStyle = new NormalRectStyle();
        }
        public RectStyle rectStyle { get; set; }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Graph.Ge.Basic.Comprass com = new Graph.Ge.Basic.Comprass()
            {
                Width = this.TrackAdorner.Width,
                Height = this.TrackAdorner.Height,
                Top = Canvas.GetTop(this.TrackAdorner),
                Left = Canvas.GetLeft(this.TrackAdorner),
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 0,
                RotateAngle=0
            };
            this.AssociatedObject.LayerContext.Objects.Add(com);
            base.MouseLeftButtonUp(sender, e);
        }
    }
    public class DCMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCompressObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            RectStyle style = param as RectStyle;

            DrawCompressObjectManipulator dm = new DrawCompressObjectManipulator();
            if (style != null) dm.rectStyle = style;
            return dm;
        }
    }

}
