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
    public class NewLineSandCommand : SectionCommandBase
    {
        public NewLineSandCommand()
        {
            Command = SectionEditCommands.NewLineSandCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            //DrawLineSandManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineSandManipulator", param) as DrawLineSandManipulator;
            //if (dm == null)
            //    return;
            //ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
