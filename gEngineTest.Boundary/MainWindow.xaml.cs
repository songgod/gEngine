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
using gEngine.Graph.Ge.Basic;
using gEngine.Graph.Ge;
using gEngine.Manipulator.Ge.Basic;
using gEngine.View;
using gEngine.Manipulator;
using gEngine.Data.Ge.Txt;
using gEngine.Util.Ge.Plane;

namespace gEngineTest.Boundary
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BoundaryManipulator boundaryMani;

        public MainWindow()
        {
            InitializeComponent();

            boundaryMani = (BoundaryManipulator)(gEngine.Manipulator.Registry.CreateManipulator("BoundaryManipulator", mc));
            boundaryMani.OnFinishDrawBoundary += BoundaryMani_OnFinishDrawBoundary;

            Map map = new Map();
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(twl);
            map.Layers.Add(layer);
            mc.DataContext = map;
        }

        private void BoundaryMani_OnFinishDrawBoundary(gEngine.Graph.Ge.Basic.Boundary border)
        {
            ContentControl cc = new ContentControl() { Content = border };
            mc.BackGroundLayer.Children.Clear();
            mc.BackGroundLayer.Children.Add(cc);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mc.FullView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mc.FullView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LayerControl lc = mc.ActiveLayerControl;
            ManipulatorSetter.SetManipulator(boundaryMani, lc);
        }
    }
}
