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
using gEngine.Manipulator.Ge.Section;
using gEngine.Util.Ge.Section;
using gEngine.Graph.Ge.Section;
using gEngine.Manipulator;
using System.Windows.Interactivity;
using gEngine.Graph.Interface;
using System.Reflection;
using gEngine.Graph.Ge.Plane;
using gTopology;
using GPTDxWPFRibbonApplication1.ViewModels;
using gEngine.Manipulator.Ge.Plane;

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
            get { return mc; }
            set { mc = (MapControl)value; }
        }
        ManipulatorBase mb = null;
        Behavior<UIElement> IView.ManipulatorBehavior
        {
            get
            {
                return mb;
            }
            set
            {
                mb = (ManipulatorBase)value;
            }
        }


        #endregion


        public WellLocationControl()
        {
            InitializeComponent();
            CreateWellLocation();
            mb = new WellLocationsConnectManipulator();
            ((WellLocationsConnectManipulator)mb).OnFinishSelect += Dm_OnFinishSelect;
        }

        private void Dm_OnFinishSelect(System.Collections.Generic.HashSet<string> names)
        {
            string strNames = string.Join(",", names);
            string winName = "GPTDxWPFRibbonApplication1.DXNewSectionSet";
            OpenWindow(winName, strNames);
        }


        private void CreateWellLocation()
        {
            Map map = new Map();
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(twl);
            map.Layers.Add(layer);
            //3.绑定lc数据源
            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mc.FullView();
        }

        /// <summary>
        /// 下右键：结束画线，打开剖面设计页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ManipulatorSetter.ClearManipulator(mc.GetLayerControl(0));
            //string winName = "GPTDxWPFRibbonApplication1.DXNewSectionSet";
            //OpenWindow(winName);
        }

        private void OpenWindow(string winName, string wellNums)
        {
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            //string wellNums = string.Join(",", ((DrawWellTieSection)mb).WellNumList);
            object[] parameters = new object[] { wellNums };
            Window win = (Window)curAssembly.CreateInstance(winName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);

            if (win != null)
            {
                win.WindowState = WindowState.Normal;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                win.ShowDialog();
            }
        }

    }
}