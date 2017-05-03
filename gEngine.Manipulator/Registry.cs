﻿using gEngine.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator
{
    static public class Registry
    {
        static private Dictionary<string, IManipulatorFactory> dicManipulatorFactory;

        static Registry()
        {
            dicManipulatorFactory = new Dictionary<string, IManipulatorFactory>();
        }

        static public void Regist(string name, IManipulatorFactory mpf)
        {
            if (string.IsNullOrEmpty(name) || mpf == null)
                return;

            if(dicManipulatorFactory.ContainsKey(name))
            {
                Log.LogError("manipulator's name is already exist");
                return;
            }

            dicManipulatorFactory[name] = mpf;
        }

        static public void UnRegist(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            dicManipulatorFactory.Remove(name);
        }

        static public IManipulatorFactory GetManipulatorFactory(string name)
        {
            if (string.IsNullOrEmpty(name) || !dicManipulatorFactory.ContainsKey(name))
                return null;

            return dicManipulatorFactory[name];
        }

        static public IManipulatorBase CreateManipulator(string name, object param=null)
        {
            IManipulatorFactory mpf = GetManipulatorFactory(name);
            if (mpf == null)
                return null;

            return mpf.CreateManipulator(param);
        }

        static public void LoadManipulators()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Manipulator.";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                foreach (Type t in types)
                {
                    Type registertype = typeof(IManipulatorFactory);
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == registertype)
                        {
                            IManipulatorFactory mpf = (IManipulatorFactory)(ab.CreateInstance(t.FullName));
                            string name = mpf.Name;
                            Regist(name, mpf);
                        }
                    }
                }
            }
        }
    }
}