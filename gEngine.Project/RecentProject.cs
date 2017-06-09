using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Project
{
    public class RecentProject
    {
        public RecentProject()
        {
            OpenProjects = new ObservedCollection<string>();
        }

        public ObservedCollection<string> OpenProjects { get; set; }

        private string projectListUrl;
        public string ProjectListUrl
        {
            get
            {
                return projectListUrl;
            }
            set
            {
                projectListUrl = value;
            }
        }

        public bool Open(string url)
        {
            ProjectListUrl = url;
            if (File.Exists(url))
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlReaderSettings setting = new XmlReaderSettings() { IgnoreComments = true };
                XmlReader reader = XmlReader.Create(url, setting);
                xmldoc.Load(reader);
                XmlNode xmlproj = xmldoc.SelectSingleNode("Projects");
                foreach (XmlNode node in xmlproj.ChildNodes)
                {
                    string ProjectUrl = node.Attributes["Url"].Value;
                    OpenProjects.Add(ProjectUrl);
                }
                reader.Close();
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                XmlElement xmlproj = xmldoc.CreateElement("Projects");
                xmldoc.AppendChild(xmlproj);
                xmldoc.Save(url);
            }
            return true;
        }

        public bool Write(string url)
        {
            if (string.IsNullOrEmpty(ProjectListUrl))
                return false;

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(ProjectListUrl);
            var res = OpenProjects.Where(s => s == url);
            if (res.Count().Equals(0))
            {
                XmlNode xmlprojs = xmldoc.SelectSingleNode("Projects");
                XmlElement xmlproj = xmldoc.CreateElement("Project");
                xmlproj.SetAttribute("Url", url);
                xmlprojs.AppendChild(xmlproj);
                xmldoc.Save(ProjectListUrl);
                OpenProjects.Add(url);
            }

            return true;
        }

        public bool IsExistProject(string url)
        {
            if (string.IsNullOrEmpty(ProjectListUrl))
                return false;

            if (File.Exists(url))
            {
                return true;
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(ProjectListUrl);
                XmlNodeList xnl = xmldoc.SelectSingleNode("Projects").ChildNodes;

                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement) xn;
                    if (xe.GetAttribute("Url") == url)
                    {
                        xe.ParentNode.RemoveChild(xe);
                    }
                }
                xmldoc.Save(ProjectListUrl);

                OpenProjects.Remove(OpenProjects.Where(s => s == url).Single());

                return false;
            }
        }
    }
}
