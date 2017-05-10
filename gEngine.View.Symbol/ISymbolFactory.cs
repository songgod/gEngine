using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.View.Symbol
{
    public interface ISymbolFactory
    {
        bool LoadFromUrl(string url);
        PointSymbol GetPointSymbol(string name);
        StrokeSymbol GetStrokeSymbol(string name);
        FillSymbol GetFillSymbol(string name);
    }
}
