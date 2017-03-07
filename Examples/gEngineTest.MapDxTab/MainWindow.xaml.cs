using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
using gEngine.Util.Ge.Plane;
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

namespace gEngineTest.MapDxTab
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Project = new Project();
            tc.DataContext = Project;
        }

        public Project Project { get; set; }

        private void AddColumnMap_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map() { Name = "Column" };
            Layer layer = new Layer();

            TxtWell tw = new TxtWell() { TxtFile = "D:\\Data\\MulWellColumnDataNew.txt" };
            WellCreator wc = new WellCreator();
            layer.Objects = wc.Create(tw);
            map.Layers.Add(layer);
            Project.Maps.Add(map);
        }

        private void AddPalneMap_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map() { Name = "Plane" };
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(twl);
            map.Layers.Add(layer);
            Project.Maps.Add(map);
        }
    }
}
