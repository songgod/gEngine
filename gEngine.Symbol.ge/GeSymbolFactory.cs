using gEngine.Symbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Symbol.gesym
{
    public class GeSymbolFactory : ISymbolFactory
    {
        public Dictionary<string,FillSymbol> DicFillSymbol { get; set; }
        public Dictionary<string,StrokeSymbol> DicStrokeSymbol { get; set; }

        public string Url { get; set; }

        public PointSymbols PointSymbols { get; set; }


        public GeSymbolFactory()
        {
            DicFillSymbol = new Dictionary<string, FillSymbol>();
            DicStrokeSymbol = new Dictionary<string, StrokeSymbol>();

            Init();
        }

        private void Init()
        {
            DicFillSymbol["GeFillSymbol"] = new GeFillSymbol();
            DicStrokeSymbol["WavyLineSymbol"] = new GeWavyLineSymbol();
        }

        public FillSymbol GetFillSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (DicFillSymbol.ContainsKey(name))
                return DicFillSymbol[name];
            return null;
        }

        public PointSymbol GetPointSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (PointSymbols.ContainsKey(name))
                return PointSymbols[name];
            return null;
        }

        public StrokeSymbol GetStrokeSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (DicStrokeSymbol.ContainsKey(name))
                return DicStrokeSymbol[name];
            return null;
        }

        public bool LoadFromUrl(string url)
        {
            return true;
        }
    }
}
