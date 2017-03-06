using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Plane;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using gEngine.Util;

namespace GPTDxWPFRibbonApplication1.Controls
{
    /// <summary>
    /// WellLocationControl.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationControl : UserControl, IView
    {
        #region IView接口实现
        FrameworkElement IView.FullScreenObject
        {
            get { return mc; }
            set { mc = (MapControl)value; }
        }
        Canvas layerCanvas;
        Canvas lineCanvas;
        Point currentPoint;
        Point lastPoint;
        bool isLeftButtonPressed;
        #endregion

        public WellLocationControl()
        {
            InitializeComponent();
            CreateWellLocation();
            
        }

        private void CreateWellLocation()
        {
            Map map = new Map();
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(twl);
            map.Layers.Add(layer);
            //3.绑定lc数据源
            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ViewUtil.FullView(mc);
            mc.FullView();
            layerCanvas = mc.GetLayerControl(0).Root;
            //MessageBox.Show(layerCanvas.Children.Count.ToString()); 
        }

        private void mc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isLeftButtonPressed = true;
            Type clickSourceType = e.OriginalSource.GetType();
            if (clickSourceType.Equals(typeof(Path)))
            {
                double x = ((Path)e.OriginalSource).RenderTransform.Value.OffsetX;
                double y = ((Path)e.OriginalSource).RenderTransform.Value.OffsetY;
                Point p = new Point(x, y);
                MessageBox.Show(p.ToString());

                lineCanvas = new Canvas()
                {
                    Height = layerCanvas.ActualHeight,
                    Width = layerCanvas.ActualWidth,
                    FlowDirection = FlowDirection.LeftToRight,
                    Visibility = Visibility.Visible
                };
                //layerCanvas.Children.Add(lineCanvas);
                currentPoint = new Point(x, y);
                DrawLine(currentPoint, currentPoint);
                lastPoint = currentPoint;
                layerCanvas.CaptureMouse();
            }

        }

        void DrawLine(Point fromPoint, Point toPoint)
        {
            Line line = new Line()
            {
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(Colors.Red)
            };
            line.X1 = toPoint.X;
            line.Y1 = toPoint.Y;
            line.X2 = fromPoint.X;
            line.Y2 = fromPoint.Y;
            lineCanvas.Children.Add(line);
        }

        private void mc_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLeftButtonPressed)
            {
                currentPoint = e.GetPosition(lineCanvas);
                DrawLine(currentPoint, lastPoint);
                lastPoint = currentPoint;
            }

        }



        private void mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isLeftButtonPressed = false;
            layerCanvas.ReleaseMouseCapture();
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.StrokeThickness = 3;
            path.Stroke = new SolidColorBrush(Colors.Red);
            path.StrokeStartLineCap = PenLineCap.Round;
            path.StrokeEndLineCap = PenLineCap.Round;
            //path.Opacity = BrushOpacity;
            PathGeometry g = new PathGeometry();
            if (lineCanvas != null)
                foreach (Line l in lineCanvas.Children)
                {
                    if (l == lineCanvas.Children[0])
                    {
                        g.Figures.Add(new PathFigure() { StartPoint = new Point(l.X1, l.Y1) });
                        g.Figures[0].Segments.Add(new LineSegment() { Point = new Point(l.X2, l.Y2) });
                    }
                    else
                    {
                        g.Figures[0].Segments.Add(new LineSegment() { Point = new Point(l.X1, l.Y1) });
                        g.Figures[0].Segments.Add(new LineSegment() { Point = new Point(l.X2, l.Y2) });
                    }
                }
            path.StrokeLineJoin = PenLineJoin.Round;
            path.Data = g;
            layerCanvas.Children.Remove(lineCanvas);
            layerCanvas.Children.Add(path);
        }
    }
}
