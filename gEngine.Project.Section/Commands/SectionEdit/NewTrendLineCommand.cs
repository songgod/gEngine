using DevExpress.Xpf.Bars;
using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Project.Ge.Section.Commands.SectionEdit;
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
    public class NewTrendLineCommand : SectionCommandBase
    {
        public NewTrendLineCommand()
        {
            Command = SectionEditCommands.NewTrendLineCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            if (lc.Manipulator == "DrawTrendLineManipulator")
                lc.Manipulator = "";
            else
                lc.Manipulator = "DrawTrendLineManipulator";
        }
    }
}
