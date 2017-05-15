using gEngine.Commands;
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

        public override void SetManipulator(LayerControl lc)
        {
            EditCurveManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("EditCurveManipulator") as EditCurveManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
