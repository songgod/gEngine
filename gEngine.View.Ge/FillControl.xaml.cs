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
    /// FillControl.xaml 的交互逻辑
    /// </summary>
    public partial class FillControl : ContentControl
    {
        public FillControl()
        {
            InitializeComponent();
        }
        public FillStyle FillStyle
        {
            get { return (FillStyle)GetValue(FillStyleProperty); }
            set { SetValue(FillStyleProperty, value); }
        }

        private static void OnFillStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FillControl lc = (FillControl)d;
            FillStyle ls = (FillStyle)e.NewValue;
            if (ls == null)
            {
                lc.Content = null;
                return;
            }
            Brush setting = FillStyle2BrushConverter.ConverterFromFillStyle(ls);
            if (setting == null)
            {
                lc.Content = null;
                return;
            }
        
            Path p = new Path();
            p.Fill = setting;
            p.Data = lc.Data;
            lc.Content = p;
        }

        // Using a DependencyProperty as the backing store for LineStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillStyleProperty =
            DependencyProperty.Register("FillStyle", typeof(FillStyle), typeof(FillControl), new PropertyMetadata(null, new PropertyChangedCallback(FillControl.OnFillStyleChanged)));


        public PathGeometry Data
        {
            get { return (PathGeometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        private static void OnFillDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FillControl lc = (FillControl)d;
            PathGeometry pg1 = (PathGeometry)e.NewValue;
            if (pg1 == null)
            {
                lc.Content = null;
                return;
            }

            FillStyle ls = lc.FillStyle;
            if (ls == null)
            {
                lc.Content = null;
                return;
            }

            Brush setting = FillStyle2BrushConverter.ConverterFromFillStyle(ls);
            if (setting == null)
                lc.Content = null;
           
            Path p = new Path();
            p.Data = lc.Data;
           
            lc.Content = p;
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(PathGeometry), typeof(FillControl), new PropertyMetadata(null, new PropertyChangedCallback(FillControl.OnFillDataChanged)));


    }
}
