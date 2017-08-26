using gEngine.Graph.Ge;
using gEngine.Manipulator;
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

namespace gEngineTest.Manipulator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MapContext = new Map();
            MapContext.Layers.Add(new Layer());
            this.DataContext = this;

            gEngine.Symbol.Registry.LoadLocalSymbols();

        }

        public Map MapContext { get; set; }

        private void Drawline_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawLineObjectManipulator";
        }

        private void DrawBezier_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawBezierLineObjectManipulator";
        }

        private void DrawPolyline_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawPolyLineObjectManipulator";
        }
        private void DrawRect_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawRectObjectManipulator";
        }

        private void DrawCompress_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawCompressObjectManipulator";
        }

        private void DrawBoundary_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawBoundaryObjectManipulator";
        }

        private void ScaleRule_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawScaleRuleObjectManipulator";
        }

        private void EditLine_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "EditLineManipulator";
        }
    }
}
