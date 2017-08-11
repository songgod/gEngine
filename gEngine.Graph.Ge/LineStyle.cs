using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class LineStyle : Freezable
    {
        public LineStyle()
        {

        }
        protected override Freezable CreateInstanceCore()
        {
            return new LineStyle();
        }

        public static LineStyle Default
        {
            get
            {
                return new LineStyle() { Width = 1.0, Stroke = Colors.Black, Symbol = "Solid", SymbolLib = "Normal" };
            }
        }

        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Color), typeof(LineStyle), new PropertyMetadata(Colors.Black));


        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(LineStyle), new PropertyMetadata(2.0));

        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(LineStyle), new PropertyMetadata("Solid"));

        public string SymbolLib
        {
            get { return (string)GetValue(SymbolLibProperty); }
            set { SetValue(SymbolLibProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SymbolLib.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolLibProperty =
            DependencyProperty.Register("SymbolLib", typeof(string), typeof(LineStyle), new PropertyMetadata("Normal"));

        public virtual LineStyle DeepClone()
        {
            LineStyle lineStyle = new LineStyle();
            lineStyle.Stroke = this.Stroke;
            lineStyle.Width = this.Width;
            lineStyle.Symbol = this.Symbol;
            lineStyle.SymbolLib = this.SymbolLib;

            return lineStyle;

        }
    }
}
