using gEngine.Graph.Ge.Section;
using gTopology;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class SetFaceTypeManipulator : ManipulatorBase
    {
        public SetFaceTypeManipulator()
        {
            FaceType = -1;
        }
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

        public int FaceType { get; set; }

        public bool InvalidFace { get; set; }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = Graph;
            if (graph == null)
                return;

            Topology editer = new Topology(graph);
            Point pos = e.GetPosition(this.AssociatedObject);
            Face face = editer.FacHit(pos, Tolerance);
            if (face != null)
            {
                if(InvalidFace)
                    editer.FacSetInvalid(face);
                else
                    editer.FacSetType(face, FaceType);
            }
                
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
