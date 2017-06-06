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
            MapList = new ObservedCollection<MapList>();
            RecentProjectList();
        }

        #region RecentProject

        public void RecentProjectList()
        {
            RecentProjects = new ObservedCollection<RecentProjects>();
            string dir = Directory.GetCurrentDirectory();
            string ProjectName = dir + "\\ProjectMRUList.Project";
            if (File.Exists(ProjectName))
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlReaderSettings setting = new XmlReaderSettings() { IgnoreComments = true };
                XmlReader reader = XmlReader.Create(ProjectName, setting);
                xmldoc.Load(reader);
                XmlNode xmlproj = xmldoc.SelectSingleNode("Projects");
                foreach (XmlNode node in xmlproj.ChildNodes)
                {
                    string ProjectUrl = node.Attributes["Url"].Value;
                    RecentProjects RecentProject = new RecentProjects(ProjectUrl);
                    RecentProjects.Add(RecentProject);
                }
                reader.Close();
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                XmlElement xmlproj = xmldoc.CreateElement("Projects");
                xmldoc.AppendChild(xmlproj);
                xmldoc.Save(ProjectName);
            }
        }

        public bool WriteRecentProject(string url)
        {
            string dir = Directory.GetCurrentDirectory();
            string ProjectName = dir + "\\ProjectMRUList.Project";

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(ProjectName);
            var res = RecentProjects.Where(s => s.Url == url);
            if (res.Count().Equals(0))
            {
                XmlNode xmlprojs = xmldoc.SelectSingleNode("Projects");
                XmlElement xmlproj = xmldoc.CreateElement("Project");
                xmlproj.SetAttribute("Url", url);
                xmlprojs.AppendChild(xmlproj);
                xmldoc.Save(ProjectName);
                RecentProjects RecentProject = new RecentProjects(url);
                RecentProjects.Add(RecentProject);
            }

            return true;
        }

        public bool IsExistProject(string url)
        {
            string dir = Directory.GetCurrentDirectory();
            string ProjectName = dir + "\\ProjectMRUList.Project";
            if (File.Exists(url))
            {
                return true;
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(ProjectName);
                XmlNodeList xnl = xmldoc.SelectSingleNode("Projects").ChildNodes;

                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement) xn;
                    if (xe.GetAttribute("Url") == url)
                    {
                        xe.ParentNode.RemoveChild(xe);
                    }
                }
                xmldoc.Save(ProjectName);

                RecentProjects.Remove(RecentProjects.Where(s => s.Url == url).Single());

                return false;
            }
        }

        #endregion

        public bool OpenMapList(string url)
        {
            OpenMaps.Clear();
            Maps.Clear();
            MapList.Clear();
            Url = url;
            string ProjectName = url;
            string ProjPath = ProjectName.Substring(0, ProjectName.LastIndexOf(@"\"));
            string MapPath = ProjPath + "\\Maps";
            var files = Directory.GetFiles(MapPath, "*.Ge", SearchOption.TopDirectoryOnly).Where(s => s.StartsWith(MapPath));
            foreach (var item in files)
            {
                MapList map = new MapList(item);
                MapList.Add(map);
            }
            return true;
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
                    string MapUrl = cNode.Attributes["Url"].Value;
                    OpenMap(MapUrl);
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
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                        XmlElement xmlproj = xmldoc.CreateElement("Gepro");
                        xmldoc.AppendChild(xmlproj);

                        XmlElement xmlmaps = xmlproj.OwnerDocument.CreateElement("Maps");
                        xmlproj.AppendChild(xmlmaps);
                        int index = 0;
                        foreach (IMap map in OpenMaps)
                        {
                            string MapFile = MapsPath + @"\" + map.Name + ".Ge";
                            XmlElement xmlmap = xmldoc.CreateElement(map.Name);
                            xmlmap.SetAttribute("Name", map.Name);
                            xmlmap.SetAttribute("Url", MapFile);
                            xmlmaps.AppendChild(xmlmap);
                            WriteMap(map, MapFile);
                            Maps[index] = new Tuple<string, IMap>(MapFile, map);
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
                        string MapFile = MapsPath + @"\" + map.Name + ".Ge";
                        XmlElement xmlmap = xmldoc.CreateElement(map.Name);
                        xmlmap.SetAttribute("Name", map.Name);
                        xmlmap.SetAttribute("Url", MapFile);
                        childNode.AppendChild(xmlmap);
                        WriteMap(map, MapFile);
                        Maps[index] = new Tuple<string, IMap>(MapFile, map);
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

            IMap map = gEngine.Graph.Interface.Registry.ReadMap(url);
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

        public ObservedCollection<MapList> MapList { get; set; }

        public ObservedCollection<RecentProjects> RecentProjects { get; set; }
    }

    public class MapList
    {
        public string Url { get; set; }
        public MapList() { }
        public MapList(string url)
        {
            Url = url;
        }
    }

    public class RecentProjects
    {
        public string Url { get; set; }
        public RecentProjects() { }
        public RecentProjects(string url)
        {
            Url = url;
        }
    }
}
