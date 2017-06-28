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
        }

        public Map MapContext { get; set; }

        private void Drawline_Click(object sender, RoutedEventArgs e)
        {
            IManipulatorBase mp = Registry.CreateManipulator("DrawLineObjectManipulator");
            ManipulatorSetter.SetManipulator(mp, mc.GetLayerControl(0));
        }

        private void DrawBezier_Click(object sender, RoutedEventArgs e)
        {
            IManipulatorBase mp = Registry.CreateManipulator("DrawBezierLineObjectManipulator");
            ManipulatorSetter.SetManipulator(mp, mc.GetLayerControl(0));
        }

        private void DrawPolyline_Click(object sender, RoutedEventArgs e)
        {
            IManipulatorBase mp = Registry.CreateManipulator("DrawPolyLineObjectManipulator");
            ManipulatorSetter.SetManipulator(mp, mc.GetLayerControl(0));
        }
    }
}
