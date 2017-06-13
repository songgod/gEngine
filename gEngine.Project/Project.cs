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

        public bool Save()
        {
            if (string.IsNullOrEmpty(Url))
            {
                SaveFileDialog SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.Filter = "工程文件|*.Gepro";
                if (SaveFileDialog.ShowDialog() == true)
                {
                    if (File.Exists(SaveFileDialog.FileName))
                    {
                        MessageBox.Show("工程文件名称重复，请重新输入！");
                        return false;
                    }

                    string ProjectName = SaveFileDialog.FileName;
                    string ProjPath = ProjectName.Substring(0, ProjectName.LastIndexOf(@"\"));
                    string MapsPath = ProjPath + @"\Maps";
                    if (Directory.Exists(MapsPath))
                    {
                        MessageBox.Show("图件目录重复！");
                        return false;
                    }
                    else
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(MapsPath);
                        directoryInfo.Create();
                        return SaveAllMap(ProjectName);
                    }
                }
                else
                {
                    MessageBox.Show("取消保存");
                    return false;
                }
            }
            else
            {
                string ProjectName = Url;
                return SaveAllMap(ProjectName);
            }
        }

        public bool SaveAs(string url)
        {
            Url = url;
            return Save();
        }

        public bool SaveAllMap(string ProjectName)
        {
            string ProjPath = ProjectName.Substring(0, ProjectName.LastIndexOf(@"\"));
            string MapsPath = ProjPath + @"\Maps";
            XmlDocument xmldoc = new XmlDocument();
            XmlElement xmlproj = null;
            XmlElement xmlmaps = null;
            if (string.IsNullOrEmpty(Url))
            {
                Url = ProjectName;
                xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                xmlproj = xmldoc.CreateElement("Gepro");
                xmldoc.AppendChild(xmlproj);
                xmlmaps = xmlproj.OwnerDocument.CreateElement("Maps");
                xmlproj.AppendChild(xmlmaps);
                XmlElement xmldbsource = xmlproj.OwnerDocument.CreateElement("DBSource");
                xmldbsource.SetAttribute("Url", DBTuple.Item1);
                xmlproj.AppendChild(xmldbsource);
            }
            else
            {
                xmldoc.Load(ProjectName);
                xmlproj = xmldoc.SelectSingleNode("Gepro") as XmlElement;
                xmlmaps = xmlproj.SelectSingleNode("Maps") as XmlElement;
            }

            for (int index = 0; index < Maps.Count; index++)
            {
                string MapFileName = string.Empty;
                string MapFile = string.Empty;
                if (string.IsNullOrEmpty(Maps[index].Item1))
                {
                    MapFileName = Maps[index].Item2.Name + ".Ge";
                    MapFile = MapsPath + @"\" + MapFileName;
                    Maps[index] = new Tuple<string, IMap>(MapFileName, Maps[index].Item2);
                }
                else
                {
                    bool IsDic = IsDirectory(Maps[index].Item1);
                    if (IsDic)
                    {
                        MapFileName = Maps[index].Item1;
                        MapFile = Maps[index].Item1;
                    }
                }

                if (Maps[index].Item2 != null)
                    WriteMap(Maps[index].Item2, MapFile);

                string mapPath = Maps[index].Item1;
                XmlNodeList xmlnodelist = xmlmaps.SelectNodes(string.Format(@"*[@Url='{0}']", mapPath));
                int nodeCount = xmlnodelist.Count;
                if (nodeCount <= 0)
                {
                    XmlElement xmlmap = xmldoc.CreateElement(Maps[index].Item2.Name);
                    xmlmap.SetAttribute("Url", MapFileName);
                    xmlmaps.AppendChild(xmlmap);
                }
            }
            xmldoc.Save(ProjectName);
            return true;
        }

        public bool IsDirectory(string filepath)
        {
            string pattern = @"[\\+]|[//+]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(filepath);
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

        public class MapCollection : ObservedCollection<Tuple<string, IMap>> { }

        public class DBSourceTuple : Tuple<string, IDBSource>
        {
            public DBSourceTuple(string url, IDBSource db) : base(url, db)
            {
            }
        }

        public MapCollection Maps { get; private set; }

        public IMaps OpenMaps { get; set; }

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
        private string url;
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                Read();
            }
        }

        public bool Read()
        {
            return false;
        }
        public bool Write()
        {
            return false;
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
            string ProjPath = Url.Substring(0, Url.LastIndexOf(@"\"));
            string MapsPath = ProjPath + @"\Maps";
            string MapFile = MapsPath + @"\" + url;

            bool find = false;
            int index = 0;
            foreach (var item in Maps)
            {
                if (item.Item1 == url)
                {
                    find = true;
                    break;
                }
                index++;
            }

            IMap map = gEngine.Graph.Interface.Registry.ReadMap(MapFile);

            if (map == null)
                return null;

            if (find == false)
            {
                Maps.Add(new Tuple<string, IMap>(url, map));
            }
            else
            {
                Maps[index] = new Tuple<string, IMap>(url, map);
            }

            OpenMaps.Add(map);
            return map;
        }

        public IMap NewMap(string type, string name)
        {
            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(name))
                return null;

            IMap map = gEngine.Graph.Interface.Registry.CreateMap(type);
            if (map == null)
                return null;
            map.Name = name;
            Maps.Add(new Tuple<string, IMap>("", map));
            OpenMaps.Add(map);
            return map;
        }

        public IMap NewMap(string type, string name, ILayers layers)
        {
            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(name))
                return null;

            IMap map = gEngine.Graph.Interface.Registry.CreateMap(type);
            if (map == null)
                return null;
            map.Name = name;
            Maps.Add(new Tuple<string, IMap>("", map));

            foreach (var layer in layers)
            {
                map.Layers.Add(layer);
            }

            OpenMaps.Add(map);
            return map;
        }

        public bool WriteMap(IMap map, string url)
        {
            if (map == null)
                return false;

            if (string.IsNullOrEmpty(url))
                return false;

            bool IsSuccess = gEngine.Graph.Interface.Registry.WriteMap(map, url);
            return IsSuccess;
        }
    }
}
