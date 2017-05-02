﻿using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.View;
using gSection.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gSection.Commands.Section
{
    public class EraseFaceCommand : SectionCommandBase
    {
        public override void Execute(object parameter)
        {
            MainWindow mw = parameter as MainWindow;
            if (mw == null)
                return;

            TabControl tc = mw.TabControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;
            SetFaceTypeManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("SetFaceTypeManipulator") as SetFaceTypeManipulator;
            if (dm == null)
                return;
            dm.FaceType = -1;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
