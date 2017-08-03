using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Section
{
    class RWSectionInfo
    {
        #region Read 

        public void Read(SectionInfo info, XmlNode node)
        {
            if (info == null || node == null)
                return;

            DataStruct ds = new DataStruct();

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
                            s.ID = Int32.Parse(cSNode.Attributes["ID"].Value);
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

            info.TopGraph = ds.ToGraph();

            ReadStyle(info, node);
        }

        private void ReadStyle(SectionInfo info, XmlNode node)
        {
            XmlNode NodeStyle = node.SelectSingleNode("Style");

            foreach (XmlNode cNode in NodeStyle)
            {
                bool HasNode = cNode.HasChildNodes;
                if (HasNode)
                {
                    foreach (XmlNode cFNode in cNode)
                    {
                        string name = cFNode.Name;
                        int Id = Int32.Parse(cFNode.Attributes["Id"].Value);
                        if (name.Equals("LineStyle"))
                        {
                            info.DicLineStyle.Add(Id, GetLineStyle(cFNode));
                        }

                        if (name.Equals("FillStyle"))
                        {
                            info.DicFillStyle.Add(Id, GetFillStyle(cFNode));
                        }
                    }
                }
                else
                {
                    string name = cNode.Name;
                    if (name.Equals("DefaultFaultLineStyle"))
                    {
                        info.DefaultFaultLineStyle = GetLineStyle(cNode);
                    }

                    if (name.Equals("DefaultStratumLineStyle"))
                    {
                        info.DefaultStratumLineStyle = GetLineStyle(cNode);
                    }

                    if (name.Equals("DefaultStandLineStyle"))
                    {
                        info.DefaultStratumLineStyle = GetLineStyle(cNode);
                    }

                    if (name.Equals("DefaultStratumFillStyle"))
                    {
                        info.DefaultStratumFillStyle = GetFillStyle(cNode);
                    }

                    if (name.Equals("DefaultStandFillStyle"))
                    {
                        info.DefaultSandFillStyle = GetFillStyle(cNode);
                    }
                }
            }
        }

        private LineStyle GetLineStyle(XmlNode node)
        {
            LineStyle lstyle = new LineStyle();
            if (!string.IsNullOrEmpty(node.Attributes["Width"].Value))
            {
                lstyle.Width = double.Parse(node.Attributes["Width"].Value);
            }

            if (!string.IsNullOrEmpty(node.Attributes["Stroke"].Value))
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush) brushConverter.ConvertFromString(node.Attributes["Stroke"].Value);
                Color Stroke = (Color) ColorConverter.ConvertFromString(brush.ToString());
                lstyle.Stroke = Stroke;
            }

            if (!string.IsNullOrEmpty(node.Attributes["Symbol"].Value))
            {
                lstyle.Symbol = node.Attributes["Symbol"].Value;
            }

            if (!string.IsNullOrEmpty(node.Attributes["SymbolLib"].Value))
            {
                lstyle.SymbolLib = node.Attributes["SymbolLib"].Value;
            }

            return lstyle;
        }

        private FillStyle GetFillStyle(XmlNode node)
        {
            FillStyle fstyle = new FillStyle();

            if (!string.IsNullOrEmpty(node.Attributes["Symbol"].Value))
            {
                fstyle.Symbol = node.Attributes["Symbol"].Value;
            }

            if (!string.IsNullOrEmpty(node.Attributes["SymbolLib"].Value))
            {
                fstyle.SymbolLib = node.Attributes["SymbolLib"].Value;
            }

            return fstyle;
        }

        #endregion

        #region Write

        public void Write(XmlNode node, SectionInfo info)
        {
            if (info == null || node == null)
                return;

            gTopology.Graph graph = info.TopGraph;
            DataStruct ds = new DataStruct();
            ds.FromGraph(graph);

            XmlDocument doc = node.OwnerDocument;

            XmlElement xmlgraph = node.OwnerDocument.CreateElement(info.TopGraph.GetType().Name);
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
                xmlSegment.SetAttribute("ID", s.ID.ToString());
                xmlSegment.SetAttribute("X1", s.X1.ToString());
                xmlSegment.SetAttribute("Y1", s.Y1.ToString());
                xmlSegment.SetAttribute("X2", s.X2.ToString());
                xmlSegment.SetAttribute("Y2", s.Y2.ToString());
                xmlSegment.SetAttribute("Type", s.Type.ToString());
                xmlSegment.SetAttribute("IsStraight", s.IsStraight.ToString());
                xmlSegment.SetAttribute("IncidentHalf", s.IncidentHalf.ToString());
                xmlSegments.AppendChild(xmlSegment);
            }

            WriteStyle(node, info);
        }
        
        private void WriteStyle(XmlNode node, SectionInfo info)
        {
            XmlDocument doc = node.OwnerDocument;

            XmlElement xmlStyle = node.OwnerDocument.CreateElement("Style");
            node.AppendChild(xmlStyle);

            Type t = info.GetType();

            foreach (PropertyInfo pi in t.GetProperties())
            {
                object proptyValue = pi.GetValue(info, null);
                string proptyName = pi.Name;
                if (proptyValue.GetType() == typeof(LineStyle))
                {
                    XmlElement xmlElement = GetLineStyleElement(xmlStyle, proptyName, (LineStyle) proptyValue);
                    xmlStyle.AppendChild(xmlElement);
                }
                if (proptyValue.GetType() == typeof(FillStyle))
                {
                    XmlElement xmlElement = GetFillStyleElement(xmlStyle, proptyName, (FillStyle) proptyValue);
                    xmlStyle.AppendChild(xmlElement);
                }
                if (proptyValue.GetType() == typeof(System.Collections.Generic.Dictionary<int, LineStyle>))
                {
                    XmlElement xmlDicLineStyle = xmlStyle.OwnerDocument.CreateElement(proptyName);
                    xmlStyle.AppendChild(xmlDicLineStyle);
                    foreach (var item in info.DicLineStyle)
                    {
                        XmlElement xmlDicLs = GetLineStyleElement(xmlDicLineStyle, item.Value.GetType().Name, item.Value);
                        xmlDicLs.SetAttribute("Id", item.Key.ToString());
                        xmlDicLineStyle.AppendChild(xmlDicLs);
                    }
                }
                if (proptyValue.GetType() == typeof(System.Collections.Generic.Dictionary<int, FillStyle>))
                {
                    XmlElement xmlDicFillStyle = xmlStyle.OwnerDocument.CreateElement(proptyName);
                    xmlStyle.AppendChild(xmlDicFillStyle);
                    foreach (var item in info.DicFillStyle)
                    {
                        XmlElement xmlDicFs = GetFillStyleElement(xmlDicFillStyle, item.Value.GetType().Name, item.Value);
                        xmlDicFs.SetAttribute("Id", item.Key.ToString());
                        xmlDicFillStyle.AppendChild(xmlDicFs);
                    }
                }
            }
        }

        private XmlElement GetLineStyleElement(XmlElement p_element, string name, LineStyle lstyle)
        {
            XmlElement xmlElement = p_element.OwnerDocument.CreateElement(name);
            xmlElement.SetAttribute("Width", string.IsNullOrEmpty(lstyle.Width.ToString()) == false ? lstyle.Width.ToString() : string.Empty);
            xmlElement.SetAttribute("Stroke", string.IsNullOrEmpty(lstyle.Stroke.ToString()) == false ? lstyle.Stroke.ToString() : string.Empty);
            xmlElement.SetAttribute("Symbol", string.IsNullOrEmpty(lstyle.Symbol) == false ? lstyle.Symbol : string.Empty);
            xmlElement.SetAttribute("SymbolLib", string.IsNullOrEmpty(lstyle.SymbolLib) == false ? lstyle.SymbolLib : string.Empty);

            return xmlElement;
        }

        private XmlElement GetFillStyleElement(XmlElement p_element, string name, FillStyle fstyle)
        {
            XmlElement xmlElement = p_element.OwnerDocument.CreateElement(name);
            xmlElement.SetAttribute("Symbol", string.IsNullOrEmpty(fstyle.Symbol) == false ? fstyle.Symbol : string.Empty);
            xmlElement.SetAttribute("SymbolLib", string.IsNullOrEmpty(fstyle.SymbolLib) == false ? fstyle.SymbolLib : string.Empty);
            return xmlElement;
        }

        #endregion
    }
}
