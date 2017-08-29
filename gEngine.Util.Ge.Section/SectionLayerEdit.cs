using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Util.Ge.Section
{
    public class SectionLayerEdit
    {
        private SectionLayer sectionlayer = null;
        private TopologySection editor = null;
        public SectionLayer SectionLayer
        {
            get
            {
                return sectionlayer;
            }
            set
            {
                sectionlayer = value;
                editor = new TopologySection(SectionLayer.SectionInfo.TopGraph);
            }
        }

        public SectionLayerEdit() { }

        public SectionLayerEdit(SectionLayer layer)
        {
            SectionLayer = layer;
        }

        public void AddFault(List<Point> points, double tolerance)
        {
            if (SectionLayer == null)
                return;
            
            editor.AddFaultLine(new PointList(points), tolerance);
            RebuildGraph();
        }

        public void AddStratum(List<Point> points, double tolerance)
        {
            editor.AddStratumLine(new PointList(points), tolerance);
            RebuildGraph();
        }

        public void AddSand(List<Point> points, double tolerance)
        {
            editor.AddSandLine(new PointList(points), tolerance);
            RebuildGraph();
        }

        public void EraseLine(List<Point> points, double tolerance)
        {
            editor.LinRemoveLine(new PointList(points));
            RebuildGraph();
        }

        public void EraseSubLine(gTopology.Line SelectLine, List<Point> points, double tolerance)
        {
            editor.LinEraseSubLine(SelectLine, new PointList(points), tolerance);
            RebuildGraph();
        }

        public gTopology.Line HitLine(Point p, double tolerance)
        {
            return editor.LinHit(p, tolerance);
        }

        public Point NearestPoint(gTopology.Line line, Point p)
        {
            return editor.LinNearestPoint(line, p);
        }

        public void ReplaceLine(gTopology.Line line, List<Point> points, double tolerance)
        {
            editor.LinReplaceSubLine(line, new PointList(points), tolerance);
            RebuildGraph();
        }

        public gTopology.Face HitFace(Point p , double tolerance)
        {
            return editor.FacHit(p, tolerance);
        }

        public void SetFaceInvalid(gTopology.Face face)
        {
            editor.FacSetInvalid(face);
            RebuildGraph();
        }

        public void SetFaceType(gTopology.Face face, int type)
        {
            editor.FacSetType(face, type);
            RebuildGraph();
        }

        public LineProxyObject GetLineProxyObject(Line line)
        {
            for (int i = SectionLayer.Objects.Count - 1; i >= 0; i--)
            {
                LineProxyObject res = SectionLayer.Objects[i] as LineProxyObject;
                if(res!=null && res.Line==line)
                {
                    return res;
                }
            }
            return null;
        }

        public void ClearGraph()
        {
            for (int i = SectionLayer.Objects.Count - 1; i >= 0; i--)
            {
                if (SectionLayer.Objects[i].GetType() == typeof(StratumFaceProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(SandFaceProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(FaultLineProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(StratumLineProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(SandLineProxyObject))
                    SectionLayer.Objects.Remove(SectionLayer.Objects[i]);
            }
        }

        public void ResetGraph()
        {
            SectionInfo sinfo = SectionLayer.SectionInfo;
            List<FaceProxyObject> ListFaces = new List<FaceProxyObject>();
            List<LineProxyObject> ListLines = new List<LineProxyObject>();
            foreach (var item in SectionLayer.Objects)
            {
                if(item is FaceProxyObject)
                {
                    ListFaces.Add(item as FaceProxyObject);
                }
                else if(item is LineProxyObject)
                {
                    ListLines.Add(item as LineProxyObject);
                }
            }
            int findex = 0;
            foreach (var item in sinfo.TopGraph.Regions)
            {
                ListFaces[findex++].Face = item;
            }
            int lindex = 0;
            foreach (var item in sinfo.TopGraph.Bounds)
            {
                ListLines[lindex++].Line = item;
            }
        }

        public void RebuildGraph()
        {
            ClearGraph();
            SectionInfo sinfo = SectionLayer.SectionInfo;
            foreach (var item in sinfo.TopGraph.Bounds)
            {
                if (item.Type == (int)SectionLineType.Fault)
                {
                    FaultLineProxyObject obj = new FaultLineProxyObject() { Line = item, SectionInfo = sinfo };
                    if (sinfo.DicLineStyle.ContainsKey(item.Id) == false)
                        obj.LineStyle = new Graph.Ge.LineStyle() { SymbolLib = "Normal", Symbol = "Solid", Stroke = Colors.Black, Width = 20.0 };
                    SectionLayer.Objects.Insert(0, obj);
                }
                else if (item.Type == (int)SectionLineType.Stratum)
                {
                    StratumLineProxyObject obj = new StratumLineProxyObject() { Line = item, SectionInfo = sinfo };
                    if (sinfo.DicLineStyle.ContainsKey(item.Id) == false)
                        obj.LineStyle = new Graph.Ge.LineStyle() { SymbolLib = "Normal", Symbol = "Solid", Stroke = Colors.Black, Width = 20.0 };
                    SectionLayer.Objects.Insert(0, obj);
                }
                else if (item.Type == (int)SectionLineType.Sand)
                {
                    SandLineProxyObject obj = new SandLineProxyObject() { Line = item, SectionInfo = sinfo };
                    if (sinfo.DicLineStyle.ContainsKey(item.Id) == false)
                        obj.LineStyle = new Graph.Ge.LineStyle() { SymbolLib = "Normal", Symbol = "Solid", Stroke = Colors.Black, Width = 20.0 };
                    SectionLayer.Objects.Insert(0, obj);
                }
            }
            foreach (var item in sinfo.TopGraph.Regions)
            {
                if (item.Type == (int)SectionLineType.Sand)
                {
                    SandFaceProxyObject obj = new SandFaceProxyObject() { Face = item, SectionInfo = sinfo };
                    if (sinfo.DicFillStyle.ContainsKey(item.InsideID) == false)
                        obj.FillStyle = new Graph.Ge.FillStyle() { SymbolLib = "Normal", Symbol = "Green" };
                    SectionLayer.Objects.Insert(0, obj);
                }
                else if(item.Type==(int)SectionLineType.Stratum)
                {
                    StratumFaceProxyObject obj = new StratumFaceProxyObject() { Face = item, SectionInfo = sinfo };
                    if (sinfo.DicFillStyle.ContainsKey(item.InsideID) == false)
                        obj.FillStyle = new Graph.Ge.FillStyle() { SymbolLib = "Normal", Symbol = "Red" };
                    SectionLayer.Objects.Insert(0,obj);
                }

            }
            
        }
    }
}
