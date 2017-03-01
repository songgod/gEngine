using gEngine.Graph.Ge.Section;
using gEngine.View;
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
                Canvas canvas = FindChild.FindVisualChild<Canvas>(this.AssociatedObject, "SectionObjectCanvas");
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

        public double Tolerance
        {
            get
            {
                Canvas canvas = FindChild.FindVisualChild<Canvas>(this.AssociatedObject, "SectionObjectCanvas");
                if (canvas == null)
                    return 0;
                ContentPresenter p = VisualTreeHelper.GetParent(canvas) as ContentPresenter;
                if (p == null)
                    return 0;

                return CalcTolerance.GetTolerance(p);
            }
        }
    }
}
