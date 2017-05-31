using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Column
{
    class RWWellSegmentColumn : RWWellColumn
    {
        public override string SupportType { get { return "WellSegmentColumn"; } }

        public override void Read(IObject Object,XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            base.Read(Object, node);

            WellSegmentColumn WellSegmentColumn = (WellSegmentColumn) Object;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                WellSegmentColumn.Segment Segment = new WellSegmentColumn.Segment();

                Segment.Top = double.Parse(childNode.Attributes["Top"].Value);
                Segment.Bottom = double.Parse(childNode.Attributes["Bottom"].Value);
                Segment.Name = childNode.Attributes["Name"].Value;
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString(childNode.Attributes["Color"].Value);
                Color color = (Color)ColorConverter.ConvertFromString(brush.ToString());
                Segment.Color = color;
                WellSegmentColumn.Segments.Add(Segment);

            }
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

            WellSegmentColumn WellSegmentColumn = (WellSegmentColumn) Object;

            foreach (var Segment in WellSegmentColumn.Segments)
            {
                string childobjecttype = Segment.GetType().Name;
                XmlElement xmlchildobject = xmlobject.OwnerDocument.CreateElement(childobjecttype);

                XmlDocument doc = xmlobject.OwnerDocument;

                XmlAttribute xmlTop = doc.CreateAttribute("Top");
                xmlTop.Value = string.IsNullOrEmpty(Segment.Top.ToString()) == false ? Segment.Top.ToString() : string.Empty;
                xmlchildobject.Attributes.Append(xmlTop);

                XmlAttribute xmlBottom = doc.CreateAttribute("Bottom");
                xmlBottom.Value = string.IsNullOrEmpty(Segment.Bottom.ToString()) == false ? Segment.Bottom.ToString() : string.Empty;
                xmlchildobject.Attributes.Append(xmlBottom);

                XmlAttribute xmlName = doc.CreateAttribute("Name");
                xmlName.Value = string.IsNullOrEmpty(Segment.Name) == false ? Segment.Name : string.Empty;
                xmlchildobject.Attributes.Append(xmlName);

                XmlAttribute xmlColor = doc.CreateAttribute("Color");
                xmlColor.Value = string.IsNullOrEmpty(Segment.Color.ToString()) == false ? Segment.Color.ToString() : string.Empty;
                xmlchildobject.Attributes.Append(xmlColor);

                xmlobject.AppendChild(xmlchildobject);
            }
        }

        public override IObject CreateObject()
        {
            return new WellSegmentColumn();
        }
    }
}
