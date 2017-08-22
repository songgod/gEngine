using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Project.Commands
{
    public class ProjectCommandsInstaller : CommandInstaller
    {
        public override string Name
        {
            get
            {
                return "ProjectCommandsInstaller";
            }
        }

        public override void Install(UIElement ui)
        {
            if (ui == null)
                return;

            ui.CommandBindings.Add(new FullViewCommand());
            ui.CommandBindings.Add(new OpenProjectCommand());
            ui.CommandBindings.Add(new SaveProjectCommand());
            ui.CommandBindings.Add(new SaveAsProjectCommand());
            ui.CommandBindings.Add(new OpenMapCommand());
            ui.CommandBindings.Add(new DeleteMapCommand());
            ui.CommandBindings.Add(new OpenRecentProjectCommand());
            ui.CommandBindings.Add(new OpenDBSourceCommand());
            ui.CommandBindings.Add(new SelectObjectCommand());
            ui.CommandBindings.Add(new PrintPngCommand());
        }
    }
}
