using DevExpress.Xpf.Bars;
using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.Project.Section.Converters;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    class NewCurveStratumCommand : SectionCommandBase
    {
        public NewCurveStratumCommand()
        {
            Command = SectionEditCommands.NewCurveStratumCommand;
        }

        public override void SetManipulator(ILayer layer, object param)
        {
            if (layer.Manipulator == "DrawCurveStratumManipulator")
                layer.Manipulator = "";
            else
            {
                layer.Manipulator = "DrawCurveStratumManipulator";
            }
        }
    }
}
