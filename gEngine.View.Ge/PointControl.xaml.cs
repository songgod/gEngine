using gEngine.Graph.Ge;
using gEngine.Symbol;
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

namespace gEngine.View.Ge
{
    /// <summary>
    /// PointControl.xaml 的交互逻辑
    /// </summary>
    public partial class PointControl : ContentControl
    {
        public PointControl()
        {
            InitializeComponent();
        }



        public PointStyle PointStyle
        {
            get { return (PointStyle)GetValue(PointStyleProperty); }
            set { SetValue(PointStyleProperty, value); }
        }

        private static void OnPointStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PointControl pc = (PointControl)d;
            PointStyle ps = (PointStyle)e.NewValue;
            if (ps == null)
                pc.Content = null;

            PointOptionSetting setting = PointStyle2OptionSettingConverter.CreateFromPointStyle(ps);
            object point = Registry.CreatePoint(setting);
            pc.Content = point;
        }

        // Using a DependencyProperty as the backing store for PointStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointStyleProperty =
            DependencyProperty.Register("PointStyle", typeof(PointStyle), typeof(PointControl), 
                new PropertyMetadata(new PointStyle() { SymbolLib = "ge", Symbol = "GeEllipsePointSymbol" }, new PropertyChangedCallback(PointControl.OnPointStyleChanged)));


    }
}
