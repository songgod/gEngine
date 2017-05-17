using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge
{
    static class Registry
    {
        static public Dictionary<string,RWObjectBase> DicObjectRW { get; set; }
        static public Dictionary<string,RWLayerBase> DicLayerRW { get; set; }

        static Registry()
        {
            DicObjectRW = new Dictionary<string, RWObjectBase>();
            DicLayerRW = new Dictionary<string, RWLayerBase>();
            RegistObjRW(new RWWell());
            RegistObjRW(new RWWellLocation());
            RegistLayerRW(new RWLayerBase());
            RegistLayerRW(new RwSectionLayer());
        }

        static public void LoadLocalRW()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Graph.Rw.Ge.";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type type = typeof(RWLayerBase);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == type)
                        {
                            RWLayerBase bs = (RWLayerBase)(ab.CreateInstance(t.FullName));
                            RegistLayerRW(bs);
                        }
                    }
                }
            }
        }

        static public void RegistObjRW(RWObjectBase rw)
        {
            DicObjectRW.Add(rw.SupportType, rw);
        }

        static public void RegistLayerRW(RWLayerBase rw)
        {
            DicLayerRW.Add(rw.SupportType, rw);
        }

        static public RWObjectBase GetObjectRW(string type)
        {
            return DicObjectRW[type];
        }

        static public RWLayerBase GetLayerRW(string type)
        {
            return DicLayerRW[type];
        }
    }
}
