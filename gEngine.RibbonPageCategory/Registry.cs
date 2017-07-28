using DevExpress.Xpf.Ribbon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Application
{
    public static class Registry
    {
        private static Dictionary<Type, GeRibbonPageCategory> dicRibbonPage;
        public static Dictionary<Type, GeRibbonPageCategory> DicRibbonPage
        {
            get
            {
                return dicRibbonPage;
            }
        }

        static Registry()
        {
            dicRibbonPage = new Dictionary<Type, GeRibbonPageCategory>();
        }
        static public void Regist(Type type, GeRibbonPageCategory grpc)
        {
            if (type == null || grpc == null)
                return;

            dicRibbonPage[type] = grpc;
        }
        static public void UnRegist(Type type)
        {
            if (type == null)
                return;

            dicRibbonPage.Remove(type);
        }
        static public GeRibbonPageCategory GetRibbonPageCategory(Type type)
        {
            if (type == null || !dicRibbonPage.ContainsKey(type))
                return null;

            return dicRibbonPage[type];
        }
        static public void LoadLocalElement()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.RibbonPageCategory.";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type ribbonPageType = typeof(GeRibbonPageCategory);
                    if (ribbonPageType == t.BaseType)
                    {
                        GeRibbonPageCategory grpc = (GeRibbonPageCategory)(ab.CreateInstance(t.FullName));
                        PropertyInfo pi = t.GetRuntimeProperty("SupportType");
                        Type key = (Type)pi.GetValue(grpc);
                        Regist(key, grpc);
                    }
                }
            }
        }
        static public void AddRibbonPageCategory(RibbonControl ribbonControl)
        {
            if (ribbonControl == null)
                return;
            foreach (var page in dicRibbonPage)
            {
                ribbonControl.Categories.Add(page.Value);
            }
        }
    }
}
