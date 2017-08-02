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
    public class NewLineFaultCommand : SectionCommandBase
    {
        public NewLineFaultCommand()
        {
            Command = SectionEditCommands.NewLineFaultCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            //DrawLineFaultManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineFaultManipulator", param) as DrawLineFaultManipulator;
            //if (dm == null)
            //    return;
            //ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
