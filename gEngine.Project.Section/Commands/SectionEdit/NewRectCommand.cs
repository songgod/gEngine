using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewRectCommand : SectionCommandBase
    {
        public NewRectCommand()
        {
            Command = SectionEditCommands.NewBoundaryCommand;
        }
        public override void SetManipulator(LayerControl lc, object param)
        {
            //DrawLineObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineObjectManipulator", param) as DrawLineObjectManipulator;
            IManipulatorBase mp = gEngine.Manipulator.Registry.CreateManipulator("DrawBoundaryObjectManipulator");
            if (mp == null)
                return;
            ManipulatorSetter.SetManipulator(mp, lc);
        }
    }
}
