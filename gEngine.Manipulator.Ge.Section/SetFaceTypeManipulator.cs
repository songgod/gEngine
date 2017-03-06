using gEngine.Graph.Ge.Section;
using gEngine.View;
using gTopology;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public class SetFaceTypeManipulator : GraphManipulatorBase
    {
        public SetFaceTypeManipulator()
        {
            FaceType = -1;
        }

        public int FaceType { get; set; }

        public bool InvalidFace { get; set; }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = Graph;
            if (graph == null)
                return;

            Topology editer = new Topology(graph);
            Point pos = e.GetPosition(GraphContainer);
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
