using gEngine.Graph.Ge;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.Util.Ge.Section;
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

namespace gEngineTest.Ge.Section
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Map map = new Map();
            SectionLayerCreator sc = new SectionLayerCreator();
            Layer SectionLayer = sc.CreateSectionLayer();
            map.Layers.Add(SectionLayer);
            mc.DataContext = map;
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            //ManipulatorSetter.SetManipulator(new DrawLineFaultManipulator(), mc.GetLayerControl(0));
        }

        private void EraseLine_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "EraseLineManipulator";
        }

        private void AddCurve_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "DrawCurveFaultManipulator";
        }

        private void AddCloseCurve_Click(object sender, RoutedEventArgs e)
        {
            //ManipulatorSetter.SetManipulator(new DrawCloseCurveManipulator(), mc.GetLayerControl(0));
        }

        private void ReplaceLine_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "ReplaceLineManipulator";
        }

        private void RemoveFace_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "SetFaceTypeManipulator";
        }

        private void EditLine_Click(object sender, RoutedEventArgs e)
        {
            mc.MapContext.Layers[0].Manipulator = "EditCurveManipulator";
        }
    }
}
