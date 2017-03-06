using gEngine.Graph.Ge.Section;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphManipulatorBase : ManipulatorBase
    {
        public gTopology.Graph Graph
        {
            get
            {
                Canvas canvas = GraphContainer;
                if (canvas == null)
                    return null;
                ContentPresenter p = VisualTreeHelper.GetParent(canvas) as ContentPresenter;
                if (p == null)
                    return null;
                SectionObject so = p.DataContext as SectionObject;
                if (so == null)
                    return null;
                return so.TopGraph;
            }
        }

        public Canvas GraphContainer
        {
            get
            {
                Canvas canvas = FindChild.FindVisualChild<Canvas>(this.AssociatedObject, "SectionObjectCanvas");
                if (canvas == null)
                {
                    throw new Exception("GraphContainer should not be null");
                }
                return canvas;
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
