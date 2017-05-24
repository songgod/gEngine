using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static gEngine.Graph.Ge.Column.Enums;

namespace gEngine.Graph.Rw.Ge.Column
{
    class RWWellLogColumn : RWWellColumn
    {
        public override string SupportType { get { return "WellLogColumn"; } }

        public override void Read(IObject Object,XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            base.Read(Object, node);

            WellLogColumn WellLogColumn = (WellLogColumn) Object;
            WellLogColumn.MathType = (MathType) Enum.Parse(typeof(MathType), node.Attributes["MathType"].Value);

            string[] StrValues = (node.Attributes["Values"].Value).Split(',');
            List<double> Values = StrValues.ToList<string>().Select(n =>double.Parse(n)).ToList<double>();
            WellLogColumn.Values = new Utility.ObsDoubles(Values);
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

            WellLogColumn WellLogColumn = (WellLogColumn) Object;
            ObsDoubles Values = (ObsDoubles)WellLogColumn.Values;
            string StrValues = string.Join(",", Values);

            XmlDocument doc = xmlobject.OwnerDocument;

            XmlAttribute xmlMathType = doc.CreateAttribute("MathType");
            xmlMathType.Value = string.IsNullOrEmpty(WellLogColumn.MathType.ToString()) == false ? WellLogColumn.MathType.ToString() : string.Empty;
            xmlobject.Attributes.Append(xmlMathType);

            XmlAttribute xmlValues = doc.CreateAttribute("Values");
            xmlValues.Value = StrValues;
            xmlobject.Attributes.Append(xmlValues);
        }

        public override IObject CreateObject()
        {
            return new WellLogColumn();
        }
    }
}