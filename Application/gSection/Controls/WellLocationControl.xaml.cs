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
                mb = (GraphManipulatorBase)value;
            }
        }

        
        #endregion

        public WellLocationControl()
        {
            InitializeComponent();
            CreateWellLocation();
            mb = new DrawLineManipulator();
        }

        private void CreateWellLocation()
        {
            Map map = new Map();
            Layer layer = new Layer();
            layer.Objects.Add(new SectionObject());
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            //layer.Objects = c.Create(twl);
            IObjects objs = c.Create(twl);
            foreach (var o in objs)
            {
                layer.Objects.Add(o);
            }
            map.Layers.Add(layer);
            //3.绑定lc数据源
            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mc.FullView();
        }

        private void mc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Type clickSourceType = e.OriginalSource.GetType();
            
            if (clickSourceType.Equals(typeof(Path)))
            {
                Path path = (Path)e.OriginalSource;
                double x = path.RenderTransform.Value.OffsetX;
                double y = path.RenderTransform.Value.OffsetY;
                Point p = new Point(x, y);
                ((DrawLineManipulator)mb).Start = p;
                path.StrokeThickness = 3;
                path.Stroke = Brushes.Black;
            }
            else
            {
                ((DrawLineManipulator)mb).Start = new Point(-1, -1);
            }
        }



        private void mc_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Type clickSourceType = e.OriginalSource.GetType();
                
                if (clickSourceType.Equals(typeof(Path)))
                {
                    Path path = (Path)e.OriginalSource;
                    double x = path.RenderTransform.Value.OffsetX;
                    double y = path.RenderTransform.Value.OffsetY;
                    Point p = new Point(x, y);
                    path.StrokeThickness = 3;
                    path.Stroke = Brushes.Black;
                    ((DrawLineManipulator)mb).End = p;
                }
                else
                {
                    ((DrawLineManipulator)mb).End = new Point(-1, -1);
                }
            }

        }



        private void mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }
        /// <summary>
        /// 按下右键：打开剖面设计页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string winName = "GPTDxWPFRibbonApplication1.New_section_set";
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            Window win = (Window)curAssembly.CreateInstance(winName);
            if (win != null)
            {
                win.WindowState = WindowState.Normal;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                win.ShowDialog();
            }
        }
    }
}
