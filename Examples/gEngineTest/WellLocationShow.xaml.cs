using gEngine.Graph.Ge;
using gEngine.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Globalization;
using static gEngine.Graph.Interface.Enums;
using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge.Business;

namespace gEngineTest
{
    public class WellTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WellType wt = (WellType)value;
            switch (wt)
            {
                case WellType.W:
                    return Colors.Blue.ToString();
                case WellType.Y:
                    return Colors.Red.ToString();
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ActualSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) - System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// WellLocationShow.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationShow : Window
    {
        public WellLocationShow()
        {
            InitializeComponent();
            PreCreateWellLocation();
        }

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

