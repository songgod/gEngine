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

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewCurveCommand : SectionCommandBase
    {
        public NewCurveCommand()
        {
            Command = SectionEditCommands.NewCurveCommand;
        }

        public override void SetManipulator(LayerControl lc)
        {
            DrawCurveManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawCurveManipulator") as DrawCurveManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}