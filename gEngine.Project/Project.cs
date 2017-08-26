using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace gEngine.Project
{
    public class Project
    {
        public string Url { get; set; }

        public string MapsPath
        {
            get
            {
                if (string.IsNullOrEmpty(Url))
                    return null;

                return Url.Substring(0, Url.LastIndexOf(@"\")) + @"\Maps";
            }
        }

        public class MapCollection : ObservedCollection<Tuple<string, IMap>> { }
        public MapCollection Maps { get; private set; }

        public IMaps OpenMaps { get; set; }

        public class DBSourceTuple : Tuple<string, IDBSource>
        {
            public DBSourceTuple(string url, IDBSource db) : base(url, db)
            {
            }
        }
        public DBSourceTuple DBTuple { get; set; }

        public IDBSource DBSource
        {
            get
            {
                if (DBTuple != null)
                    return DBTuple.Item2;
                return null;
            }
        }

        public Project()
        {
            Maps = new MapCollection();
            OpenMaps = new IMaps();
            Maps.OnItemRemoved += Maps_OnItemRemoved;
            OpenMaps.OnItemRemoved += OpenMaps_OnItemRemoved;
        }

        public bool Open(string url)
        {
            Url = url;
            return read();
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(Url))
            {
                SaveFileDialog SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.Filter = "工程文件|*.Gepro";
                if (SaveFileDialog.ShowDialog() == true)
                {
                    if (!CheckSave(SaveFileDialog.FileName))
                        return false;

                    Url = SaveFileDialog.FileName;
                    DirectoryInfo directoryInfo = new DirectoryInfo(MapsPath);
                    directoryInfo.Create();

                    return Write();
                }
                return false;
            }
            else
            {
                return Write();
            }
        }

        public bool SaveAs(string url)
        {
            if (!CheckSave(url))
                return false;

            Url = url;
            DirectoryInfo directoryInfo = new DirectoryInfo(MapsPath);
            directoryInfo.Create();
            
            return Write();
        }

        public bool OpenDBSource(string url)
        {
            IDBSource source = gEngine.Data.Interface.Register.CreateDBSource(url);
            if (source == null)
                return false;
            DBTuple = new Project.DBSourceTuple(url, source);
            return true;
        }

        public IMap OpenMap(string url)
        {
            string fullurl = "";
            IMap map = null;
            for (int i = 0; i < Maps.Count; ++i)
            {
                if (Maps[i].Item1 == url)
                {
                    if (Maps[i].Item2 != null)//已经打开
                        return Maps[i].Item2;
                    else
                    {
                        fullurl = GetMapFullPath(Maps[i].Item1);
                        map = gEngine.Graph.Interface.Registry.ReadMap(fullurl);
                        if (map == null)
                            return null;

                        Maps[i] = new Tuple<string, IMap>(url, map);
                        OpenMaps.Add(map);
                        return map;
                    }
                }
            }

            fullurl = GetMapFullPath(url);
            map = gEngine.Graph.Interface.Registry.ReadMap(fullurl);
            if (map == null)
                return null;
            Maps.Add(new Tuple<string, IMap>(url, map));
            OpenMaps.Add(map);
            return map;
        }

        public IMap NewMap(string type, string name)
        {
            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(name))
                return null;
            string url = name + "." + type;
            var res = Maps.Where(item => item.Item1 == url);
            if (res.Count() != 0)
            {
                MessageBox.Show("存在同名图件");
                return null;
            }


            IMap map = gEngine.Graph.Interface.Registry.CreateMap(type);
            if (map == null)
                return null;
            map.Name = name;
            Maps.Add(new Tuple<string, IMap>(url, map));
            OpenMaps.Add(map);
            return map;
        }

        public void ActiveMap(IMap map)
        {
            for (int i = 0; i < OpenMaps.Count; i++)
            {
                if(OpenMaps[i]==map)
                {
                    OpenMaps.CurrentIndex = i;
                    break;
                }
            }
        }

        public IMap GetMap(int i)
        {
            return OpenMaps.CurrentMap;
        }

        public IMap GetActiveMap()
        {
            int active = OpenMaps.CurrentIndex;
            return GetMap(active);
        }

        public bool SaveMap(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            var res = Maps.Where(item => item.Item1 == url);
            if (res.Count() == 0)
                return false;

            Tuple<string, IMap> tuplemap = res.ElementAt(0);

            string fullurl = GetMapFullPath(url);

            bool IsSuccess = gEngine.Graph.Interface.Registry.WriteMap(tuplemap.Item2, fullurl);
            return IsSuccess;
        }

        public bool SaveAllMaps()
        {
            if (string.IsNullOrEmpty(Url))
                return false;

            foreach (var item in Maps)
            {
                SaveMap(item.Item1);
            }
            return true;
        }

        public bool DeleteMap(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            if (string.IsNullOrEmpty(Url))
                return false;

            var targetMap = Maps.Where(x => x.Item1.Equals(url));
            if (targetMap.Count().Equals(0))
                return false;

            Maps.Remove(targetMap.ElementAt(0));
            string fullurl = GetMapFullPath(url);
            File.Delete(fullurl);
            Write();
            return true;
        }

        private string GetMapFullPath(string url)
        {
            if (url.Contains('\\') || url.Contains('/'))
            {
                return url;
            }
            else
            {
                return MapsPath + @"\" + url;
            }
        }

        private bool CheckSave(string url)
        {
            if (File.Exists(url))
            {
                MessageBox.Show("工程文件名称重复，请重新输入！");
                return false;
            }
            string ProjPath = url.Substring(0, url.LastIndexOf(@"\"));
            string MapsPath = ProjPath + @"\Maps";
            if (Directory.Exists(MapsPath))
            {
                MessageBox.Show("图件目录重复！");
                return false;
            }
            return true;
        }

        private bool Write()
        {
            if (string.IsNullOrEmpty(Url))
                return false;
            SaveAllMaps();

            string ProjPath = Url.Substring(0, Url.LastIndexOf(@"\"));
            string MapsPath = ProjPath + @"\Maps";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlElement xmlproj = xmldoc.CreateElement("Gepro");
            xmldoc.AppendChild(xmlproj);
            XmlElement xmlmaps = xmlproj.OwnerDocument.CreateElement("Maps");
            xmlproj.AppendChild(xmlmaps);

            for (int index = 0; index < Maps.Count; index++)
            {
                XmlElement xmlmap = xmldoc.CreateElement("Map");
                xmlmap.SetAttribute("Url", Maps[index].Item1);
                xmlmaps.AppendChild(xmlmap);
            }
            XmlElement xmldbsource = xmlproj.OwnerDocument.CreateElement("DBSource");
            xmldbsource.SetAttribute("Url", DBTuple.Item1);
            xmlproj.AppendChild(xmldbsource);
            xmldoc.Save(Url);
            return true;
        }

        private bool read()
        {
            XmlDocument xmldoc = new XmlDocument();

            XmlReaderSettings setting = new XmlReaderSettings() { IgnoreComments = true };
            XmlReader reader = XmlReader.Create(Url, setting);
            xmldoc.Load(reader);
            XmlNode xmlproj = xmldoc.SelectSingleNode("Gepro");
            foreach (XmlNode node in xmlproj.ChildNodes)
            {
                if (node.Name.Equals("DBSource"))
                {
                    string dbName = node.Attributes["Url"].Value;
                    OpenDBSource(dbName);
                }

                foreach (XmlNode cNode in node)
                {
                    string MapFileName = cNode.Attributes["Url"].Value;
                    Maps.Add(new Tuple<string, IMap>(MapFileName, null));
                }
            }
            reader.Close();
            return true;
        }

        private void OpenMaps_OnItemRemoved(int aIndex, IMap aItem)
        {
            int index = 0;
            foreach (var item in Maps)
            {
                if (item.Item2 == aItem)
                {
                    Maps[index] = new Tuple<string, IMap>(Maps[index].Item1, null);
                    break;
                }
                index++;
            }
        }

        private void Maps_OnItemRemoved(int aIndex, Tuple<string, IMap> aItem)
        {
            if (OpenMaps.Contains(aItem.Item2))
                OpenMaps.Remove(aItem.Item2);
        }
    }
}
