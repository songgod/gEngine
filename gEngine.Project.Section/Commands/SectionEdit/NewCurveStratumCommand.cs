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
    class NewCurveStratumCommand : SectionCommandBase
    {
        public NewCurveStratumCommand()
        {
            Command = SectionEditCommands.NewCurveStratumCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            DrawCurveStratumManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawCurveStratumManipulator", param) as DrawCurveStratumManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
