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
            if (string.IsNullOrEmpty(url))
                return null;

            Registry.LoadLocalRW();

            XmlDocument xmldoc = new XmlDocument();

            try
            {
                XmlReaderSettings setting = new XmlReaderSettings() { IgnoreComments = true };
                XmlReader reader = XmlReader.Create(url, setting);
                xmldoc.Load(reader);
                XmlNode xmlmap = xmldoc.SelectSingleNode("Map");
                Map map = new Map() { Name = xmlmap.Attributes["Name"].Value };
                foreach (XmlNode node in xmlmap.ChildNodes)
                {
                    RWLayerBase layerrw = Registry.GetLayerRW(node.Name);
                    if (layerrw == null)
                    {
                        Log.LogWarning("Cound not find " + node.Name + " layer readerwriter");
                        continue;
                    }
                    ILayer layer = layerrw.CreateLayer();
                    layerrw.ReadLayer(layer,node);
                    if (layer == null)
                        continue;
                    layer.Name = node.Attributes["Name"].Value;
                    layer.Type = node.Attributes["Type"].Value;
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

        public bool WriteMap(IMap map, string url)
        {
            if (map == null || string.IsNullOrEmpty(url))
                return false;

            Registry.LoadLocalRW();

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
                xmllayer.SetAttribute("Name", layer.Name);
                xmllayer.SetAttribute("Type", layer.Type);
                layerrw.WriteLayer(xmllayer, layer);
                xmlmap.AppendChild(xmllayer);
            }
            xmldoc.Save(url);
            return true;
        }
    }
}
