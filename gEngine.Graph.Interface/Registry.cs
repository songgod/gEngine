using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Interface
{
    static public class Registry
    {
        static private Dictionary<string, IMapReadWriter> dicreadwriter;
        static private Dictionary<string, ILayerCreator> diclayercreator;
        static Registry()
        {
            dicreadwriter = new Dictionary<string, IMapReadWriter>();
            diclayercreator = new Dictionary<string, ILayerCreator>();
        }

        static public void Regist(IMapReadWriter rw)
        {
            if (rw == null || String.IsNullOrWhiteSpace(rw.SupportType))
                return;

            dicreadwriter[rw.SupportType] = rw;
        }

        static public void Regist(ILayerCreator c)
        {
            if (c == null || String.IsNullOrWhiteSpace(c.MapType))
                return;

            diclayercreator[c.MapType] = c;
        }

        static public void UnRegistMapReadWriter(string type)
        {
            if (string.IsNullOrEmpty(type))
                return;

            dicreadwriter.Remove(type);
        }

        static public void UnRegistLayerCreator(string type)
        {
            if (string.IsNullOrEmpty(type))
                return;

            diclayercreator.Remove(type);
        }

        static public IMapReadWriter GetMapReadWriter(string type)
        {
            if (string.IsNullOrEmpty(type) || !dicreadwriter.ContainsKey(type))
                return null;

            return dicreadwriter[type];
        }

        static public ILayerCreator GetLayerCreator(string type)
        {
            if (string.IsNullOrEmpty(type) || !diclayercreator.ContainsKey(type))
                return null;

            return diclayercreator[type];
        }

        static public void LoadMapReadWriter()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Graph.Rw";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type type = typeof(IMapReadWriter);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == type)
                        {
                            IMapReadWriter bs = (IMapReadWriter)(ab.CreateInstance(t.FullName));
                            Regist(bs);
                        }
                    }
                }
            }
        }

        static public void LoadLayerCreator()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Graph.Rw";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type type = typeof(ILayerCreator);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == type)
                        {
                            ILayerCreator bs = (ILayerCreator)(ab.CreateInstance(t.FullName));
                            Regist(bs);
                        }
                    }
                }
            }
        }

        static public IMap ReadMap(string url)
        {
            string type = url.Substring(url.LastIndexOf('.') + 1);
            IMapReadWriter rw = GetMapReadWriter(type);
            if (rw == null)
                return null;

            return rw.ReadMap(url);
        }

        static public bool WriteMap(IMap map, string url)
        {
            string type = url.Substring(url.LastIndexOf('.') + 1);
            IMapReadWriter rw = GetMapReadWriter(type);
            if (rw == null)
                return false;

            return rw.WriteMap(map,url);
        }

        static public IMap CreateMap(string type)
        {
            IMapReadWriter rw = GetMapReadWriter(type);
            if (rw == null)
                return null;

            return rw.CreateMap();
        }

        static public ILayer CreateLayer(string maptype, string layertype)
        {
            ILayerCreator lc = GetLayerCreator(maptype);
            if (lc == null)
                return null;

            return lc.CreateLayer(layertype);
        }

        static public List<String> GetLayerTypes(string maptype)
        {
            ILayerCreator lc = GetLayerCreator(maptype);
            if (lc == null)
                return null;
            return lc.LayerTypes;
        }
    }
}
