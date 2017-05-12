using System;
using System.Windows;
using gEngine.Project;

namespace gEngine.Project.Basic
{
    public class BasicCommandsInstaller : CommandInstaller
    {
        public override string Name
        {
            get
            {
                return "BasicCommandsInstaller";
            }
        }

        public override void Install(UIElement ui)
        {
            if (ui == null)
                return;
            ui.CommandBindings.Add(new NewCommonLayerCommand());
        }
    }
}