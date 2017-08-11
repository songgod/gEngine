using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace gEngine.Manipulator.Ge.Basic
{
    public class DrawRectObjectManipulator: DrawRectManipulatorBase
    {
        public DrawRectObjectManipulator()
        {
            FillStyle = new FillStyle();
        }
        public FillStyle FillStyle { get; set; }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.TrackAdorner == null)
                return;
            Graph.Ge.Basic.Rect rect = new Graph.Ge.Basic.Rect()
            {
                Width = this.TrackAdorner.Width,
                Height = this.TrackAdorner.Height,
                Top = Canvas.GetTop(this.TrackAdorner),
                Left = Canvas.GetLeft(this.TrackAdorner),
                Fill = Brushes.White,
                Stroke = new SolidColorBrush(Colors.Red),
                StrokeThickness = 1.0,
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
