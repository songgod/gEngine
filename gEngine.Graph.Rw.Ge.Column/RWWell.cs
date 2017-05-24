using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Util;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Column
{
    class RWWell : RWObjectBase
    {
        public override string SupportType { get { return "Well"; } }

        public override void Read(IObject Object, XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            Well well = (Well) Object;

            well.Name = node.Attributes["Name"].Value;
            well.Location = Int32.Parse(node.Attributes["Location"].Value);
            well.LongitudinalProportion = Int32.Parse(node.Attributes["LongitudinalProportion"].Value);

            foreach (XmlNode childNode in node.ChildNodes)
            {
                WellColumns WellColumns = new WellColumns();
                foreach (XmlNode cNode in childNode.ChildNodes)
                {
                    RWWellColumn objectrw = Registry.GetObjectRW(cNode.Name);
                    if (objectrw == null)
                    {
                        Log.LogWarning("Cound not find " + cNode.Name + " object readerwriter");
                        continue;
                    }
                    IObject CObject = objectrw.CreateObject();
                    objectrw.Read(CObject, cNode);
                    if (CObject == null)
                        return;
                    ((WellColumn) (CObject)).Owner = well;
                    WellColumns.Add((WellColumn) CObject);
                }
                if (WellColumns.Count > 0)
                    well.LstColumns.Add(WellColumns);
            }
        }

        public override void Write(XmlNode node, IObject obj)
        {
            if (obj == null)
                return;

            if (node == null)
                return;

            Well Well = (Well) obj;
            XmlDocument doc = node.OwnerDocument;

            foreach (WellColumns WellColumns in Well.LstColumns)
            {
                XmlElement ColumnsXmlNode = node.OwnerDocument.CreateElement("WellColumns");
                foreach (IObject Object in WellColumns)
                {
                    string objecttype = Object.GetType().Name.ToString();
                    RWWellColumn objectrw = Registry.GetObjectRW(objecttype);
                    if (objectrw == null)
                    {
                        Log.LogWarning("Cound not find " + objecttype + " object readerwriter");
                        continue;
                    }
                    objectrw.Write(ColumnsXmlNode, Object);
                }
                node.AppendChild(ColumnsXmlNode);
            }

            XmlAttribute xmlWellName = doc.CreateAttribute("Name");
            xmlWellName.Value = string.IsNullOrEmpty(Well.Name) == false ? Well.Name : string.Empty;
            node.Attributes.Append(xmlWellName);

            XmlAttribute xmlLocation = doc.CreateAttribute("Location");
            xmlLocation.Value = string.IsNullOrEmpty(Well.Location.ToString()) == false ? Well.Location.ToString() : string.Empty;
            node.Attributes.Append(xmlLocation);

            XmlAttribute xmlLongitudinalProportion = doc.CreateAttribute("LongitudinalProportion");
            xmlLongitudinalProportion.Value = string.IsNullOrEmpty(Well.LongitudinalProportion.ToString()) == false ? Well.LongitudinalProportion.ToString() : string.Empty;
            node.Attributes.Append(xmlLongitudinalProportion);
        }

        public override IObject CreateObject()
        {
            return new Well();
        }
    }
}
