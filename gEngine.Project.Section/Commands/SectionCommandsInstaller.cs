using gEngine.Project.Commands;
using gEngine.Project.Ge.Section.Commands.SectionEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Project.Ge.Section.Commands
{
    public class SectionCommandsInstaller : CommandInstaller
    {
        public override string Name
        {
            get
            {
                return "SectionCommandsInstaller";
            }
        }

        public override void Install(UIElement ui)
        {
            if (ui == null)
                return;
            ui.CommandBindings.Add(new NewSectionMapCommand());
            ui.CommandBindings.Add(new EditLineCommand());
            ui.CommandBindings.Add(new EraseFaceCommand());
            ui.CommandBindings.Add(new EraseLineCommand());
            ui.CommandBindings.Add(new NewCurveCommand());
            ui.CommandBindings.Add(new NewFaultCommand());
            ui.CommandBindings.Add(new NewLineCommand());
            ui.CommandBindings.Add(new ReplaceLineCommand());
        }
    }
}
