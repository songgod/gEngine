﻿using System;
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
        static private ResourceDictionary resDictionary;


        static Registry()
        {
            resDictionary = new ResourceDictionary();
        }

        static public void Regist(ResourceDictionary rd)
        {
            if (rd == null)
                return;
            if (!resDictionary.MergedDictionaries.Contains(rd))
                resDictionary.MergedDictionaries.Add(rd);
        }

        static public void UnRegist(ResourceDictionary rd)
        {
            if (rd == null)
                return;

            resDictionary.MergedDictionaries.Remove(rd);
        }

        static public DataTemplate GetDataTemplate(string dataTemplateName)
        {
            if (string.IsNullOrEmpty(dataTemplateName))
                return null;

            foreach (ResourceDictionary mergedResDic in resDictionary.MergedDictionaries)
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
                }
            }
            Application.Current.Resources.MergedDictionaries.Add(resDictionary);

        }



    }
}