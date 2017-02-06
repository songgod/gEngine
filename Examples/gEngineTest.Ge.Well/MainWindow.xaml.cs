using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
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

namespace gEngineTest.Ge.Well
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitWell();
        }

        private void InitWell()
        {
            Layer layer = new Layer();

            TxtWell tw = new TxtWell() { TxtFile = "D:\\Data\\MulWellColumnData.txt" };
            WellCreator wc = new WellCreator();
            layer.Objects = wc.Create(tw);

            Binding bd = new Binding("Objects") { Source = layer };
            lyControl.SetBinding(ItemsControl.ItemsSourceProperty, bd);


        }
    }
}
