using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewCurveSandCommand : SectionCommandBase
    {
        public NewCurveSandCommand()
        {
            Command = SectionEditCommands.NewCurveSandCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            DrawCurveSandManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawCurveSandManipulator", param) as DrawCurveSandManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
