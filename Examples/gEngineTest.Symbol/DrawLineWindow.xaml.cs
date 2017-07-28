using gEngine.Graph.Ge;
using GraphAlgo;
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
using System.Windows.Shapes;
using System.Reflection;
using gEngine.Util;

namespace gEngineTest.Symbol
{
    /// <summary>
    /// DrawLineWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DrawLineWindow : Window
    {
        public DrawLineWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        static PathGeometry InitPathData()
        {
            PathGeometry geom = new PathGeometry();
            PathFigure figure = new PathFigure() { StartPoint = new Point(50, 100) };
            PointCollection pc = new PointCollection();
            pc.Add(new Point(210, 20));
            pc.Add(new Point(70, 210));
            pc.Add(new Point(300, 200));

            PointCollection pz = new PointCollection();

            pz.Add(new Point(350, 350));
            pz.Add(new Point(400, 400));
            pz.Add(new Point(420, 200));

            PolyBezierSegment pbs = new PolyBezierSegment() { Points = pc };
            PolyLineSegment bls = new PolyLineSegment() { Points = pz };

            //PathGeometry geom = new PathGeometry();
            //PathFigure figure = new PathFigure() { StartPoint = new Point(100, 100) };
            //PointCollection pz = new PointCollection();
            //pz.Add(new Point(200, 200));

            //PointCollection pc = new PointCollection();
            //pc.Add(new Point(300, 200));
            //pc.Add(new Point(400, 100));
            //pc.Add(new Point(300, 50));

            //PolyBezierSegment pbs = new PolyBezierSegment() { Points = pc };
            //PolyLineSegment bls = new PolyLineSegment() { Points = pz };

            figure.Segments.Add(pbs);
            figure.Segments.Add(bls);
            geom.Figures.Add(figure);
            return geom;
        }

        static double Distance(Point p0, Point p1)
        {
            return Math.Sqrt((Math.Pow((p1.X - p0.X), 2) + Math.Pow((p1.Y - p0.Y), 2)));
        }

        public LineStyle LineStyle
        {
            get { return (LineStyle) GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(LineStyle), typeof(DrawLineWindow), new PropertyMetadata(new LineStyle() { SymbolLib = "ge", Symbol = "WavyLineSymbol" }));

        public PathGeometry Data
        {
            get { return (PathGeometry) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(PathGeometry), typeof(DrawLineWindow), new PropertyMetadata(InitPathData()));
    }
}
