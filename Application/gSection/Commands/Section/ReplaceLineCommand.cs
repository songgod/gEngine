﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.View;
using gSection.View;

namespace gSection.Commands.Section
{
    public class ReplaceLineCommand : SectionCommandBase
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

            ReplaceLineManipulator dm = new ReplaceLineManipulator();
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
