using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawRectObjectManipulator: RectManipulator
    {
        public DrawRectObjectManipulator()
        {
            FillStyle = new FillStyle();
        }
        public FillStyle FillStyle { get; set; }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Graph.Ge.Basic.Rect rect = new Graph.Ge.Basic.Rect()
            {
                Width = this.TrackAdorner.Width,
                Height = this.TrackAdorner.Height,
                Top = Canvas.GetTop(this.TrackAdorner),
                Left = Canvas.GetLeft(this.TrackAdorner),
                Fill = null,
                Stroke = Brushes.Black,
                FillStyle = FillStyle
            };
            this.AssociatedObject.LayerContext.Objects.Add(rect);
            base.MouseLeftButtonUp(sender, e);
        }

    }
    public class DRMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawRectObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            FillStyle style = param as FillStyle;

            DrawRectObjectManipulator dm = new DrawRectObjectManipulator();
            if (style != null) dm.FillStyle = style;
            return dm;
        }
    }
}
