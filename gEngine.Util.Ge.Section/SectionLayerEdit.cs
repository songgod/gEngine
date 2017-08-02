using gEngine.Graph.Ge.Section;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                if (SectionLayer.Objects[i].GetType() == typeof(LineProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(FaceProxyObject))
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
                    SectionLayer.Objects.Add(new StratumFaceProxyObject() { Face = item, SectionInfo = sinfo });
                else if(item.Type==(int)SectionLineType.Sand)
                    SectionLayer.Objects.Add(new SandFaceProxyObject() { Face = item, SectionInfo = sinfo });
            }
            foreach (var item in sinfo.TopGraph.Bounds)
            {
                if (item.Type == (int)SectionLineType.Fault)
                    SectionLayer.Objects.Add(new FaultLineProxyObject() { Line = item, SectionInfo = sinfo });
                else if (item.Type == (int)SectionLineType.Stratum)
                    SectionLayer.Objects.Add(new StratumLineProxyObject() { Line = item, SectionInfo = sinfo });
                else if (item.Type == (int)SectionLineType.Sand)
                    SectionLayer.Objects.Add(new SandLineProxyObject() { Line = item, SectionInfo = sinfo });
            }
        }
    }
}
