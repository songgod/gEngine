using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.View;
using gEngine.Graph.Interface;

namespace gEngine.Manipulator
{
    class ManipulatorChangedCallbackInstaller
    {
        public ManipulatorChangedCallbackInstaller()
        {
            MapControl.OnManipulatorChanged += MapControl_OnManipulatorChanged;
            LayerControl.OnManipulatorChanged += LayerControl_OnManipulatorChanged;
            ObjectControl.OnManipulatorChanged += ObjectControl_OnManipulatorChanged;
        }

        private void MapControl_OnManipulatorChanged(MapControl mc, string manipulator)
        {
            if (mc == null)
                return;

            if (!String.IsNullOrWhiteSpace(manipulator))
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(manipulator);
                ManipulatorSetter.SetManipulator(mb, mc);
            }
            else
            {
                ManipulatorSetter.ClearManipulator(mc);
            }
        }

        private void LayerControl_OnManipulatorChanged(LayerControl lc, string manipulator)
        {
            if (lc == null)
                return;

            if (!String.IsNullOrWhiteSpace(manipulator))
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(manipulator);
                ManipulatorSetter.SetManipulator(mb, lc);
            }
            else
            {
                ManipulatorSetter.ClearManipulator(lc);
            }
        }

        private void ObjectControl_OnManipulatorChanged(ObjectControl oc, string manipulator)
        {
            if (oc == null)
                return;

            IObject obj = oc.DataContext as IObject;
            if (obj == null)
                return;

            if (!String.IsNullOrWhiteSpace(manipulator) && obj.IsSelected)
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(manipulator);
                ManipulatorSetter.SetManipulator(mb, oc);
            }
            else
            {
                ManipulatorSetter.ClearManipulator(oc);
            }
        }
    }
}
