using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Graph.Ge;

namespace gEngine.Graph.Tpl.Ge
{
    static public class Registry
    {
        static public List<string> Templates;

        static public bool SaveTemplate(Graph.Ge.Object obj, string name)
        {
            if (obj == null || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return false;

            Type type = obj.GetType();
            string filename = type.Name + "." + name + ".tpl";

            return true;
        }

        static public Graph.Ge.Object GetTemplate(Type type, string name)
        {
            return null;
        }

        static public List<string> GetTemplateNames(Type type)
        {
            return null;
        }

        static public void LoadTemplate()
        {
            //遍历文件夹下的tpl文件
        }
    }
}
