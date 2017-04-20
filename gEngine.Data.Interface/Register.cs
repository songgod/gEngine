using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    static public class Register
    {
        static private Dictionary<string, IDBFactoryCreater> dicfactorycreater;

        static Register()
        {
            dicfactorycreater = new Dictionary<string, IDBFactoryCreater>();
        }

        static public void Regist(string type, IDBFactoryCreater creater)
        {
            if (string.IsNullOrEmpty(type) || creater == null)
                return;

            dicfactorycreater[type] = creater;
        }

        static public void UnRegist(string type)
        {
            if (string.IsNullOrEmpty(type))
                return;

            dicfactorycreater.Remove(type);
        }

        static public IDBFactoryCreater GetCreater(string type)
        {
            if (string.IsNullOrEmpty(type) || !dicfactorycreater.ContainsKey(type))
                return null;

            return dicfactorycreater[type];
        }

        static public void LoadCreater()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Data";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type type = typeof(IDBFactoryCreater);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if(interf==type)
                        {
                            IDBFactoryCreater bs = (IDBFactoryCreater)(ab.CreateInstance(t.FullName));
                            Regist(bs.SupportType, bs);
                        }
                    }
                }
            }
        }

        static public IDBFactory CreateDBFactory(string info)
        {
            string type = info.Substring(info.LastIndexOf('.') + 1);
            string url = info.Substring(0, info.LastIndexOf('.'));
            IDBFactoryCreater c = GetCreater(type);
            if (c == null)
                return null;

            return c.CreateFactory(url);
        }
    }
}
