using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Column
{
    class RWWellColumn : RWObjectBase
    {
        public override string SupportType { get { return "WellColumn"; } }

        public override void Read(IObject Object, XmlNode node)
        {
            if (node == null)
                return;

            if (Object == null)
                return;
            
            WellColumn WellColumn = (WellColumn)Object;
            WellColumn.Name = node.Attributes["Name"].Value;
            WellColumn.Width = Int32.Parse(node.Attributes["Width"].Value);
            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush) brushConverter.ConvertFromString(node.Attributes["Color"].Value);
            Color color = (Color) ColorConverter.ConvertFromString(brush.ToString());
            WellColumn.Color = color;
        }

        public override void Write(XmlNode node, IObject obj)
        {
            if (node == null)
                return;

            if (obj == null)
                return;

            WellColumn WellColumn = (WellColumn) obj;
            XmlDocument doc = node.OwnerDocument;

            XmlAttribute xmlName = doc.CreateAttribute("Name");
            xmlName.Value = string.IsNullOrEmpty(WellColumn.Name) == false ? WellColumn.Name : string.Empty;
            node.Attributes.Append(xmlName);

            XmlAttribute xmlColor = doc.CreateAttribute("Color");
            xmlColor.Value = string.IsNullOrEmpty(WellColumn.Color.ToString()) == false ? WellColumn.Color.ToString() : string.Empty;
            node.Attributes.Append(xmlColor);

            XmlAttribute xmlWidth = doc.CreateAttribute("Width");
            xmlWidth.Value = string.IsNullOrEmpty(WellColumn.Width.ToString()) == false ? WellColumn.Width.ToString() : string.Empty;
            node.Attributes.Append(xmlWidth);
        }

        public override IObject CreateObject()
        {
            return null;
        }
    }
}
