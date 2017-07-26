using gEngine.Project.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Project.Ge.Plane.Commands
{
    public class PlaneCommandsInstaller : CommandInstaller
    {
        public override string Name
        {
            get
            {
                return "PlaneCommandsInstaller";
            }
        }

        public override void Install(UIElement ui)
        {
            if (ui == null)
                return;
            ui.CommandBindings.Add(new NewPlaneMapCommand());
            ui.CommandBindings.Add(new NewPlaneLineCommand());
        }
    }
}
