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
            ui.CommandBindings.Add(new NewLineCommand());
            ui.CommandBindings.Add(new NewBoundaryCommand());
            ui.CommandBindings.Add(new NewRectCommand());
            ui.CommandBindings.Add(new NewPolyLineCommand());
            ui.CommandBindings.Add(new NewBezierLineCommand());
            ui.CommandBindings.Add(new NewCompressCommand());
            ui.CommandBindings.Add(new NewScaleRuleCommand());
            
        }
    }
}