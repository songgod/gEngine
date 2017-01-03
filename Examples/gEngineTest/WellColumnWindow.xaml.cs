using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Business;
using gEngine.Graph.Interface;
using gEngine.Utility;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Shapes;

namespace gEngineTest
{
    /// <summary>
    /// WellColumnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WellColumnWindow : Window
    {
        public WellColumnWindow()
        {
            InitializeComponent();
            InitWell();
        }

        private void InitWell()
        {
            Layer layer = new Layer();

            TxtWell tw = new TxtWell() { TxtFile = "D:\\Data\\MulWellColumnData.txt" };
            layer.Objects = GraphGeFactory.Single().Create(tw);

            Binding bd = new Binding("Objects") { Source = layer };
            lyControl.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ViewUtil.FullView(lyControl);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewUtil.FullView(lyControl);
        }
    }
}
