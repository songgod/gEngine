using gEngine.Graph.Ge.Section;
using gTopology;
using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphCurveManipulator : CurveManipulator
    {
        public gTopology.Graph Graph
        {
            get
            {
                ContentPresenter p = VisualTreeHelper.GetParent(this.AssociatedObject) as ContentPresenter;
                if (p == null)
                    return null;
                SectionObject so = p.DataContext as SectionObject;
                if (so == null)
                    return null;
                return so.TopGraph;
            }
        }

        public double Tolerance
        {
            get
            {
                ContentPresenter p = VisualTreeHelper.GetParent(this.AssociatedObject) as ContentPresenter;
                if (p == null)
                    return 0;

                return CalcTolerance.GetTolerance(p);
            }
        }
    }
}
