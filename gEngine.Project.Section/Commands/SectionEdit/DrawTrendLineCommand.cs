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
    public class DrawTrendLineCommand : SectionCommandBase
    {
        public DrawTrendLineCommand()
        {
            Command = SectionEditCommands.DrawTrendLineCommand;
        }

        public override void SetManipulator(LayerControl lc)
        {
            IManipulatorBase dm = gEngine.Manipulator.Registry.CreateManipulator("DrawBezierLineObjectManipulator");
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
