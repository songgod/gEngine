using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace gEngine.View
{
    static public class Registry
    {
        static private Dictionary<string, DataTemplate> dictemplate;
        static private Dictionary<string, IValueConverter> dicValueConverter;
        static private Dictionary<string, IMultiValueConverter> dicMultiValueConverter;

        static Registry()
        {
            dictemplate = new Dictionary<string, DataTemplate>();
            dicValueConverter = new Dictionary<string, IValueConverter>();
            dicMultiValueConverter = new Dictionary<string, IMultiValueConverter>();
        }

        static public void Regist(string name, DataTemplate dt)
        {
            if (string.IsNullOrEmpty(name) || dt == null)
                return;

            dictemplate[name] = dt;
        }

        static public void UnRegistDataTemplate(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            dictemplate.Remove(name);
        }

        static public DataTemplate GetDataTemplate(string name)
        {
            if (string.IsNullOrEmpty(name) || !dictemplate.ContainsKey(name))
                return null;

            return dictemplate[name];
        }

        static public void Regist(string name, IValueConverter ivc)
        {
            if (string.IsNullOrEmpty(name) || ivc == null)
                return;

            dicValueConverter[name] = ivc;
        }

        static public void UnRegistValueConverter(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            dicValueConverter.Remove(name);
        }

        static public IValueConverter GetValueConverter(string name)
        {
            if (string.IsNullOrEmpty(name) || !dicValueConverter.ContainsKey(name))
                return null;

            return dicValueConverter[name];
        }

        static public void Regist(string name, IMultiValueConverter imvc)
        {
            if (string.IsNullOrEmpty(name) || imvc == null)
                return;

            dicMultiValueConverter[name] = imvc;
        }

        static public void UnRegistMultiValueConverter(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            dicMultiValueConverter.Remove(name);
        }

        static public IMultiValueConverter GetMultiValueConverter(string name)
        {
            if (string.IsNullOrEmpty(name) || !dicValueConverter.ContainsKey(name))
                return null;

            return dicMultiValueConverter[name];
        }

        static public void LoadLocalElement()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.View.";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    //Type valuetype = typeof(IValueConverter);
                    //Type mvaluetype = typeof(IMultiValueConverter);
                    //var interfaces = t.GetInterfaces();
                    //foreach (var interf in interfaces)
                    //{
                    //    //if (interf == valuetype)
                    //    //{
                    //    //    IValueConverter bs = (IValueConverter)(ab.CreateInstance(t.FullName));
                    //    //    Regist(bs.SupportType, bs);
                    //    //}
                    //}

                    Type resourceDictType = typeof(ResourceDictionary);

                    if (resourceDictType == t.BaseType)
                    {
                        ResourceDictionary resourceDict = (ResourceDictionary)(ab.CreateInstance(t.FullName));
                        Application.Current.Resources.MergedDictionaries.Add(resourceDict);

                        
                    }


                }
            }
        }



    }
}
