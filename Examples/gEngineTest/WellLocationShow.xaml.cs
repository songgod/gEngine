using gEngine.Graph.Ge;
using gEngine.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Globalization;
using static gEngine.Graph.Ge.Enums;
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
        string type = DataTemplateType.井位图.ToString();
        public WellLocationShow()
        {
            InitializeComponent();
            if (InitComboSource())
            {
                CreateWellLocation();
            }
            
        }
        /// <summary>
        /// 初始化模板选择下拉框数据源
        /// </summary>
        private bool InitComboSource()
        {
            bool b = false;
            var manager = new DataTemplateManager();
            cbTemplate.ItemsSource = manager.GetAllDataTemplatesByType(type);
            if(cbTemplate.Items.Count>0)
            {
                cbTemplate.SelectedIndex = 0;
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 概要：创建井位图
        /// </summary>
        private void CreateWellLocation()
        {
            //1.引入数据模板
            var manager = new DataTemplateManager();
            manager.RegDataTemplateByFile<WellLocation>(type,cbTemplate.SelectedValue.ToString());
            //2.创建Layer
            Layer layer = new Layer();
            TXTWellLocations twl = new TXTWellLocations() { TxtFile = "d:/welllocations.txt" };
            layer.Objects = GraphGeFactory.Single().Create(twl);
            //3.绑定lc数据源
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

        private void cbTemplate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateWellLocation();
        }
    }
}

