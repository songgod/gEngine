using gEngine.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Project.Ge.Column.Commands
{
    public class ColumnCommandsInstaller : CommandInstaller
    {
        public override string Name
        {
            get
            {
                return "ColumnCommandsInstaller";
            }
        }

        public override void Install(UIElement ui)
        {
            if (ui == null)
                return;
            ui.CommandBindings.Add(new SaveTemplateCommand());
            ui.CommandBindings.Add(new ChangeTemplateCommand());
        }
    }
}
