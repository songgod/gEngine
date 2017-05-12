using gEngine.Project.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Project
{
    static public class Registry
    {
        public static Dictionary<string, CommandInstaller> DicCmdInstallers;

        static Registry()
        {
            DicCmdInstallers = new Dictionary<string, CommandInstaller>();
        }

        public static void Regist(CommandInstaller ins)
        {
            if (ins == null)
                return;

            if (DicCmdInstallers.ContainsKey(ins.Name))
                return;

            DicCmdInstallers[ins.Name] = ins;
        }

        public static void UnRegist(string name)
        {
            if (DicCmdInstallers.ContainsKey(name) == false)
                return;

            DicCmdInstallers.Remove(name);
        }

        static public void LoadLocalElement()
        {
            string dir = Directory.GetCurrentDirectory();
            string qstr = dir + "\\gEngine.Project";
            var files = Directory.GetFiles(dir, "*.dll", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(qstr));
            foreach (var item in files)
            {
                Assembly ab = Assembly.LoadFrom(item);
                Type[] types = ab.GetTypes();
                Type installtype = typeof(CommandInstaller);
                foreach (Type t in types)
                {
                    if (t.BaseType == installtype)
                    {
                        CommandInstaller cmd = (CommandInstaller)ab.CreateInstance(t.FullName);
                        Regist(cmd);
                    }
                }
            }
        }

        static public void InstallCommands(UIElement ui)
        {
            if (ui == null)
                return;
            foreach (var ins in DicCmdInstallers)
            {
                ins.Value.Install(ui);
            }
        }
    }
}
