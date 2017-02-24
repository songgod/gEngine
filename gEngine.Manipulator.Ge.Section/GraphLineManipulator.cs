using gTopology;
using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class GraphLineManipulator : LineManipulator
    {
        public Graph Graph
        {
            get
            {
                ContentPresenter p = VisualTreeHelper.GetParent(this.AssociatedObject) as ContentPresenter;
                if (p == null)
                    return null;
                Graph graph = p.DataContext as Graph;
                return graph;
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
