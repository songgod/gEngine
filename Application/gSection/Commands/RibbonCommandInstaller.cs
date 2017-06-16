using gEngine.Project;
using gSection.CommandBindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gSection.Commands
{
    public class RibbonCommandInstaller: CommandInstaller
    {
        
        
            public override string Name
            {
                get
                {
                    return "RibbonCommandInstaller";
                }
            }

            public override void Install(UIElement ui)
            {
                if (ui == null)
                    return;
                ui.CommandBindings.Add(new SelectObjectCommand());
            }
        
    }
}
