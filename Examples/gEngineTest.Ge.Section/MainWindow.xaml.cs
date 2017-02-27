using gEngine.Graph.Ge;
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
            SectionLayer = SectionLayerCreator.CreateSectionLayer();
            this.DataContext = SectionLayer;
        }

        public Layer SectionLayer { get; set; }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new DrawLineManipulator(), lc);
        }

        private void EraseLine_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new EraseLineManipulator(), lc);
        }

        private void AddCurve_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new DrawCurveManipulator(), lc);
        }

        private void AddCloseCurve_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new DrawCloseCurveManipulator(), lc);
        }

        private void ReplaceLine_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new ReplaceLineManipulator(), lc);
        }

        private void RemoveFace_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new SetFaceTypeManipulator() { InvalidFace = true }, lc);
        }

        private void EditLine_Click(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new EditCurveManipulator(), lc);
        }
    }
}
