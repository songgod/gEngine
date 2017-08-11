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

        static Registry()
        {
            dicreadwriter = new Dictionary<string, IMapReadWriter>();
        }

        static public void Regist(string type, IMapReadWriter rw)
        {
            if (string.IsNullOrEmpty(type) || rw == null)
                return;

            dicreadwriter[type] = rw;
        }

        static public void UnRegist(string type)
        {
            if (string.IsNullOrEmpty(type))
                return;

            dicreadwriter.Remove(type);
        }

        static public IMapReadWriter GetReadWriter(string type)
        {
            if (string.IsNullOrEmpty(type) || !dicreadwriter.ContainsKey(type))
                return null;

            return dicreadwriter[type];
        }

        static public void LoadReadWriter()
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
                            Regist(bs.SupportType, bs);
                        }
                    }
                }
            }
        }

        static public IMap ReadMap(string url)
        {
            string type = url.Substring(url.LastIndexOf('.') + 1);
            IMapReadWriter rw = GetReadWriter(type);
            if (rw == null)
                return null;

            return rw.ReadMap(url);
        }

        static public bool WriteMap(IMap map, string url)
        {
            string type = url.Substring(url.LastIndexOf('.') + 1);
            IMapReadWriter rw = GetReadWriter(type);
            if (rw == null)
                return false;

            return rw.WriteMap(map,url);
        }

        static public IMap CreateMap(string type)
        {
            IMapReadWriter rw = GetReadWriter(type);
            if (rw == null)
                return null;

            return rw.CreateMap();
        }
    }
}
