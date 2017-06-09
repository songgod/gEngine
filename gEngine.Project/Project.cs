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
                        Url = ProjectName;
                        DirectoryInfo directoryInfo = new DirectoryInfo(MapsPath);
                        directoryInfo.Create();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                        XmlElement xmlproj = xmldoc.CreateElement("Gepro");
                        xmldoc.AppendChild(xmlproj);

                        XmlElement xmlmaps = xmlproj.OwnerDocument.CreateElement("Maps");
                        xmlproj.AppendChild(xmlmaps);
                        int index = 0;
                        foreach (IMap map in OpenMaps)
                        {
                            string MapFileName =  map.Name + ".Ge";
                            string MapFile = MapsPath + @"\" + MapFileName;
                            XmlElement xmlmap = xmldoc.CreateElement(map.Name);
                            xmlmap.SetAttribute("Name", map.Name);
                            xmlmap.SetAttribute("Url", MapFileName);
                            xmlmaps.AppendChild(xmlmap);
                            WriteMap(map, MapFile);
                            Maps[index] = new Tuple<string, IMap>(MapFileName, map);
                            index++;
                        }
                        xmldoc.Save(ProjectName);
                        return true;
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
                string ProjPath = ProjectName.Substring(0, ProjectName.LastIndexOf(@"\"));
                string MapsPath = ProjPath + @"\Maps";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(ProjectName);
                XmlNode node = xmldoc.SelectSingleNode("Gepro");
                XmlNode childNode = node.SelectSingleNode("Maps");
                int index = 0;
                foreach (IMap map in OpenMaps)
                {
                    if (string.IsNullOrEmpty(Maps[index].Item1))
                    {
                        string MapFileName = map.Name + ".Ge";
                        string MapFile = MapsPath + @"\" + MapFileName;
                        XmlElement xmlmap = xmldoc.CreateElement(map.Name);
                        xmlmap.SetAttribute("Name", map.Name);
                        xmlmap.SetAttribute("Url", MapFileName);
                        childNode.AppendChild(xmlmap);
                        WriteMap(map, MapFile);
                        Maps[index] = new Tuple<string, IMap>(MapFileName, map);
                    }
                    index++;
                }

                xmldoc.Save(ProjectName);
                return true;
            }
        }

        public bool SaveAs(string url)
        {
            Url = url;
            return Save();
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
