using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Section
{
    class RWStratumObject : RWSectionObject
    {
        public override string SupportType
        {
            get
            {
                return "StratumObject";
            }
        }

        public override void Read(IObject Object, XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            base.Read(Object, node);
        }

        public override void Write(XmlNode node, IObject Object)
        {
            if (node == null)
                return;
            if (Object == null)
                return;
            base.Write(node, Object);
        }

        public override IObject CreateObject()
        {
            return new StratumObject();
        }
    }
}

