using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Symbol
{
    public interface ISymbolFactory
    {
        bool LoadFromUrl(string url);
        string Url { get; set; }
        PointSymbol GetPointSymbol(string name);
        StrokeSymbol GetStrokeSymbol(string name);
        LineSymbol GetLineSymbol(string name);
        FillSymbol GetFillSymbol(string name);
        PointSymbols PointSymbols { get; set; }

        LineSymbols LineSymbols { get; set; }
    }


    public class PointSymbols : Dictionary<string, PointSymbol> { }

    public class LineSymbols : Dictionary<string, LineSymbol> { }
}
