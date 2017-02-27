using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Plane;
using gEngine.View;
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
using gEngine.Util;

namespace GPTDxWPFRibbonApplication1.Controls
{
    /// <summary>
    /// WellLocationControl.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationControl : UserControl, IView
    {
        #region IView接口实现
        FrameworkElement IView.FullScreenObject
        {
            get { return lc; }
            set { lc = (LayerControl)value; }
        }
        #endregion

        public WellLocationControl()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewUtil.FullView(lc);
        }
    }
}
