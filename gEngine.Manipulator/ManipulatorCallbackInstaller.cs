using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.View;

namespace gEngine.Manipulator
{
    class ManipulatorCallbackInstaller
    {
        public ManipulatorCallbackInstaller()
        {
            LayerControl.OnManipulatorChanged += LayerControl_OnManipulatorChanged;
            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private void LayerControl_OnManipulatorChanged(LayerControl oc, string manipulator)
        {
            if (oc == null)
                return;
            if(String.IsNullOrWhiteSpace(manipulator))
            {
                ManipulatorSetter.ClearManipulator(oc);
            }
            else
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(manipulator);
                ManipulatorSetter.SetManipulator(mb, oc);
            }
        }

        private void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
            if (oc == null || oc.ObjectContext == null)
                return;
            if (oc.IsSelected)
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateObjectManipulator(oc.ObjectContext);
                ManipulatorSetter.SetManipulator(mb, oc);
            }
            else
            {
                ManipulatorSetter.ClearManipulator(oc);
            }
        }
    }
}
