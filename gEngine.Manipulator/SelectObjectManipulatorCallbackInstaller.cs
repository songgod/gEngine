﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.View;

namespace gEngine.Manipulator
{
    static class SelectObjectManipulatorCallbackInstaller
    {
        static SelectObjectManipulatorCallbackInstaller()
        {

            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private static void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
            if (oc == null || oc.ObjectContext == null)
                return;
            if (oc.IsSelected)
            {
                IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(oc.ObjectContext);
                ManipulatorSetter.SetManipulator(mb, oc);
            }
            else
            {
                ManipulatorSetter.ClearManipulator(oc);
            }
        }
    }
}
