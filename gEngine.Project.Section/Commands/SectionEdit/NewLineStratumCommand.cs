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
    public class NewLineStratumCommand : SectionCommandBase
    {
        public NewLineStratumCommand()
        {
            Command = SectionEditCommands.NewLineStratumCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            //DrawLineStratumManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineStratumManipulator", param) as DrawLineStratumManipulator;
            //if (dm == null)
            //    return;
            //ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
