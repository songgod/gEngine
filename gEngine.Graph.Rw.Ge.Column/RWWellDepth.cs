using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Column
{
    class RWWellDepth : RWWellColumn
    {
        public override string SupportType { get { return "WellDepth"; } }

        public override void Read(IObject Object, XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            base.Read(Object, node);

            WellDepth WellDepth = (WellDepth) Object;
            string[] StrDepths = (node.Attributes["Depths"].Value).Split(',');
            List<double> Depths = StrDepths.ToList<string>().Select(n => double.Parse(n)).ToList<double>();
            WellDepth.Depths = new Utility.ObsDoubles(Depths);
        }

        public override void Write(XmlNode node, IObject Object)
        {
            if (node == null)
                return;
            if (Object == null)
                return;

            string objecttype = Object.GetType().Name;

            XmlElement xmlobject = node.OwnerDocument.CreateElement(objecttype);
            node.AppendChild(xmlobject);
            base.Write(xmlobject, Object);

            ObsDoubles Depths = (ObsDoubles) ((WellDepth) Object).Depths;
            string StrDepths = string.Join(",", Depths);

            XmlDocument doc = xmlobject.OwnerDocument;
            XmlAttribute xmlDepths = doc.CreateAttribute("Depths");
            xmlDepths.Value = StrDepths;
            xmlobject.Attributes.Append(xmlDepths);
        }

        public override IObject CreateObject()
        {
            return new WellDepth();
        }
    }
}
