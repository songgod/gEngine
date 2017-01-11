using gEngine.Graph.Ge;
using gEngine.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Globalization;
using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge.Business;
using gEngine.View.Datatemplate;
using gEngine.Graph.Interface;

namespace gEngineTest
{
    /// <summary>
    /// WellLocationShow.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationShow : Window
    {
        public WellLocationShow()
        {
            InitializeComponent();
            BuildDataTemplate();
            PreCreateWellLocation();
        }

        /// <summary>
        /// 概要：引入数据模板
        /// </summary>
        private void BuildDataTemplate()
        {
            var manager = new DataTemplateManager();
            manager.RegisterDataTemplate<WellLocation, WellLocationDataTemplate>();
        }

        /// <summary>
        /// 概要：创建井位图
        /// </summary>
        private void PreCreateWellLocation()
        {
            //1.创建Layer
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            layer.Objects = GraphGeFactory.Single().Create(twl);
            //2.绑定lc数据源
            Binding bd = new Binding("Objects") { Source = layer };
            lc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }

        /// <summary>
        /// 在InitializeComponent后执行
        /// 满屏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewUtil.FullView(lc);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewUtil.FullView(lc);
        } 
    }
}

