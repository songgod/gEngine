using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Graph.Ge;
using gEngine.Graph.Rw.Ge;
using gEngine.Util;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using gEngine.Graph.Interface;

namespace gEngine.Graph.Tpl.Ge
{
    static public class Registry
    {
        static Registry()
        {
            Templates = new List<string>();
        }

        static public List<string> Templates;

        static public string TplsPath
        {
            get
            {
                string dir = Directory.GetCurrentDirectory();
                return dir + @"\Templates";
            }
        }

        static public bool SaveTemplate(Graph.Ge.Object obj, string name)
        {
            if (obj == null || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return false;

            Type type = obj.GetType();

            string directoryname = Path.GetDirectoryName(name);
            string filename = Path.GetFileName(name);
            string fulltplname = directoryname + type.Name + "." + filename;

            if (!CheckSave(fulltplname))
                return false;

            gEngine.Graph.Rw.Ge.Registry.LoadLocalRW();
            RWObjectBase objectrw = gEngine.Graph.Rw.Ge.Registry.GetObjectRW(type.Name);
            if (objectrw == null)
            {
                Log.LogWarning("Cound not find " + type.Name + " object readerwriter");
                return false;
            }

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlElement xmlNode = xmldoc.CreateElement(type.Name);
            xmldoc.AppendChild(xmlNode);
            objectrw.Write(xmlNode, obj);

            xmldoc.Save(fulltplname);
            return true;
        }

        static public Graph.Ge.Object GetTemplate(Type type, string name)
        {
            if (type == null || string.IsNullOrEmpty(name))
                return null;

            gEngine.Graph.Rw.Ge.Registry.LoadLocalRW();
            RWObjectBase objectrw = gEngine.Graph.Rw.Ge.Registry.GetObjectRW(type.Name);

            if (objectrw == null)
            {
                Log.LogWarning("Cound not find " + type.Name + " object readerwriter");
                return null;
            }

            string tplName = TplsPath + @"\" + name;

            XmlDocument xmldoc = new XmlDocument();

            XmlReaderSettings setting = new XmlReaderSettings() { IgnoreComments = true };
            XmlReader reader = XmlReader.Create(tplName, setting);
            xmldoc.Load(reader);
            XmlNode xmlNode = xmldoc.SelectSingleNode(type.Name);

            IObject obj = objectrw.CreateObject();
            objectrw.Read(obj, xmlNode);
            if (obj == null)
                return null;

            return obj as gEngine.Graph.Ge.Object;
        }

        static public List<string> GetTemplateNames(Type type)
        {
            if (type == null)
                return null;

            List<string> tplList = new List<string>();
            string tplType = type.Name;
            var tpl = Templates.Where(x => x.StartsWith(tplType + "."));
            if (tpl.Count() > 0)
            {
                foreach (string tplName in tpl)
                {
                    tplList.Add(tplName);
                }
            }
            return tplList;
        }

        static public void LoadTemplate()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(TplsPath);

            foreach (FileInfo fileInfo in dirInfo.GetFiles("*.tpl"))
            {
                Templates.Add(fileInfo.Name);
            }
        }

        static private bool CheckSave(string url)
        {
            if (File.Exists(url))
            {
                MessageBox.Show("数据模板目录重复！");
                return false;
            }
            return true;
        }
    }
}
