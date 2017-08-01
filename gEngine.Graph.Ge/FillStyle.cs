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
