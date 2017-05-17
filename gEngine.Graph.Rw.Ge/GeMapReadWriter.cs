using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge
{
    public class GeMapReadWriter : IMapReadWriter
    {
        public string SupportType
        {
            get
            {
                return "Ge";
            }
        }

        public IMap CreateMap()
        {
            return new Map();
        }

        public IMap ReadMap(string url)
        {
            XmlDocument xmldoc = new XmlDocument();
            
            try
            {
                XmlReaderSettings setting = new XmlReaderSettings() { IgnoreComments = true };
                XmlReader reader = XmlReader.Create(url,setting);
                xmldoc.Load(reader);
                XmlNode xmlmap = xmldoc.SelectSingleNode("Map");
                Map map = new Map() { Name = xmlmap.Attributes["Name"].Value };
                foreach (XmlNode node in xmlmap.ChildNodes)
                {
                    RWLayerBase layerrw = Registry.GetLayerRW(node.Name);
                    ILayer layer = layerrw.ReadLayer(node);
                    map.Layers.Add(layer);
                }
                reader.Close();
                return map;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public bool WriteMap(IMap map, string url)
        //{
        //    if (map == null || string.IsNullOrEmpty(url))
        //        return false;


        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
        //    XmlElement xmlmap = xmldoc.CreateElement("Map");
        //    xmlmap.SetAttribute("Name", map.Name);
        //    xmldoc.AppendChild(xmlmap);
        //    foreach (ILayer layer in map.Layers)
        //    {
        //        string layertype = layer.GetType().ToString();
        //        RWLayerBase layerrw = Registry.GetLayerRW(layertype);
        //        if (layerrw == null)
        //        {
        //            Log.LogWarning("Cound not find "+ layertype+" layer readerwriter");
        //            continue;
        //        }

        //        XmlElement xmllayer = xmlmap.OwnerDocument.CreateElement(layer.GetType().ToString());
        //        layerrw.WriteLayer(xmllayer, layer);
        //        xmlmap.AppendChild(xmllayer);
        //    }
        //    xmldoc.Save(url);
        //    return true;
        //}

        public bool WriteMap(IMap map, string url)
        {
            if (map == null || string.IsNullOrEmpty(url))
                return false;


            XmlDocument xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlElement xmlmap = xmldoc.CreateElement("Map");
            xmlmap.SetAttribute("Name", map.Name);
            xmldoc.AppendChild(xmlmap);
            foreach (ILayer layer in map.Layers)
            {
                string layertype = layer.GetType().Name.ToString();
                RWLayerBase layerrw = Registry.GetLayerRW(layertype);
                if (layerrw == null)
                {
                    Log.LogWarning("Cound not find " + layertype + " layer readerwriter");
                    continue;
                }

                XmlElement xmllayer = xmlmap.OwnerDocument.CreateElement(layertype);
                layerrw.WriteLayer(xmllayer, layer);
                xmlmap.AppendChild(xmllayer);
            }
            xmldoc.Save(url);
            return true;
        }
    }
}
