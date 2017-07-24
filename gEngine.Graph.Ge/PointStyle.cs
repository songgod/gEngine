using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class PointStyle : Freezable
    {
        public PointStyle()
        {
            SetDefaultProperty();
        }

        protected override Freezable CreateInstanceCore()
        {
            return new PointStyle();
        }

        public string SymbolLib { get; set; }

        //public string Symbol { get; set; }

        public Brush Stroke { get; set; }

        public double Height { get; set; }

        private void SetDefaultProperty()
        {
            Stroke = new SolidColorBrush(Colors.Red);

            Height = 30;
        }



        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(PointStyle));



        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(PointStyle),
                new PropertyMetadata((double)30));

        //private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    PointStyle ps = (PointStyle)d;
        //    double w = (double)e.NewValue;
        //    new PointStyle() { Width = w };
        //}

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(PointStyle), new PropertyMetadata(new SolidColorBrush(Colors.Red)));


    }
}
