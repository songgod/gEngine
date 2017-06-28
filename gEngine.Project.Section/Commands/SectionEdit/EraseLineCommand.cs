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
    public class EraseLineCommand : SectionCommandBase
    {
        public EraseLineCommand()
        {
            Command = SectionEditCommands.EraseLineCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            EraseLineManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("EraseLineManipulator",param) as EraseLineManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
