using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge.Section
{
    public class SectionInfo
    {
        public SectionInfo()
        {
            TopGraph = new gTopology.Graph();
            DicLineStyle = new Dictionary<int, LineStyle>();
            DicFillStyle = new Dictionary<int, FillStyle>();
            DefaultFaultLineStyle = LineStyle.Default;
            DefaultStratumLineStyle = LineStyle.Default;
            DefaultSandLineStyle = LineStyle.Default;

            DefaultStratumFillStyle = new FillStyle() { Symbol = "Red", SymbolLib = "Normal" };
            DefaultSandFillStyle = new FillStyle() { Symbol = "Green", SymbolLib = "Normal" };
        }

        public gTopology.Graph TopGraph { get; set; }

        public Dictionary<int,LineStyle> DicLineStyle { get; set; }

        public Dictionary<int, FillStyle> DicFillStyle { get; set; }

        public LineStyle DefaultFaultLineStyle { get; set; }
        public LineStyle DefaultStratumLineStyle { get; set; }
        public LineStyle DefaultSandLineStyle { get; set; }
        public FillStyle DefaultStratumFillStyle { get; set; }
        public FillStyle DefaultSandFillStyle { get; set; }

        public LineStyle GetFaultLineStyle(int id)
        {
            if (DicLineStyle.ContainsKey(id))
                return DicLineStyle[id];
            return DefaultFaultLineStyle;
        }

        public LineStyle GetStratumLineStyle(int id)
        {
            if (DicLineStyle.ContainsKey(id))
                return DicLineStyle[id];
            return DefaultStratumLineStyle;
        }

        public LineStyle GetSandLineStyle(int id)
        {
            if (DicLineStyle.ContainsKey(id))
                return DicLineStyle[id];
            return DefaultSandLineStyle;
        }

        public void SetLineStyle(int id, LineStyle ls)
        {
            DicLineStyle[id] = ls;
        }

        public FillStyle GetStratumFillStyle(int id)
        {
            if (DicFillStyle.ContainsKey(id))
                return DicFillStyle[id];
            return DefaultStratumFillStyle;
        }

        public FillStyle GetSandFillStyle(int id)
        {
            if (DicFillStyle.ContainsKey(id))
                return DicFillStyle[id];
            return DefaultSandFillStyle;
        }

        public void SetFillStyle(int id, FillStyle fs)
        {
            DicFillStyle[id] = fs;
        }
    }
}
