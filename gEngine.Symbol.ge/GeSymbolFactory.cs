using gEngine.Symbol;
using gEngine.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Symbol.gesym
{
    public class GeSymbolFactory : ISymbolFactory
    {
        public Dictionary<string, FillSymbol> DicFillSymbol { get; set; }
        public Dictionary<string, StrokeSymbol> DicStrokeSymbol { get; set; }
        public Dictionary<string, PointSymbol> DicPointSymbol { get; set; }
        public string Url { get; set; }

        public List<string> PointSymbolNames
        {
            get
            {
                return DicPointSymbol.Keys.ToList();
            }
            set
            {
            }
        }

        public List<string> StrokeSymbolNames
        {
            get
            {
                return DicStrokeSymbol.Keys.ToList();
            }
            set
            {
            }
        }

        public List<string> FillSymbolNames
        {
            get
            {
                return DicFillSymbol.Keys.ToList();
            }
            set
            {
            }
        }


        public GeSymbolFactory()
        {
            DicFillSymbol = new Dictionary<string, FillSymbol>();
            DicStrokeSymbol = new Dictionary<string, StrokeSymbol>();
            DicPointSymbol = new Dictionary<string, PointSymbol>();
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
            DicFillSymbol["GeFillSymbol"] = new GeFillSymbol();
            DicStrokeSymbol["WavyLineSymbol"] = new GeWavyLineSymbol();
            DicStrokeSymbol["GeStoreLineSymbol"] = new GeStoreLineSymbol();
            DicPointSymbol["GeEllipsePointSymbol"] = new GeEllipsePointSymbol();
            DicPointSymbol["GeRectanglePointSymbol"] = new GeRectanglePointSymbol();
            return true;
        }
    }
}
