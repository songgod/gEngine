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
    public class SetFaceTypeManipulator : LayerManipulator
    {
        
        public SetFaceTypeManipulator()
        {
            FaceType = -1;
        }

        public int FaceType { get; set; }

        public bool InvalidFace { get; set; }

        public GraphUtil GraphUtil { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;

            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;

            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
        }

        private void Mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = GraphUtil.Graph;
            if (graph == null)
                return;

            Topology editer = new Topology(graph);
            MapControl mc = this.AssociatedObject.Owner;
            Point pos = mc.Dp2LP(e.GetPosition(mc));
            Face face = editer.FacHit(pos, GraphUtil.Tolerance);
            if (face != null)
            {
                if (InvalidFace)
                    editer.FacSetInvalid(face);
                else
                    editer.FacSetType(face, FaceType);
            }
        }
    }

    public class SFTFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "SetFaceTypeManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new SetFaceTypeManipulator();
        }
    }
}
