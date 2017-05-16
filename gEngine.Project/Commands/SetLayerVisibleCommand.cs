using gEngine.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class SetLayerVisibleCommand : CommandBinding
    {
        public SetLayerVisibleCommand()
        {
            Command = OperateViewCommands.SetLayerVisibleCommand;
            Executed += SetLayerVisibleCommand_Executed;
            CanExecute += SetLayerVisibleCommand_CanExecute;
        }

        private void SetLayerVisibleCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //string param = e.Parameter as string;
            //int layerid = param.Substring(0, param.LastIndexOf(':'));
            throw new NotImplementedException();
        }

        private void SetLayerVisibleCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
