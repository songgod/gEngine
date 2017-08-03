﻿using gEngine.Graph.Interface;
using gEngine.Util;
using gEngine.View;
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
            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private static void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
            if (oc == null || oc.ObjectContext == null)
                return;
            if (oc.IsSelected)
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(oc.ObjectContext);
                ManipulatorSetter.SetManipulator(mb, oc);
            }
            else
            {
                ManipulatorSetter.ClearManipulator(oc);
            }
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

        static public IManipulatorFactory GetManipulatorFactory(IObject iobject)
        {
            foreach (IManipulatorFactory mpf in dicManipulatorFactory.Values)
            {
                if (mpf is IObjectManipulatorFactory)
                {
                    IObjectManipulatorFactory ompf = mpf as IObjectManipulatorFactory;
                    if (ompf.SupportIObjectType == iobject.GetType())
                    {
                        return ompf;
                    }
                }
            }
            return new EditDefaultManipulatorFactory();
        }

        static public IManipulatorBase CreateManipulator(IObject iobject, object param = null)
        {
            IManipulatorFactory mpf = GetManipulatorFactory(iobject);
            if (mpf == null)
                return null;

            return mpf.CreateManipulator(param);
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
                    if (t.IsInterface)
                        continue;
                    var interfaces = t.GetInterfaces();
                    foreach (var interf in interfaces)
                    {
                        if (interf == typeof(IManipulatorFactory))
                        {
                            IManipulatorFactory mpf = (IManipulatorFactory)(ab.CreateInstance(t.FullName));
                            Regist(mpf.Name, mpf);
                            break;
                        }                  
                    }
                }
            }
        }
    }
}
