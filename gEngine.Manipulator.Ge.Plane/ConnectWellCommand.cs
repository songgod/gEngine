using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace gEngine.Manipulator.Ge.Plane
{
    public class ConnectWellCommand : IUndoRedoCommand
    {
        private WellLocationsConnectManipulator mp;

        public ConnectWellCommand(Behavior<UIElement> behavior)
        {
            this.mp = behavior as WellLocationsConnectManipulator;
        }


        public void Undo()
        {
            mp.Undo();
        }

        public void Redo()
        {
            mp.Redo();
        }
    }
}
