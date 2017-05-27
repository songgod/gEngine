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
            ui.CommandBindings.Add(new SetLayerVisibleCommand());
            ui.CommandBindings.Add(new SetLayerEditableCommand());
            ui.CommandBindings.Add(new OpenMapsSelectCommand());
            ui.CommandBindings.Add(new SetLayerOpacityCommand());
        }
    }
}
