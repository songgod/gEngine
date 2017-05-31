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
    public class NewFaultCommand : SectionCommandBase
    {
        public NewFaultCommand()
        {
            Command = SectionEditCommands.NewFaultCommand;
        }

        public override void SetManipulator(LayerControl lc)
        {
            DrawLineManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineManipulator", SectionObject.LineType.FaultLine) as DrawLineManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
