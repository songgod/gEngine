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
            IManipulatorBase dm = gEngine.Manipulator.Registry.CreateManipulator("DrawBezierLineObjectManipulator",param);
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
