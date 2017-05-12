using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Project
{
    public abstract class CommandInstaller
    {
        public abstract string Name { get; }
        public abstract void Install(UIElement ui);
    }
}
