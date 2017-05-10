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
        static public ResourceDictionary ResDictionary;
        static public Dictionary<string, Dictionary<string, IToolBase>> DicTools { get; set; }

        static Registry()
        {
            ResDictionary = new ResourceDictionary();
            DicTools = new Dictionary<string, Dictionary<string, IToolBase>>();
        }

        static public void Regist(ResourceDictionary rd)
        {
            if (rd == null)
                return;
            if (!ResDictionary.MergedDictionaries.Contains(rd))
                ResDictionary.MergedDictionaries.Add(rd);
        }

        static public void UnRegist(ResourceDictionary rd)
        {
            if (rd == null)
                return;

            ResDictionary.MergedDictionaries.Remove(rd);
        }

        static public bool Regist(IToolBase tool)
        {
            if (tool == null || string.IsNullOrEmpty(tool.NameSpace) || string.IsNullOrEmpty(tool.Name))
                return false;

            Dictionary<string, IToolBase> dic = null;
            if (false == DicTools.ContainsKey(tool.NameSpace))
            {
                dic = new Dictionary<string, IToolBase>();
                DicTools[tool.NameSpace] = dic;
            }
            else
                dic = DicTools[tool.NameSpace];

            if (dic.ContainsKey(tool.Name))
                return false;

            dic[tool.Name] = tool;
            return true;
        }

        static public bool UnRegist(IToolBase tool)
        {
            if (tool == null || string.IsNullOrEmpty(tool.NameSpace) || string.IsNullOrEmpty(tool.Name))
                return false;

            if (false == DicTools.ContainsKey(tool.NameSpace))
                return false;

            string nspace = tool.NameSpace;
            Dictionary<string, IToolBase> dic = DicTools[nspace];
            if (false == dic.ContainsKey(tool.Name))
                return false;

            dic.Remove(tool.Name);
            if (dic.Count == 0)
                DicTools.Remove(nspace);

            return true;
        }

        static public DataTemplate GetDataTemplate(string dataTemplateName)
        {
            if (string.IsNullOrEmpty(dataTemplateName))
                return null;

            foreach (ResourceDictionary mergedResDic in ResDictionary.MergedDictionaries)
            {
                foreach (var v in mergedResDic.Values)
                {
                    if (v.GetType() == typeof(DataTemplate))
                    {
                        DataTemplate dataTemplate = v as DataTemplate;
                        if (dataTemplate.DataTemplateKey.ToString() == dataTemplateName)
                            return dataTemplate;
                    }
                }
            }
            return null;
        }

        static public IToolBase GetTool(string NameSpace, string Name)
        {
            if (string.IsNullOrEmpty(NameSpace) || string.IsNullOrEmpty(Name))
                return null;

            if (false == DicTools.ContainsKey(NameSpace))
                return null;

            Dictionary<string, IToolBase> dic = DicTools[NameSpace];
            if (false == dic.ContainsKey(Name))
                return null;

            return dic[Name];
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
                    Type resourceDictType = typeof(ResourceDictionary);
                    if (resourceDictType == t.BaseType)
                    {
                        ResourceDictionary resourceDict = (ResourceDictionary)(ab.CreateInstance(t.FullName));
                        Regist(resourceDict);
                    }
                    Type toolType = typeof(IToolBase);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == toolType)
                        {
                            IToolBase tb = (IToolBase)(ab.CreateInstance(t.FullName));
                            Regist(tb);
                        }
                    }
                }
            }
            Application.Current.Resources.MergedDictionaries.Add(ResDictionary);

        }
    }
}
