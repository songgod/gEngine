using DevExpress.Xpf.Ribbon;
using gEngine.Graph.Interface;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.Application
{
    public static class Registry
    {
        private static Dictionary<Type, GeRibbonPageCategory> dicRibbonPageCategory;
        private static Dictionary<string, RibbonPage> dicRibbonPage;
        public static Dictionary<Type, GeRibbonPageCategory> DicRibbonPageCategory
        {
            get
            {
                return dicRibbonPageCategory;
            }
        }
        public static Dictionary<string, RibbonPage> DicRibbonPage
        {
            get
            {
                return dicRibbonPage;
            }
        }

        static Registry()
        {
            dicRibbonPageCategory = new Dictionary<Type, GeRibbonPageCategory>();
            dicRibbonPage = new Dictionary<string, RibbonPage>();
            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private static void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
            IObject iobject = oc.DataContext as IObject;
            GeRibbonPageCategory grpc = GetRibbonPageCategory(iobject.GetType());
            if (grpc != null)
            {
                BindingExpression exp = grpc.GetBindingExpression(GeRibbonPageCategory.IsVisibleProperty);
                if (exp == null)
                {
                    //绑定1：将iobject的IsSelected属性绑定到ribbon的IsVisibleProperty属性上
                    Binding bd = new Binding("IsSelected");
                    bd.Source = iobject;
                    bd.Mode = BindingMode.OneWay;
                    grpc.SetBinding(RibbonPageCategory.IsVisibleProperty, bd);

                    //绑定2：将iobject绑定到ribbon的datacontext上
                    grpc.DataContext = iobject;
                }
            }
        }

        static public void Regist(Type type, GeRibbonPageCategory grpc)
        {
            if (type == null || grpc == null)
                return;

            dicRibbonPageCategory[type] = grpc;
        }
        static public void Regist(string name, RibbonPage rp)
        {
            if (string.IsNullOrEmpty(name) || rp == null)
                return;

            dicRibbonPage[name] = rp;
        }
        static public void UnRegist(Type type)
        {
            if (type == null)
                return;

            dicRibbonPageCategory.Remove(type);
        }
        static public void UnRegist(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            dicRibbonPage.Remove(name);
        }
        static public GeRibbonPageCategory GetRibbonPageCategory(Type type)
        {
            if (type == null || !dicRibbonPageCategory.ContainsKey(type))
                return null;

            return dicRibbonPageCategory[type];
            //if (type == null) return null;
            //while (!dicRibbonPageCategory.ContainsKey(type))
            //{
            //    Type baseType = type.BaseType;
            //    GetRibbonPageCategory(baseType);
            //}
            //return dicRibbonPageCategory[type];
        }

        static public RibbonPage GetRibbonPage(string name)
        {
            if (string.IsNullOrEmpty(name) || !dicRibbonPage.ContainsKey(name))
                return null;

            return dicRibbonPage[name];
        }
        static public void LoadLocalElement()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Application.";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type ribbonPageCategoryType = typeof(GeRibbonPageCategory);
                    if (ribbonPageCategoryType == t.BaseType)
                    {
                        GeRibbonPageCategory grpc = (GeRibbonPageCategory)(ab.CreateInstance(t.FullName));
                        PropertyInfo pi = t.GetRuntimeProperty("SupportType");
                        Type key = (Type)pi.GetValue(grpc);
                        Regist(key, grpc);
                    }

                    Type ribbonPageType = typeof(RibbonPage);
                    if (ribbonPageType == t.BaseType)
                    {
                        RibbonPage rp = (RibbonPage)(ab.CreateInstance(t.FullName));
                        Regist(rp.Name, rp);
                    }
                }
            }
        }
        static public void AddRibbonPageCategory(RibbonControl ribbonControl)
        {
            if (ribbonControl == null)
                return;
            foreach (var page in dicRibbonPageCategory)
            {
                ribbonControl.Categories.Add(page.Value);
            }

            foreach (var category in ribbonControl.Categories)
            {
                if (category.IsDefault)
                {
                    foreach (var page in dicRibbonPage.Values)
                    {
                        category.Pages.Insert(1,page);
                    }
                }
            }
            
        }
    }
}
