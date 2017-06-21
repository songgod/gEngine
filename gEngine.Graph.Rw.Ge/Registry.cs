using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge
{
    public static class Registry
    {
        static public Dictionary<string, RWObjectBase> DicObjectRW { get; set; }
        static public Dictionary<string, RWLayerBase> DicLayerRW { get; set; }

        static Registry()
        {
            DicObjectRW = new Dictionary<string, RWObjectBase>();
            DicLayerRW = new Dictionary<string, RWLayerBase>();
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
                    Type objecttype = typeof(RWObjectBase);

                    if (t != objecttype)
                    {
                        if (FindChildClass(t) == objecttype)
                        {
                            RWObjectBase bs = (RWObjectBase) (ab.CreateInstance(t.FullName));
                            RegistObjRW(bs);
                        }
                    }
                }
            }
        }

        public static Type FindChildClass(Type t)
        {
            var parent = t.BaseType;
            if (parent.Name != "Object")
            {
                return FindChildClass(parent);
            }
            else
                return t;
        }

        static public void RegistObjRW(RWObjectBase rw)
        {
            if (DicObjectRW.ContainsKey(rw.SupportType))
                return;
            DicObjectRW.Add(rw.SupportType, rw);
        }

        static public void RegistLayerRW(RWLayerBase rw)
        {
            if (DicLayerRW.ContainsKey(rw.SupportType))
                return;
            DicLayerRW.Add(rw.SupportType, rw);
        }

        static public RWObjectBase GetObjectRW(string type)
        {
            if (!DicObjectRW.ContainsKey(type))
                return null;
            return DicObjectRW[type];
        }

        static public RWLayerBase GetLayerRW(string type)
        {
            if (!DicLayerRW.ContainsKey(type))
                return null;
            return DicLayerRW[type];
        }
    }
}
