using gEngine.Symbol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Symbol.normal
{
    public class NormalSymbolFactory : ISymbolFactory
    {
        protected Dictionary<string, PointSymbol> DicPointSymbols;
        protected Dictionary<string, StrokeSymbol> DicStrokeSymbols;
        protected Dictionary<string, FillSymbol> DicFillSymbols;

        public NormalSymbolFactory()
        {
            DicPointSymbols = new Dictionary<string, PointSymbol>();
            DicStrokeSymbols = new Dictionary<string, StrokeSymbol>();
            DicFillSymbols = new Dictionary<string, FillSymbol>();
        }
        public List<string> FillSymbolNames
        {
            get
            {
                return DicFillSymbols.Keys.ToList();
            }
            set
            {
                ;
            }
        }

        public List<string> PointSymbolNames
        {
            get
            {
                return DicPointSymbols.Keys.ToList();
            }
            set
            {
                ;
            }
        }

        public List<string> StrokeSymbolNames
        {
            get
            {
                return DicStrokeSymbols.Keys.ToList();
            }
            set
            {
                ;
            }
        }

        public string Url
        {
            get
            {
                string dir = Directory.GetCurrentDirectory();
                return dir + @"\Symbols\Normal.normal";
            }
            set
            {
                ;
            }
        }

        public FillSymbol GetFillSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (DicFillSymbols.ContainsKey(name))
                return DicFillSymbols[name];
            return null;
        }

        public PointSymbol GetPointSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (DicPointSymbols.ContainsKey(name))
                return DicPointSymbols[name];
            return null;
        }

        public StrokeSymbol GetStrokeSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (DicStrokeSymbols.ContainsKey(name))
                return DicStrokeSymbols[name];
            return null;
        }

        public bool LoadFromUrl(string url)
        {
            DicPointSymbols["Ellpise"] = new EllpisePointSymbol();
            DicStrokeSymbols["Solid"] = new SolidStrokeSymbol();
            DicStrokeSymbols["Dot11"] = new DotStrokeSymbol() { DotSymbolName = "Dot11StrokeSymbol", Dot = new DoubleCollection() { 1.0, 1.0 } };
            DicStrokeSymbols["Dot121"] = new DotStrokeSymbol() { DotSymbolName = "Dot121StrokeSymbol", Dot = new DoubleCollection() { 1.0, 2.0, 1.0 } };
            DicStrokeSymbols["Dot131"] = new DotStrokeSymbol() { DotSymbolName = "Dot131StrokeSymbol", Dot = new DoubleCollection() { 1.0, 3.0, 1.0 } };
            DicStrokeSymbols["Dot141"] = new DotStrokeSymbol() { DotSymbolName = "Dot141StrokeSymbol", Dot = new DoubleCollection() { 1.0, 4.0, 1.0 } };
            DicStrokeSymbols["Dot232"] = new DotStrokeSymbol() { DotSymbolName = "Dot232StrokeSymbol", Dot = new DoubleCollection() { 2.0, 3.0, 2.0 } };
            DicStrokeSymbols["Dot242"] = new DotStrokeSymbol() { DotSymbolName = "Dot242StrokeSymbol", Dot = new DoubleCollection() { 1.0, 4.0, 1.0 } };
            DicStrokeSymbols["Dot252"] = new DotStrokeSymbol() { DotSymbolName = "Dot252StrokeSymbol", Dot = new DoubleCollection() { 1.0, 5.0, 1.0 } };
            DicStrokeSymbols["Dot262"] = new DotStrokeSymbol() { DotSymbolName = "Dot262StrokeSymbol", Dot = new DoubleCollection() { 1.0, 6.0, 1.0 } };

            DicFillSymbols["Red"] = new SolidFillSymbol() { SolidSymbolName = "RedFill", SolidColor = Colors.Red };
            DicFillSymbols["Green"] = new SolidFillSymbol() { SolidSymbolName = "GreenFill", SolidColor = Colors.Green };
            DicFillSymbols["Blue"] = new SolidFillSymbol() { SolidSymbolName = "BlueFill", SolidColor = Colors.Blue };
            DicFillSymbols["Black"] = new SolidFillSymbol() { SolidSymbolName = "BlackFill", SolidColor = Colors.Black };
            DicFillSymbols["White"] = new SolidFillSymbol() { SolidSymbolName = "WhiteFill", SolidColor = Colors.White };
            DicFillSymbols["LightGray"] = new SolidFillSymbol() { SolidSymbolName = "LightGrayFill", SolidColor = Colors.LightGray };
            DicFillSymbols["Gray"] = new SolidFillSymbol() { SolidSymbolName = "GrayFill", SolidColor = Colors.Gray };

            GradientStopCollection gsc1 = new GradientStopCollection();
            gsc1.Add(new GradientStop() { Offset = 0, Color = Colors.Red });
            gsc1.Add(new GradientStop() { Offset = 0.25, Color = Colors.Blue });
            gsc1.Add(new GradientStop() { Offset = 0.6, Color = Colors.DarkRed });
            gsc1.Add(new GradientStop() { Offset = 1, Color = Colors.Aqua });
            DicFillSymbols["LinearGradient1"] = new GradientFillSymbol() { GradientSymbolName = "LinearGradient1Fill", GradientColor = gsc1 };
            GradientStopCollection gsc2 = new GradientStopCollection();
            gsc2.Add(new GradientStop() { Offset = 0, Color = Colors.CadetBlue });
            gsc2.Add(new GradientStop() { Offset = 0.25, Color = Colors.Red });
            gsc2.Add(new GradientStop() { Offset = 0.6, Color = Colors.Green });
            gsc2.Add(new GradientStop() { Offset = 1, Color = Colors.HotPink });
            DicFillSymbols["LinearGradient2"] = new GradientFillSymbol() { GradientSymbolName = "LinearGradient2Fill", GradientColor = gsc2 };

            return true;
        }
    }
}
