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
    /// LineControl.xaml 的交互逻辑
    /// </summary>
    public partial class LineControl : ContentControl
    {
        public LineControl()
        {
            InitializeComponent();
        }

        public LineStyle LineStyle
        {
            get { return (LineStyle)GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        private static void OnLineStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LineControl lc = (LineControl)d;
            LineStyle ls = (LineStyle)e.NewValue;
            if (ls == null)
                lc.Content = null;

            if(ls.LinType==LineStyle.LineType.NormalLine)
            {
                NormalLineStyle nls = ls as NormalLineStyle;
                Path path = new Path() { Data = lc.Data, Stroke = new SolidColorBrush(nls.Stroke),StrokeThickness=nls.Width, StrokeDashArray=nls.StrokeDashArray };
                lc.Content = path;
            }
            else if(ls.LinType==LineStyle.LineType.ComplexLine)
            {
                LineOptionSetting setting = ComplexLineStylePath2OptionSettingConverter.ConvertFromLineStyle(ls as ComplexLineStyle, lc.Data);
                if (setting == null)
                    lc.Content = null;
                object stroke = Registry.CreateStroke(setting);
                lc.Content = stroke;
            }
        }

        // Using a DependencyProperty as the backing store for LineStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(LineStyle), typeof(LineControl), 
                new PropertyMetadata(null,new PropertyChangedCallback(LineControl.OnLineStyleChanged)));


        public PathGeometry Data
        {
            get { return (PathGeometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        private static void OnLineDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LineControl lc = (LineControl)d;
            PathGeometry pg = (PathGeometry)e.NewValue;
            if (pg == null)
            {
                lc.Content = null;
                return;
            }
                
            LineStyle ls = lc.LineStyle;
            if (ls == null)
            {
                lc.Content = null;
                return;
            }

            if (ls.LinType == LineStyle.LineType.NormalLine)
            {
                NormalLineStyle nls = ls as NormalLineStyle;
                Path path = new Path() { Data = pg, Stroke = new SolidColorBrush(nls.Stroke), StrokeThickness = nls.Width, StrokeDashArray = nls.StrokeDashArray };
                lc.Content = path;
            }
            else if (ls.LinType == LineStyle.LineType.ComplexLine)
            {
                LineOptionSetting setting = ComplexLineStylePath2OptionSettingConverter.ConvertFromLineStyle(ls as ComplexLineStyle, pg);
                if (setting == null)
                    lc.Content = null;
                object stroke = Registry.CreateStroke(setting);
                lc.Content = stroke;
            }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(PathGeometry), typeof(LineControl), new PropertyMetadata(null, new PropertyChangedCallback(LineControl.OnLineDataChanged)));
    }
}
