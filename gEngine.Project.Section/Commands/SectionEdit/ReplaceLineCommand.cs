using System;
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
    public class ReplaceLineCommand : SectionCommandBase
    {
        public ReplaceLineCommand()
        {
            Command = SectionEditCommands.ReplaceLineCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            ReplaceLineManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("ReplaceLineManipulator",param) as ReplaceLineManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
