using gEngine.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
            string libpath = Directory.GetCurrentDirectory() + "\\gEngine.Symbol." + ext+".dll";
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
            if (string.IsNullOrEmpty(url) || System.IO.File.Exists(url)==false)
                return;

            string filename = System.IO.Path.GetFileNameWithoutExtension(url);

            if (SymbolFactorys.ContainsKey(filename))
            {
                Log.LogWarning("Already has symbol factory "+filename);
                return;
            }

            string ext = url.Substring(url.LastIndexOf('.') + 1);

            ISymbolFactory factory = LoadSymbolFactory(ext);
            if (factory == null)
                return;
            if (factory.LoadFromUrl(url) == false)
                return;
            factory.Url = url;
            SymbolFactorys[filename] = factory;
        }

        static public void UnRegister(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            SymbolFactorys.Remove(name);
        }

        static public void LoadLocalSymbols()
        {
            string dir = Directory.GetCurrentDirectory()+"\\Symbols\\";
            var files = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                Register(item);
            }
        }

        static public PointSymbol GetPointSymbol(string factoryname, string symbolname)
        {
            if (string.IsNullOrEmpty(factoryname) || string.IsNullOrEmpty(symbolname))
                return null;

            if (SymbolFactorys.ContainsKey(factoryname) == false)
                return null;

            ISymbolFactory symbolfactory = SymbolFactorys[factoryname];
            if (symbolfactory == null)
                return null;

            PointSymbol psym = symbolfactory.GetPointSymbol(symbolname);
            if (psym == null)
                return null;

            return psym;
        }

        static public StrokeSymbol GetStrokeSymbol(string factoryname, string symbolname)
        {
            if (string.IsNullOrEmpty(factoryname) || string.IsNullOrEmpty(symbolname))
                return null;

            if (SymbolFactorys.ContainsKey(factoryname) == false)
                return null;

            ISymbolFactory symbolfactory = SymbolFactorys[factoryname];
            if (symbolfactory == null)
                return null;

            StrokeSymbol ssym = symbolfactory.GetStrokeSymbol(symbolname);
            if (ssym == null)
                return null;

            return ssym;
        }

        static public FillSymbol GetFillSymbol(string factoryname, string symbolname)
        {
            if (string.IsNullOrEmpty(factoryname) || string.IsNullOrEmpty(symbolname))
                return null;

            if (SymbolFactorys.ContainsKey(factoryname) == false)
                return null;

            ISymbolFactory symbolfactory = SymbolFactorys[factoryname];
            if (symbolfactory == null)
                return null;

            FillSymbol fsym = symbolfactory.GetFillSymbol(symbolname);
            if (fsym == null)
                return null;

            return fsym;
        }

        static public object CreatePoint(OptionSetting param)
        {
            if (param==null)
                return DefaultPointSymbol.Create(param);

            if (string.IsNullOrEmpty(param.Factory) || string.IsNullOrEmpty(param.Symbol))
                return DefaultPointSymbol.Create(param);

            string factoryname = param.Factory;
            string symbolname = param.Symbol;
            
            PointSymbol psym = GetPointSymbol(factoryname, symbolname);
            if (psym == null)
                return DefaultPointSymbol.Create(param);

            object res = psym.Create(param);
            if(res==null)
                return DefaultPointSymbol.Create(param);

            return res;
        }

        static public object CreateStroke(LineOptionSetting param)
        {
            if (param == null ||
                string.IsNullOrEmpty(param.Factory) ||
                string.IsNullOrEmpty(param.Symbol) || param.Path == null)
                return StrokePathUtil.GetAfterConverterGeom(DefaultStrokeSymbol.SymbolGeometry, param);

            string factoryname = param.Factory;
            string symbolname = param.Symbol;

            StrokeSymbol ssym = GetStrokeSymbol(factoryname, symbolname);
            
            if (ssym == null)
                return StrokePathUtil.GetAfterConverterGeom(DefaultStrokeSymbol.SymbolGeometry, param);

            object res = StrokePathUtil.GetAfterConverterGeom(ssym.SymbolGeometry,param);
            if (res == null)
                return StrokePathUtil.GetAfterConverterGeom(DefaultStrokeSymbol.SymbolGeometry, param);

            return res;
        }

        static public Brush CreateFillBrush(OptionSetting param)
        {
            if (param == null)
                return DefaultFillSymbol.Create(param);

            if (string.IsNullOrEmpty(param.Factory) || string.IsNullOrEmpty(param.Symbol))
                return DefaultFillSymbol.Create(param);

            string factoryname = param.Factory;
            string symbolname = param.Symbol;

            FillSymbol fsym = GetFillSymbol(factoryname, symbolname);
            if (fsym == null)
                return DefaultFillSymbol.Create(param);

            Brush res = fsym.Create(param);
            if(res==null)
                return DefaultFillSymbol.Create(param);
            return res;
        }
    }
}
