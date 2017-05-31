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
        public Dictionary<string,PointSymbol> DicPointSymbol { get; set; }

        public string Url { get; set; }

        public GeSymbolFactory()
        {
            DicFillSymbol = new Dictionary<string, FillSymbol>();
            DicStrokeSymbol = new Dictionary<string, StrokeSymbol>();
            DicPointSymbol = new Dictionary<string, PointSymbol>();
            Init();
        }

        private void Init()
        {
            DicFillSymbol["GeFillSymbol"] = new GeFillSymbol();
            DicStrokeSymbol["GeStrokeSymbol"] = new GeStrokeSymbol();
            DicPointSymbol["GePointSymbol"] = new GePointSymbol();
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

            if (DicPointSymbol.ContainsKey(name))
                return DicPointSymbol[name];
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
