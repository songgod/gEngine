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

            AddCurve(points, tolerance, SectionLineType.Fault);
        }

        public void AddFault(Point start, Point end, double tolerance)
        {
            AddLine(start, end, tolerance, SectionLineType.Fault);
        }

        public void AddStratum(List<Point> points, double tolerance)
        {
            AddCurve(points, tolerance, SectionLineType.Stratum);
        }

        public void AddStratum(Point start, Point end, double tolerance)
        {
            AddLine(start, end, tolerance, SectionLineType.Stratum);
        }

        public void AddSand(List<Point> points, double tolerance)
        {
            AddCurve(points, tolerance, SectionLineType.Sand);
        }

        public void AddSand(Point start, Point end, double tolerance)
        {
            AddLine(start, end, tolerance, SectionLineType.Sand);
        }

        private void AddCurve(List<Point> points, double tolerance, SectionLineType type)
        {
            if (SectionLayer != null)
            {
                Topology editor = new Topology(SectionLayer.SectionInfo.TopGraph);
                editor.LinAddCurve(new PointList(points), tolerance,false, (int)type);
                RebuildGraph();
            }
        }

        private void AddLine(Point start, Point end, double tolerance, SectionLineType type)
        {
            if (SectionLayer != null)
            {
                Topology editor = new Topology(SectionLayer.SectionInfo.TopGraph);
                editor.LinAddLine(start, end, tolerance, (int)type);
                RebuildGraph();
            }
        }

        public void ClearGraph()
        {
            for (int i = SectionLayer.Objects.Count - 1; i >= 0; i--)
            {
                if (SectionLayer.Objects[i].GetType() == typeof(NodeProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(LineProxyObject) ||
                    SectionLayer.Objects[i].GetType() == typeof(FaceProxyObject))
                    SectionLayer.Objects.Remove(SectionLayer.Objects[i]);
            }
        }

        public void RebuildGraph()
        {
            ClearGraph();
            SectionInfo sinfo = SectionLayer.SectionInfo;
            foreach (var item in sinfo.TopGraph.Nods)
            {
                SectionLayer.Objects.Add(new NodeProxyObject() { Node = item, SectionInfo=sinfo });
            }
            foreach (var item in sinfo.TopGraph.Bounds)
            {
                SectionLayer.Objects.Add(new LineProxyObject() { Line = item, SectionInfo = sinfo });
            }
            foreach (var item in sinfo.TopGraph.Regions)
            {
                SectionLayer.Objects.Add(new FaceProxyObject() { Face = item, SectionInfo = sinfo });
            }
        }
    }
}
