using gEngine.Symbol;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace gEngineTest.Symbol
{
    public class PointGraph
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Symbol { get; set; }
    }
    public class LineGraph
    {
        public PointCollection Points { get; set; }
        public string Symbol { get; set; }
    }

    public class PolygonGraph
    {
        public PointCollection Points { get; set; }
        public string Symbol { get; set; }
    }

    public class LineSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection pc = value as PointCollection;
            string symbol = parameter as string;
            
            PathGeometry pathgeom = new PathGeometry();
            PolyLineSegment ps = new PolyLineSegment() { Points = pc };
            PathFigure pf = new PathFigure() { StartPoint = pc.First() };
            pf.Segments.Add(ps);
            pathgeom.Figures.Add(pf);
            LineOptionSetting setting = new LineOptionSetting() { Path = pathgeom, Factory = "", Symbol = symbol };
            return Registry.CreateStroke(setting);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public PointGraph Point { get; set; }
        public LineGraph Line { get; set; }
        public PolygonGraph Polygon { get; set; }
        public MainWindow()
        {
            Point = new PointGraph() { X = 100, Y = 100 };
            Line = new LineGraph() { Points = new PointCollection() };
            Line.Points.Add(new Point(100, 100));
            Line.Points.Add(new Point(200, 200));
            Polygon = new PolygonGraph() { Points = new PointCollection() };
            Polygon.Points.Add(new System.Windows.Point(20, 40));
            Polygon.Points.Add(new System.Windows.Point(300, 400));
            Polygon.Points.Add(new System.Windows.Point(600, 200));
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
