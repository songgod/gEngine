using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Manipulator.Ge.Section;
using gEngine.Util.Ge.Plane;
using gEngine.Util.Ge.Section;
using gEngine.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

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

        DrawLineManipulator dm = new DrawLineManipulator();

        private void CreateWellLocation()
        {
            Map map = new Map();
            //Layer sectionLayer = new Layer();
            Layer sectionLayer = SectionLayerCreator.CreateSectionLayer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            WellLocationsCreator c = new WellLocationsCreator();
            //sectionLayer.Objects = c.Create(twl);
            IObjects objs = c.Create(twl);
            foreach (var o in objs)
            {
                sectionLayer.Objects.Add(o);
            }
            map.Layers.Add(sectionLayer);
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

        }


        private void mc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Type clickSourceType = e.OriginalSource.GetType();
            if (clickSourceType.Equals(typeof(Path)))
            {
                Path elp=(Path)e.OriginalSource;
                double x = elp.RenderTransform.Value.OffsetX;
                double y = elp.RenderTransform.Value.OffsetY;
                Point p = new Point(x, y);
                //MessageBox.Show(p.ToString());
                
                dm.Start = p;
                elp.StrokeThickness = 3;
                elp.Stroke = Brushes.Black;
                //ManipulatorSetter.SetManipulator(new DrawLineManipulator(), mc.GetLayerControl(0));
            }
            else
            {
                //MessageBox.Show(dm.Start.ToString());
                dm.Start = new Point(-1,-1);
                //dm.End = new Point(-1, -1);
            }
        }

        private void mc_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Type clickSourceType = e.OriginalSource.GetType();
                if (clickSourceType.Equals(typeof(Path)))
                {
                    Path elp = (Path)e.OriginalSource;
                    double x = elp.RenderTransform.Value.OffsetX;
                    double y = elp.RenderTransform.Value.OffsetY;
                    Point p = new Point(x, y);
                    elp.StrokeThickness = 3;
                    elp.Stroke = Brushes.Black;
                    dm.End = p;

                }
                else
                {
                    dm.End = new Point(-1, -1);
                }
            }
              
        }
    }
}
