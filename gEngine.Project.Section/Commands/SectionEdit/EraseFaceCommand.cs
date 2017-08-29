using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.Project.Controls;
using gEngine.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public class EraseFaceCommand : SectionCommandBase
    {
        public EraseFaceCommand()
        {
            Command = SectionEditCommands.EraseFaceCommand;
        }

        public override void SetManipulator(ILayer layer, object param)
        {
            if (layer.Manipulator == "EraseFaceManipulator")
                layer.Manipulator = "";
            else
            {
                layer.Manipulator = "EraseFaceManipulator";
            }
                
        }
    }
}
