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

        public enum SectionLineType
        {
            UnKnown = 0,
            Fault,
            Stratum,
            Sand
        }

        public void AddFault(List<Point> points, double tolerance)
        {
            if (SectionLayer == null)
                return;

            AddCurve(SectionLayer.SandObject, points, tolerance, SectionLineType.Fault);
            AddCurve(SectionLayer.StratumObject, points, tolerance, SectionLineType.Fault);
        }

        public void AddStratum(List<Point> points, double tolerance)
        {
            AddCurve(SectionLayer.StratumObject, points, tolerance, SectionLineType.Stratum);
        }

        public void AddStratum(Point start, Point end, double tolerance)
        {
            AddLine(SectionLayer.StratumObject, start, end, tolerance, SectionLineType.Stratum);
        }

        public void AddSand(List<Point> points, double tolerance)
        {
            AddCurve(SectionLayer.SandObject, points, tolerance, SectionLineType.Sand);
        }

        public void AddSand(Point start, Point end, double tolerance)
        {
            AddLine(SectionLayer.SandObject, start, end, tolerance, SectionLineType.Sand);
        }

        private void AddCurve(SectionObject obj, List<Point> points, double tolerance, SectionLineType type)
        {
            if (obj != null)
            {
                Topology editor = new Topology(obj.TopGraph);
                editor.LinAddCurve(new PointList(points), tolerance,false, (int)type);
            }
        }

        private void AddLine(SectionObject obj, Point start, Point end, double tolerance, SectionLineType type)
        {
            if (obj != null)
            {
                Topology editor = new Topology(obj.TopGraph);
                editor.LinAddLine(start, end, tolerance, (int)type);
            }
        }

    }
}
