using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class EditLineCommand : SectionCommandBase
    {
        public EditLineCommand()
        {
            Command = SectionEditCommands.EditLineCommand;
        }

        public override void SetManipulator(ILayer lyr, object param)
        {
            if (lyr.Manipulator == "EditCurveManipulator")
                lyr.Manipulator = "";
            else
                lyr.Manipulator = "EditCurveManipulator";
        }
    }
}
