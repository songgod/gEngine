using System;
using System.Windows;
using gEngine.Project;

namespace gEngine.Project.Ge.Basic.Commands
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
            ui.CommandBindings.Add(new NewLineLayerCommand());
            ui.CommandBindings.Add(new NewFillLayerCommand());
        }
    }
}