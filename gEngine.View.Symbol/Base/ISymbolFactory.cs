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
        FillSymbol GetFillSymbol(string name);
    }
}
