using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Plane;
using gEngine.View;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace gEngineTest.Ge.WellLocation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateWellLocation();
        }

        private void CreateWellLocation()
        {
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(twl);
            //3.绑定lc数据源
            Binding bd = new Binding("Objects") { Source = layer };
            lc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewUtil.FullView(lc);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewUtil.FullView(lc);
        }
    }
}
