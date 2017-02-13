using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.View;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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
            InitDataTemplate();
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

        private void InitDataTemplate()
        {
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(
                    System.Windows.Application.LoadComponent(
                        new Uri("gEngineTest;component/WellColumnDictionary.xaml",
                        UriKind.Relative)) as System.Windows.ResourceDictionary);

            //System.Windows.Application.Current.Resources.MergedDictionaries.Add(
            //        System.Windows.Application.LoadComponent(
            //            new Uri("gEngine.View.Datatemplate;component/Resources/WellColumnDictionary.xaml",
            //            UriKind.Relative)) as System.Windows.ResourceDictionary);
        }

        private void GetGeometryPath()
        {
            Random rdm = new Random();
            List<Path> pathList = FindVisualChild<Path>(this.lyControl);
            foreach (Path path in pathList)
            {
                SolidColorBrush sBrush = new SolidColorBrush();
                if (((Base)path.DataContext).Name.Equals("DEPTH"))
                {
                    sBrush.Color = Colors.Blue;
                }
                else
                {
                    sBrush.Color = Color.FromRgb((byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255));
                }
                path.Stroke = sBrush;
            }
        }

        private List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ViewUtil.FullView(lyControl);
            GetGeometryPath();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewUtil.FullView(lyControl);
        }
    }
}
