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
            PathFigure figure = new PathFigure() { StartPoint = new Point(0, 0) };
            PointCollection pc = new PointCollection();
            pc.Add(new Point(250, 0));
            pc.Add(new Point(50, 200));
            pc.Add(new Point(300, 200));

            PointCollection pz = new PointCollection();

            pz.Add(new Point(350, 350));
            pz.Add(new Point(400, 400));


            PolyBezierSegment pbs = new PolyBezierSegment() { Points = pc };
            PolyLineSegment bls = new PolyLineSegment() { Points = pz };
            figure.Segments.Add(pbs);
            figure.Segments.Add(bls);

            geom.Figures.Add(figure);

            return geom;
        }

        public LineStyle LineStyle
        {
            get { return (LineStyle) GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(LineStyle), typeof(DrawLineWindow), new PropertyMetadata(new ComplexLineStyle() { SymbolLib = "ge", Symbol = "GeStrokeSymbol" }));

        public PathGeometry Data
        {
            get { return (PathGeometry) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(PathGeometry), typeof(DrawLineWindow), new PropertyMetadata(InitPathData()));
        //DependencyProperty.Register("Data", typeof(PathGeometry), typeof(DrawLineWindow));
    }
}
