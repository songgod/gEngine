using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge
{
    public class RwSectionLayer : RWLayerBase
    {
        public override string SupportType { get { return "SectionLayer"; } }

        public override ILayer ReadLayer(XmlNode node)
        {
            //SectionLayer layer = new SectionLayer;



            //return layer;
            return base.ReadLayer(node);
        }

        public override void WriteLayer(XmlNode node, ILayer layer)
        {
            base.WriteLayer(node, layer);
        }
    }
}
