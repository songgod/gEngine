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
        public enum LineType
        {
            NormalLine=0,
            ComplexLine,
            Unkown
        }
        public virtual LineType LinType { get { return LineType.Unkown; } }
        protected override Freezable CreateInstanceCore()
        {
            return new LineStyle();
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

    }

    public class NormalLineStyle : LineStyle
    {
        public NormalLineStyle()
        {
        }
        public override LineType LinType { get { return LineType.NormalLine; } }

        protected override Freezable CreateInstanceCore()
        {
            return new NormalLineStyle();
        }
        
        public DoubleCollection StrokeDashArray
        {
            get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeDashArrayProperty =
            DependencyProperty.Register("StrokeDashArray", typeof(DoubleCollection), typeof(NormalLineStyle));

    }

    public class ComplexLineStyle : LineStyle
    {
        public ComplexLineStyle()
        {
            
        }
        public override LineType LinType { get { return LineType.ComplexLine; } }

        protected override Freezable CreateInstanceCore()
        {
            return new ComplexLineStyle();
        }

        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(ComplexLineStyle), new PropertyMetadata(""));
        
        public string SymbolLib
        {
            get { return (string)GetValue(SymbolLibProperty); }
            set { SetValue(SymbolLibProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SymbolLib.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolLibProperty =
            DependencyProperty.Register("SymbolLib", typeof(string), typeof(ComplexLineStyle), new PropertyMetadata(""));

        
    }
}
