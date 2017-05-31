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
    public class RWLayerBase
    {
        public virtual string SupportType { get { return "Layer"; } }

        public virtual void ReadLayer(ILayer Ilayer, XmlNode node)
        {
            if (node == null)
                return;

            Layer layer = (Layer) Ilayer;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                RWObjectBase objectrw = Registry.GetObjectRW(childNode.Name);
                if (objectrw == null)
                {
                    Log.LogWarning("Cound not find " + childNode.Name + " object readerwriter");
                    continue;
                }
                IObject Object = objectrw.CreateObject();
                objectrw.Read(Object, childNode);
                if (Object == null)
                    continue;
                layer.Objects.Add(Object);
            }
        }

        public virtual void WriteLayer(XmlNode node, ILayer layer)
        {
            if (layer == null)
                return;

            if (node == null)
                return;

            foreach (IObject Object in layer.Objects)
            {
                string objecttype = Object.GetType().Name.ToString();

                RWObjectBase objectrw = Registry.GetObjectRW(objecttype);

                if (objectrw == null)
                {
                    Log.LogWarning("Cound not find " + objecttype + " object readerwriter");
                    continue;
                }

                XmlElement xmlobject = node.OwnerDocument.CreateElement(objecttype);
                objectrw.Write(xmlobject, Object);
                node.AppendChild(xmlobject);
            }
        }

        public virtual ILayer CreateLayer()
        {
            return new Layer();
        }
    }
}
