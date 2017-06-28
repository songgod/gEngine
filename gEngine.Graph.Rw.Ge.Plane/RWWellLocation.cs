using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;
using static gEngine.Graph.Ge.Plane.Enums;

namespace gEngine.Graph.Rw.Ge.Plane
{
    class RWWellLocation : RWObjectBase
    {
        public override string SupportType { get { return "WellLocation"; } }

        public override void Read(IObject Object,XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            WellLocation WellLocation = (WellLocation)Object;

            WellLocation.WellNum = node.Attributes["WellNum"].Value;
            WellLocation.WellCategory = (WellCategory) Enum.Parse(typeof(WellCategory), node.Attributes["WellCategory"].Value);
            WellLocation.WellType = (WellType) Enum.Parse(typeof(WellType), node.Attributes["WellType"].Value);
            WellLocation.X = double.Parse(node.Attributes["X"].Value);
            WellLocation.Y = double.Parse(node.Attributes["Y"].Value);

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name.Equals(WellLocation.PointStyle.GetType().Name))
                {
                    BrushConverter brushConverter = new BrushConverter();
                    Brush brush = (Brush) brushConverter.ConvertFromString(childNode.Attributes["Color"].Value);
                    Color color = (Color) ColorConverter.ConvertFromString(brush.ToString());
                    WellLocation.PointStyle.Color = color;
                    WellLocation.PointStyle.SymbolLib = childNode.Attributes["SymbolLib"].Value;
                    WellLocation.PointStyle.Symbol = childNode.Attributes["Symbol"].Value;
                }
            } 
        }

        public override void Write(XmlNode node, IObject obj)
        {
            if (obj == null)
                return;

            if (node == null)
                return;

            WellLocation wl = (WellLocation) obj;
            XmlDocument doc = node.OwnerDocument;

            XmlAttribute xmlWellNum = doc.CreateAttribute("WellNum");
            xmlWellNum.Value = string.IsNullOrEmpty(wl.WellNum) == false ? wl.WellNum : string.Empty;
            node.Attributes.Append(xmlWellNum);

            XmlAttribute xmlWellCategory = doc.CreateAttribute("WellCategory");
            xmlWellCategory.Value = string.IsNullOrEmpty(wl.WellCategory.ToString()) == false ? wl.WellCategory.ToString() : string.Empty;
            node.Attributes.Append(xmlWellCategory);

            XmlAttribute xmlWellType = doc.CreateAttribute("WellType");
            xmlWellType.Value = string.IsNullOrEmpty(wl.WellType.ToString()) == false ? wl.WellType.ToString() : string.Empty;
            node.Attributes.Append(xmlWellType);

            XmlAttribute xmlX = doc.CreateAttribute("X");
            xmlX.Value = string.IsNullOrEmpty(wl.X.ToString()) == false ? wl.X.ToString() : string.Empty;
            node.Attributes.Append(xmlX);

            XmlAttribute xmlY = doc.CreateAttribute("Y");
            xmlY.Value = string.IsNullOrEmpty(wl.Y.ToString()) == false ? wl.Y.ToString() : string.Empty;
            node.Attributes.Append(xmlY);

            XmlElement XEPointStyle = node.OwnerDocument.CreateElement(wl.PointStyle.GetType().Name);
            node.AppendChild(XEPointStyle);

            XmlAttribute xmlColor = doc.CreateAttribute("Color");
            xmlColor.Value = string.IsNullOrEmpty(wl.PointStyle.Color.ToString()) == false ? wl.PointStyle.Color.ToString() : string.Empty;
            XEPointStyle.Attributes.Append(xmlColor);

            XmlAttribute xmlSymbolLib = doc.CreateAttribute("SymbolLib");
            xmlSymbolLib.Value = string.IsNullOrEmpty(wl.PointStyle.SymbolLib) == false ? wl.PointStyle.SymbolLib : string.Empty;
            XEPointStyle.Attributes.Append(xmlSymbolLib);

            XmlAttribute xmlSymbol = doc.CreateAttribute("Symbol");
            xmlSymbol.Value = string.IsNullOrEmpty(wl.PointStyle.Symbol) == false ? wl.PointStyle.Symbol : string.Empty;
            XEPointStyle.Attributes.Append(xmlSymbol);

        }

        public override IObject CreateObject()
        {
            return new WellLocation();
        }
    }
}
