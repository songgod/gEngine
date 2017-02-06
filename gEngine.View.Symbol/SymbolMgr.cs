using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.View.Symbol
{
    public static class SymbolMgr
    {
        static private Dictionary<string, ISymbol> symbols;
        static private DefaultPointSymbol defaultpointsymbol;
        static private DefaultStrokeSymbol defaultstrokesymbol;
        static private DefaultFillSymbol defaultfillsymbol;

        static SymbolMgr()
        {
            symbols = new Dictionary<string, ISymbol>();
            defaultpointsymbol = new DefaultPointSymbol();
            defaultstrokesymbol = new DefaultStrokeSymbol();
            defaultfillsymbol = new DefaultFillSymbol();
        }
        
        static public void Register(string name, ISymbol symbol)
        {
            if (string.IsNullOrEmpty(name) || symbol == null)
                return;

            if (symbols.ContainsKey(name))
                throw new Exception("register symbol is exist");
            symbols[name] = symbol;
        }

        static public void UnRegister(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            symbols.Remove(name);
        }

        static public ISymbol GetDefaultSymbol(string type)
        {
            if (type == PointSymbol.type)
                return defaultpointsymbol;
            else if (type == StrokeSymbol.type)
                return defaultstrokesymbol;
            else if (type == FillSymbol.type)
                return defaultfillsymbol;
            return null;
        }

        static public PointSymbol GetPointSymbol(string name)
        {
            if (string.IsNullOrEmpty(name) || !symbols.ContainsKey(name))
                return defaultpointsymbol;

            ISymbol symbol = symbols[name];
            if (symbol == null || symbol.Type != PointSymbol.type)
                return defaultpointsymbol;

            return symbol as PointSymbol;
        }

        static public StrokeSymbol GetStrokeSymbol(string name)
        {
            if (string.IsNullOrEmpty(name) || !symbols.ContainsKey(name))
                return defaultstrokesymbol;

            ISymbol symbol = symbols[name];
            if (symbol == null || symbol.Type != StrokeSymbol.type)
                return defaultstrokesymbol;

            return symbol as StrokeSymbol;
        }

        static public FillSymbol GetFillSymbol(string name)
        {
            if (string.IsNullOrEmpty(name) || !symbols.ContainsKey(name))
                return defaultfillsymbol;

            ISymbol symbol = symbols[name];
            if (symbol == null || symbol.Type != FillSymbol.type)
                return defaultfillsymbol;

            return symbol as FillSymbol;
        }

        static public ISymbol GetSymbol(string name, string type)
        {
            if (string.IsNullOrEmpty(name) || !symbols.ContainsKey(name))
                return GetDefaultSymbol(type);
            ISymbol symbol = symbols[name];
            if (symbol == null || symbol.Type!=type)
                return GetDefaultSymbol(type);

            return symbol;
        }

        static public List<string> GetSymbolNames()
        {
            return symbols.Keys.ToList();
        }

        static public List<string> GetSymbolNames(string type)
        {
            return symbols.Select(v => v.Key).Where(v => (v == type)).ToList();
        }
    }
}
