using gEngine.Commands;
using gEngine.Graph.Interface;
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
    public class NewCurveSandCommand : SectionCommandBase
    {
        public NewCurveSandCommand()
        {
            Command = SectionEditCommands.NewCurveSandCommand;
        }

        public override void SetManipulator(ILayer layer, object param)
        {
            if (layer.Manipulator == "DrawCurveSandManipulator")
                layer.Manipulator = "";
            else
            {
                layer.Manipulator = "DrawCurveSandManipulator";
            }
        }
    }
}
