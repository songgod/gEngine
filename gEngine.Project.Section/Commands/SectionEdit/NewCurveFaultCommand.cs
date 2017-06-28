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
using gEngine.Graph.Ge.Section;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewCurveFaultCommand : SectionCommandBase
    {
        public NewCurveFaultCommand()
        {
            Command = SectionEditCommands.NewCurveFaultCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            DrawCurveFaultManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawCurveFaultManipulator", param) as DrawCurveFaultManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
