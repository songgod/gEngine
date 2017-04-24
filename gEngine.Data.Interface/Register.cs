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
        static private Dictionary<string, IDBSourceFactory> dicSourcefactory;

        static Register()
        {
            dicSourcefactory = new Dictionary<string, IDBSourceFactory>();
        }

        static public void Regist(string type, IDBSourceFactory creater)
        {
            if (string.IsNullOrEmpty(type) || creater == null)
                return;

            dicSourcefactory[type] = creater;
        }

        static public void UnRegist(string type)
        {
            if (string.IsNullOrEmpty(type))
                return;

            dicSourcefactory.Remove(type);
        }

        static public IDBSourceFactory GetDBFactory(string type)
        {
            if (string.IsNullOrEmpty(type) || !dicSourcefactory.ContainsKey(type))
                return null;

            return dicSourcefactory[type];
        }

        static public void LoadDBFactorys()
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
                    Type type = typeof(IDBSourceFactory);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if(interf==type)
                        {
                            IDBSourceFactory bs = (IDBSourceFactory)(ab.CreateInstance(t.FullName));
                            Regist(bs.SupportType, bs);
                        }
                    }
                }
            }
        }

        static public IDBSource CreateDBSource(string info)
        {
            string type = info.Substring(info.LastIndexOf('.') + 1);
            string url = info.Substring(0, info.LastIndexOf('.'));
            IDBSourceFactory c = GetDBFactory(type);
            if (c == null)
                return null;

            return c.CreateSource(url);
        }
    }
}
