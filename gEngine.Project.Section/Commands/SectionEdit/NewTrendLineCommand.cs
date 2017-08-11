using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Project.Ge.Section.Commands.SectionEdit;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewTrendLineCommand : SectionCommandBase
    {
        public NewTrendLineCommand()
        {
            Command = SectionEditCommands.NewTrendLineCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            if (ManipulatorSetter.IsContainManipulator("DrawTrendLineManipulator", lc))
                ManipulatorSetter.ClearManipulator(lc);
            else
            {
                IManipulatorBase dm = gEngine.Manipulator.Registry.CreateManipulator("DrawTrendLineManipulator", param);
                if (dm == null)
                    return;
                ManipulatorSetter.SetManipulator(dm, lc);
            }
        }
    }
}
