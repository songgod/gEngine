using DevExpress.Xpf.Ribbon;
using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.Util;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    class OpenMapCommand : CommandBinding
    {
        public OpenMapCommand()
        {
            Command = MapCommands.OpenMapCommand;
            CanExecute += OpenMapCommand_CanExecute;
            Executed += OpenMapCommand_Executed;
        }

        private void OpenMapCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
        
        private void OpenMapCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DoubleClickTimer.IsDoubleClick())
            {
                List<object> LsPara = e.Parameter as List<object>;

                if (LsPara == null)
                    return;

                if (LsPara[0] == null || LsPara[1] == null)
                    return;

                ProjectControl pc = LsPara[0] as ProjectControl;
                TextBlock TextBlock = LsPara[1] as TextBlock;
                string MapFileName = TextBlock.Text;

                if (pc == null || pc.Project == null)
                    return;
                pc.Project.OpenMap(MapFileName);
                pc.MapsControl.SelectLast();
                e.Handled = true;
            }
        }
    }
}
