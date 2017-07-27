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
        public Dictionary<string,FillSymbol> DicFillSymbol { get; set; }
        public Dictionary<string,StrokeSymbol> DicStrokeSymbol { get; set; }
        public Dictionary<string, PointSymbol> DicPointSymbol { get; set; }
        public Dictionary<string, LineSymbol> DicLineSymbol { get; set; }
        public string Url { get; set; }

        public List<string> PointSymbolNames { get; set; }

        public List<string> LineSymbolNames { get; set; }
        public GeSymbolFactory()
        {
            PointSymbolNames = new List<string>();
            LineSymbolNames = new List<string>();
            DicFillSymbol = new Dictionary<string, FillSymbol>();
            DicStrokeSymbol = new Dictionary<string, StrokeSymbol>();
            DicPointSymbol = new Dictionary<string, PointSymbol>();
            DicLineSymbol = new Dictionary<string, LineSymbol>();

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

            if (DicPointSymbol.ContainsKey(name))
                return DicPointSymbol[name];
            return null;
        }
        public LineSymbol GetLineSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (DicLineSymbol.ContainsKey(name))
                return DicLineSymbol[name];
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
            string ext = url.Substring(url.LastIndexOf('.') + 1);
            string libpath = Directory.GetCurrentDirectory() + "\\gEngine.Symbol." + ext + ".dll";
            try
            {
                Assembly ab = Assembly.LoadFrom(libpath);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type type = typeof(PointSymbol);
                    if (type == t.BaseType)
                    {
                        PointSymbol ps = (PointSymbol)(ab.CreateInstance(t.FullName));
                        DicPointSymbol[ps.Name] = ps;
                        PointSymbolNames.Add(ps.Name);
                    }

                    type = typeof(LineSymbol);
                    if (type == t.BaseType)
                    {
                        LineSymbol ps = (LineSymbol)(ab.CreateInstance(t.FullName));
                        DicLineSymbol[ps.Name] = ps;
                        LineSymbolNames.Add(ps.Name);
                    }
                }
            }
            catch (Exception)
            {
                Log.LogWarning("load symbol plugin " + libpath + "failed!");
            }

            return true;
        }
    }
}
