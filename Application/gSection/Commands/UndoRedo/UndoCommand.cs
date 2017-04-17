using gEngine.Util;
using gEngine.View;
using gSection.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gSection.Commands.UndoRedo
{
    public class UndoCommand : Command
    {
        UndoRedoCommandManager _undoManager;
        public UndoCommand()
        {
            _undoManager = UndoRedoCommandManager.CreateInstance();
        }
        public override bool CanExecute(object parameter)
        {
            MainWindow mw = parameter as MainWindow;
            if (mw == null)
                return false;

            TabControl tc = mw.TabControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return false;
            if (!_undoManager.DicRedoCommands.ContainsKey(mc)) return false;
            return _undoManager.DicUndoCommands[mc].Count > 0;
        }

        public override void Execute(object parameter)
        {
            MainWindow mw = parameter as MainWindow;
            if (mw == null)
                return;

            TabControl tc = mw.TabControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;
            _undoManager.Undo(mc);
        }
    }
}
