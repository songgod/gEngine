﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.View;
using gEngine.Commands;
using gEngine.Graph.Ge.Section;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewLineSandCommand : SectionCommandBase
    {
        public NewLineSandCommand()
        {
            Command = SectionEditCommands.NewLineSandCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            //DrawLineObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineObjectManipulator", param) as DrawLineObjectManipulator;
            IManipulatorBase mp = gEngine.Manipulator.Registry.CreateManipulator("DrawLineObjectManipulator");
            if (mp == null)
                return;
            ManipulatorSetter.SetManipulator(mp, lc);
        }
    }
}
