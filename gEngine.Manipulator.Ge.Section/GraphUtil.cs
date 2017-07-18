using gEngine.Graph.Ge.Section;
using gEngine.View;
using gEngine.View.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphUtil
    {
        private gTopology.Graph graph = null;
        private Canvas graphcontainer = null;
        private LayerControl lyrctrl = null;

        public GraphUtil(LayerControl lc)
        {
            lyrctrl = lc;
        }

        public gTopology.Graph Graph
        {
            get
            {
                if (graph != null)
                    return graph;

                Canvas canvas = GraphContainer;
                if (canvas == null)
                    return null;
                ContentPresenter p = VisualTreeHelper.GetParent(canvas) as ContentPresenter;
                if (p == null)
                    return null;
                SectionInfo so = p.DataContext as SectionInfo;
                if (so == null)
                    return null;
                graph = so.TopGraph;
                return graph;
            }
        }

        public Canvas GraphContainer
        {
            get
            {
                if (graphcontainer != null)
                    return graphcontainer;
                if (lyrctrl == null)
                {
                    throw new Exception("LayerControl should not be null");
                }

                Canvas canvas = FindChild.FindVisualChild<Canvas>(lyrctrl, "SectionObjectCanvas");
                if (canvas == null)
                {
                    throw new Exception("GraphContainer should not be null");
                }
                graphcontainer = canvas;
                return graphcontainer;
            }
        }

        public double Tolerance
        {
            get
            {
                ContentPresenter p = VisualTreeHelper.GetParent(GraphContainer) as ContentPresenter;
                if (p == null)
                    return 0;

                return CalcTolerance.GetTolerance(p);
            }
        }
    }
}
