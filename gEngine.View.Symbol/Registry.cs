using gEngine.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Symbol
{
    public static class Registry
    {
        static public Dictionary<string, ISymbolFactory> SymbolFactorys { get; set; }
        static public PointSymbol DefaultPointSymbol { get; set; }
        static public StrokeSymbol DefaultStrokeSymbol { get; set; }
        static public FillSymbol DefaultFillSymbol { get; set; }

        static Registry()
        {
            SymbolFactorys = new Dictionary<string, ISymbolFactory>();
            DefaultPointSymbol = new DefaultPointSymbol();
            DefaultStrokeSymbol = new DefaultStrokeSymbol();
            DefaultFillSymbol = new DefaultFillSymbol();
        }

        static public ISymbolFactory LoadSymbolFactory(string ext)
        {
            string libpath = Directory.GetCurrentDirectory() + "/" + "gEngine.Symbol." + ext;
            try
            {
                Assembly ab = Assembly.LoadFrom(libpath);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type type = typeof(ISymbolFactory);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == type)
                        {
                            ISymbolFactory bs = (ISymbolFactory)(ab.CreateInstance(t.FullName));
                            return bs;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Log.LogWarning("load symbol plugin " + libpath + "failed!");
            }
            return null;
        }
        
        static public void Register(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            if (SymbolFactorys.ContainsKey(url))
                return;

            string ext = url.Substring(url.LastIndexOf('.') + 1);
            ISymbolFactory factory = LoadSymbolFactory(ext);
            if (factory == null)
                return;
            if (factory.LoadFromUrl(url) == false)
                return;

            SymbolFactorys[url] = factory;
            SaveConfig();
        }

        static public void UnRegister(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;
            SymbolFactorys.Remove(url);
            SaveConfig();
        }

        static public ISymbol GetDefaultSymbol(string type)
        {
            if (type == PointSymbol.type)
                return DefaultPointSymbol;
            else if (type == StrokeSymbol.type)
                return DefaultStrokeSymbol;
            else if (type == FillSymbol.type)
                return DefaultFillSymbol;
            return null;
        }

        static public PointSymbol GetPointSymbol(string facotryname, string symbolname)
        {
            if (string.IsNullOrEmpty(facotryname) || string.IsNullOrEmpty(symbolname) || !SymbolFactorys.ContainsKey(facotryname))
                return DefaultPointSymbol;

            ISymbolFactory symbolfactory = SymbolFactorys[facotryname];
            if (symbolfactory == null)
                return DefaultPointSymbol;

            PointSymbol psym = symbolfactory.GetPointSymbol(symbolname);
            if (psym == null)
                return DefaultPointSymbol;

            return psym;
        }

        static public StrokeSymbol GetStrokeSymbol(string facotryname, string symbolname)
        {
            if (string.IsNullOrEmpty(facotryname) || string.IsNullOrEmpty(symbolname) || !SymbolFactorys.ContainsKey(facotryname))
                return DefaultStrokeSymbol;

            ISymbolFactory symbolfactory = SymbolFactorys[facotryname];
            if (symbolfactory == null)
                return DefaultStrokeSymbol;

            StrokeSymbol ssym = symbolfactory.GetStrokeSymbol(symbolname);
            if (ssym == null)
                return DefaultStrokeSymbol;

            return ssym;
        }

        static public FillSymbol GetFillSymbol(string facotryname, string symbolname)
        {
            if (string.IsNullOrEmpty(facotryname) || string.IsNullOrEmpty(symbolname) || !SymbolFactorys.ContainsKey(facotryname))
                return DefaultFillSymbol;

            ISymbolFactory symbolfactory = SymbolFactorys[facotryname];
            if (symbolfactory == null)
                return DefaultFillSymbol;

            FillSymbol fsym = symbolfactory.GetFillSymbol(symbolname);
            if (fsym == null)
                return DefaultFillSymbol;

            return fsym;
        }

        static public void LoadSymbols()
        {
            string configfile = Directory.GetCurrentDirectory() + "\\symbol.config";
            System.IO.StreamReader reader = new StreamReader(configfile, Encoding.Default);
            while (reader.Peek() > 0)
            {
                string curLine = reader.ReadLine();
                Register(curLine);
            }
        }

        static public void SaveConfig()
        {
            string configfile = Directory.GetCurrentDirectory() + "\\symbol.config";
            System.IO.StreamWriter writer = new StreamWriter(configfile, false,Encoding.Default);
            foreach (var item in SymbolFactorys)
            {
                writer.WriteLine(item.Key);
            }
            writer.Close();
        }
    }
}
