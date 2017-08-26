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
using gEngine.Graph.Interface;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class NewCurveFaultCommand : SectionCommandBase
    {
        public NewCurveFaultCommand()
        {
            Command = SectionEditCommands.NewCurveFaultCommand;
        }

        public override void SetManipulator(ILayer layer, object param)
        {
            if (layer.Manipulator == "DrawCurveFaultManipulator")
                layer.Manipulator = "";
            else
            {
                layer.Manipulator = "DrawCurveFaultManipulator";
            }
        }
    }
}
