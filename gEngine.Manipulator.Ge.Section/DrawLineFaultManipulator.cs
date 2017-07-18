using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineFaultManipulator : DrawLineManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Graph == null)
                return;

            SectionLayerEdit editor = new SectionLayerEdit(SectionLayer);
            editor.AddFault(Start, End, Tolerance);

            base.MouseLeftButtonUp(sender, e);
        }
    }

    public class DrawLineFaultManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawLineFaultManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawLineFaultManipulator();
        }
    }
}
