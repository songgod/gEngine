using gEngine.Graph.Ge.Section;
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
        public SectionLayer SectionLayer { get; set; }

        public SectionLayerEdit() { }

        public SectionLayerEdit(SectionLayer layer)
        {
            SectionLayer = layer;
        }

        public void AddFault(List<Point> points, double tolerance)
        {
            if (SectionLayer == null)
                return;

            TopologySection editor = new TopologySection(SectionLayer.SectionInfo.TopGraph);
            editor.AddFaultLine(new PointList(points), tolerance);
            RebuildGraph();
        }

        public void AddStratum(List<Point> points, double tolerance)
        {
            TopologySection editor = new TopologySection(SectionLayer.SectionInfo.TopGraph);
            editor.AddStratumLine(new PointList(points), tolerance);
            RebuildGraph();
        }

        public void AddSand(List<Point> points, double tolerance)
        {
            TopologySection editor = new TopologySection(SectionLayer.SectionInfo.TopGraph);
            editor.AddSandLine(new PointList(points), tolerance);
            RebuildGraph();
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

        public void RebuildGraph()
        {
            ClearGraph();
            SectionInfo sinfo = SectionLayer.SectionInfo;
            foreach (var item in sinfo.TopGraph.Regions)
            {
                if(item.Type==(int)SectionLineType.Stratum)
                {
                    StratumFaceProxyObject obj = new StratumFaceProxyObject() { Face = item, SectionInfo = sinfo };
                    if (sinfo.DicFillStyle.ContainsKey(item.InsideID) == false)
                        obj.FillStyle = new Graph.Ge.FillStyle() { SymbolLib = "Normal", Symbol = "Red" };
                    SectionLayer.Objects.Add(obj);
                }
                else if(item.Type==(int)SectionLineType.Sand)
                {
                    SandFaceProxyObject obj = new SandFaceProxyObject() { Face = item, SectionInfo = sinfo };
                    if (sinfo.DicFillStyle.ContainsKey(item.InsideID) == false)
                        obj.FillStyle = new Graph.Ge.FillStyle() { SymbolLib = "Normal", Symbol = "Green" };
                    SectionLayer.Objects.Add(obj);
                }
            }
            foreach (var item in sinfo.TopGraph.Bounds)
            {
                if (item.Type == (int)SectionLineType.Fault)
                {
                    FaultLineProxyObject obj = new FaultLineProxyObject() { Line = item, SectionInfo = sinfo };
                    if (sinfo.DicLineStyle.ContainsKey(item.Id) == false)
                        obj.LineStyle = new Graph.Ge.LineStyle() { SymbolLib = "Normal", Symbol = "Solid", Stroke=Colors.Black,Width=20.0 };
                    SectionLayer.Objects.Add(obj);
                }  
                else if (item.Type == (int)SectionLineType.Stratum)
                {
                    StratumLineProxyObject obj = new StratumLineProxyObject() { Line = item, SectionInfo = sinfo };
                    if (sinfo.DicLineStyle.ContainsKey(item.Id) == false)
                        obj.LineStyle = new Graph.Ge.LineStyle() { SymbolLib = "Normal", Symbol = "Solid", Stroke = Colors.Black, Width = 20.0 };
                    SectionLayer.Objects.Add(obj);
                }
                else if (item.Type == (int)SectionLineType.Sand)
                {
                    SandLineProxyObject obj = new SandLineProxyObject() { Line = item, SectionInfo = sinfo };
                    if (sinfo.DicLineStyle.ContainsKey(item.Id) == false)
                        obj.LineStyle = new Graph.Ge.LineStyle() { SymbolLib = "Normal", Symbol = "Solid", Stroke = Colors.Black, Width = 20.0 };
                    SectionLayer.Objects.Add(obj);
                }
            }
        }
    }
}
