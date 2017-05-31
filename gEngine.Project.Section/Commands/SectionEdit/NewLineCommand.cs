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
    public class NewLineCommand : SectionCommandBase
    {
        public NewLineCommand()
        {
            Command = SectionEditCommands.NewLineCommand;
        }

        public override void SetManipulator(LayerControl lc)
        {
            DrawLineManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineManipulator",SectionObject.LineType.StratumLine) as DrawLineManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
