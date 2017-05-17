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
        public virtual  string SupportType { get { return "Layer"; } }
        public virtual ILayer ReadLayer(XmlNode node)
        {
            Layer layer = new Layer();

            

            return layer;
        }

        public virtual void WriteLayer(XmlNode node, ILayer layer)
        {
            foreach (IObject obj in layer.Objects)
            {
                string objecttype = obj.GetType().Name.ToString();

                RWObjectBase objectrw = Registry.GetObjectRW(objecttype);

                if (objectrw == null)
                {
                    Log.LogWarning("Cound not find " + objecttype + " object readerwriter");
                    continue;
                }


            }
        }
    }
}
