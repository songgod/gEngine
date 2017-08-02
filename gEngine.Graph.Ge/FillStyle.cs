using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class FillStyle : Freezable
    {


        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(FillStyle), new PropertyMetadata("White"));



        public string SymbolLib
        {
            get { return (string)GetValue(SymbolLibProperty); }
            set { SetValue(SymbolLibProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SymbolLib.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolLibProperty =
            DependencyProperty.Register("SymbolLib", typeof(string), typeof(FillStyle), new PropertyMetadata("Normal"));

        //public double StrokeThickness
        //{
        //    get { return (double)GetValue(StrokeThicknessProperty); }
        //    set { SetValue(StrokeThicknessProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty StrokeThicknessProperty =
        //    DependencyProperty.Register("StrokeThickness", typeof(double), typeof(FillStyle));

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(FillStyle));

        //public Brush Stroke
        //{
        //    get { return (Brush)GetValue(StrokeProperty); }
        //    set { SetValue(StrokeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty StrokeProperty =
        //    DependencyProperty.Register("Stroke", typeof(Brush), typeof(FillStyle));

        protected override Freezable CreateInstanceCore()
        {
            return new FillStyle();
        }

        public static FillStyle Default
        {
            get
            {
                return new FillStyle() { Symbol = "White", SymbolLib = "Normal" };
            }
        }
    }
}
