using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Plane;
using gEngine.Manipulator.Ge.Section;
using gEngine.Util.Ge.Plane;
using gEngine.Util.Ge.Section;
using gEngine.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngineTest.Ge.WellLocation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        WellLocationsConnectManipulator dm;
        public MainWindow()
        {
            InitializeComponent();
            CreateWellLocation();

            dm = new WellLocationsConnectManipulator();
            dm.OnFinishSelect += Dm_OnFinishSelect;

        }

        private void Dm_OnFinishSelect(System.Collections.Generic.HashSet<string> names)
        {
            //ManipulatorSetter.ClearManipulator(mc.GetLayerControl(0));
            string strNames = string.Join(",", names);
            
            MessageBox.Show(strNames);
        }

        

        private void CreateWellLocation()
        {
            Map map = new Map();
            Layer layer = new Layer();
            //Layer sectionLayer = SectionLayerCreator.CreateSectionLayer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(twl);
            //IObjects objs = c.Create(twl);
            //foreach (var o in objs)
            //{
            //    sectionLayer.Objects.Add(o);
            //}
            map.Layers.Add(layer);
            //3.绑定lc数据源
            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);

            
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
            ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));

            dm.IsStopMove = false;
            dm.SelectWellLocations.Clear();
        }



    }
}
