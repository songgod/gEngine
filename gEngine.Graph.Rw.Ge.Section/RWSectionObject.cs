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
    class RWSectionObject : RWObjectBase
    {
        public override string SupportType { get { return "SectionObject"; } }

        public override void Read(IObject Object, XmlNode node)
        {
            if (Object == null)
                return;
            if (node == null)
                return;

            if (node.Name != Object.GetType().Name)
                return;

            DataStruct ds = new DataStruct();

            SectionObject SectionObject = (SectionObject) Object;
            SectionObject.Name = node.Attributes["Name"].Value;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                foreach (XmlNode cFNode in childNode.ChildNodes)
                {
                    foreach (XmlNode cSNode in cFNode.ChildNodes)
                    {
                        string name = cSNode.Name;

                        if (name.Equals("InterPoint"))
                        {
                            InterPoint p = new InterPoint();
                            p.Outgoing = Int32.Parse(cSNode.Attributes["Outgoing"].Value);
                            p.X = double.Parse(cSNode.Attributes["X"].Value);
                            p.Y = double.Parse(cSNode.Attributes["Y"].Value);
                            ds.InterPoints.Add(p);
                            continue;
                        }

                        if (name.Equals("HalfEdge"))
                        {
                            HalfEdge h = new HalfEdge();
                            h.Start = Int32.Parse(cSNode.Attributes["Start"].Value);
                            h.Twin = Int32.Parse(cSNode.Attributes["Twin"].Value);
                            h.LeftRegion = Int32.Parse(cSNode.Attributes["LeftRegion"].Value);
                            h.NextHalf = Int32.Parse(cSNode.Attributes["NextHalf"].Value);
                            h.PreHalf = Int32.Parse(cSNode.Attributes["PreHalf"].Value);
                            h.Segment = Int32.Parse(cSNode.Attributes["Segment"].Value);
                            ds.HalfEdges.Add(h);
                            continue;
                        }

                        if (name.Equals("Region"))
                        {
                            Region r = new Region();
                            r.FirstHalf = Int32.Parse(cSNode.Attributes["FirstHalf"].Value);
                            r.Type = Int32.Parse(cSNode.Attributes["Type"].Value);
                            r.InnerNum = Int32.Parse(cSNode.Attributes["InnerNum"].Value);
                            string s = cSNode.Attributes["InnerHead"].Value;
                            if (!string.IsNullOrEmpty(s))
                            {
                                string[] strList = s.Split(' ');
                                r.InnerHead = strList.Select(n => { return Int32.Parse(n); }).ToList();
                            }
                            ds.Regions.Add(r);
                            continue;
                        }

                        if (name.Equals("Segment"))
                        {
                            Segment s = new Segment();
                            s.X1 = double.Parse(cSNode.Attributes["X1"].Value);
                            s.Y1 = double.Parse(cSNode.Attributes["Y1"].Value);
                            s.X2 = double.Parse(cSNode.Attributes["X2"].Value);
                            s.Y2 = double.Parse(cSNode.Attributes["Y2"].Value);
                            s.Type = Int32.Parse(cSNode.Attributes["Type"].Value);
                            s.IsStraight = bool.Parse(cSNode.Attributes["IsStraight"].Value);
                            s.IncidentHalf = Int32.Parse(cSNode.Attributes["IncidentHalf"].Value);
                            ds.Segments.Add(s);
                        }
                    }
                }
            }

            SectionObject.TopGraph = ds.ToGraph();
        }

        public override void Write(XmlNode node, IObject obj)
        {
            if (obj == null)
                return;

            if (node == null)
                return;

            SectionObject SectionObject = (SectionObject) obj;

            gTopology.Graph graph = (gTopology.Graph) SectionObject.TopGraph;
            DataStruct ds = new DataStruct();
            ds.FromGraph(graph);

            XmlDocument doc = node.OwnerDocument;
            XmlAttribute xmlName = doc.CreateAttribute("Name");
            xmlName.Value = string.IsNullOrEmpty(SectionObject.Name) == false ? SectionObject.Name : string.Empty;
            node.Attributes.Append(xmlName);

            XmlElement xmlgraph = node.OwnerDocument.CreateElement(SectionObject.TopGraph.GetType().Name);
            node.AppendChild(xmlgraph);

            XmlElement xmlInterPoints = xmlgraph.OwnerDocument.CreateElement("InterPoints");
            xmlgraph.AppendChild(xmlInterPoints);

            XmlElement xmlHalfEdges = xmlgraph.OwnerDocument.CreateElement("HalfEdges");
            xmlgraph.AppendChild(xmlHalfEdges);

            XmlElement xmlRegions = xmlgraph.OwnerDocument.CreateElement("Regions");
            xmlgraph.AppendChild(xmlRegions);

            XmlElement xmlSegments = xmlgraph.OwnerDocument.CreateElement("Segments");
            xmlgraph.AppendChild(xmlSegments);

            foreach (InterPoint p in ds.InterPoints)
            {
                XmlElement xmlInterPoint = xmlInterPoints.OwnerDocument.CreateElement("InterPoint");
                xmlInterPoint.SetAttribute("Outgoing", p.Outgoing.ToString("0"));
                xmlInterPoint.SetAttribute("X", p.X.ToString());
                xmlInterPoint.SetAttribute("Y", p.Y.ToString());
                xmlInterPoints.AppendChild(xmlInterPoint);
            }

            foreach (HalfEdge h in ds.HalfEdges)
            {
                XmlElement xmlHalfEdge = xmlInterPoints.OwnerDocument.CreateElement("HalfEdge");
                xmlHalfEdge.SetAttribute("Start", h.Start.ToString("0"));
                xmlHalfEdge.SetAttribute("Twin", h.Twin.ToString("0"));
                xmlHalfEdge.SetAttribute("LeftRegion", h.LeftRegion.ToString("0"));
                xmlHalfEdge.SetAttribute("NextHalf", h.NextHalf.ToString("0"));
                xmlHalfEdge.SetAttribute("PreHalf", h.PreHalf.ToString("0"));
                xmlHalfEdge.SetAttribute("Segment", h.Segment.ToString("0"));
                xmlHalfEdges.AppendChild(xmlHalfEdge);
            }

            foreach (Region r in ds.Regions)
            {
                XmlElement xmlRegion = xmlInterPoints.OwnerDocument.CreateElement("Region");
                xmlRegion.SetAttribute("FirstHalf", r.FirstHalf.ToString());
                xmlRegion.SetAttribute("Type", r.Type.ToString("0"));
                xmlRegion.SetAttribute("InnerNum", r.InnerNum.ToString("0"));
                xmlRegion.SetAttribute("InnerHead", string.Join(" ", r.InnerHead.ToArray()));
                xmlRegions.AppendChild(xmlRegion);
            }

            foreach (Segment s in ds.Segments)
            {
                XmlElement xmlSegment = xmlInterPoints.OwnerDocument.CreateElement("Segment");
                xmlSegment.SetAttribute("X1", s.X1.ToString());
                xmlSegment.SetAttribute("Y1", s.Y1.ToString());
                xmlSegment.SetAttribute("X2", s.X2.ToString());
                xmlSegment.SetAttribute("Y2", s.Y2.ToString());
                xmlSegment.SetAttribute("Type", s.Type.ToString());
                xmlSegment.SetAttribute("IsStraight", s.IsStraight.ToString());
                xmlSegment.SetAttribute("IncidentHalf", s.IncidentHalf.ToString());
                xmlSegments.AppendChild(xmlSegment);
            }
        }

        public override IObject CreateObject()
        {
            return new SectionObject();
        }
    }
}
