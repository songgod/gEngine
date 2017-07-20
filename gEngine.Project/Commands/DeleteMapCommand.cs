using DevExpress.Xpf.Grid;
using gEngine.Commands;
using gEngine.Project.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    class DeleteMapCommand : CommandBinding
    {
        public DeleteMapCommand()
        {
            Command = MapCommands.DeleteMapCommand;
            CanExecute += DeleteMapCommand_CanExecute;
            Executed += DeleteMapCommand_Executed;
        }

        private void DeleteMapCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            List<object> LsPara = e.Parameter as List<object>;
            if (LsPara == null)
                return;

            if (LsPara[0] == null || LsPara[1] == null)
                return;

            ProjectControl pc = LsPara[0] as ProjectControl;
            GridControl gc = LsPara[1] as GridControl;

            if (pc == null || pc.Project == null)
                return;

            int[] selectedRowHandles = gc.GetSelectedRowHandles();
            if (selectedRowHandles.Length <= 0)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void DeleteMapCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<object> LsPara = e.Parameter as List<object>;
            if (LsPara == null)
                return;

            if (LsPara[0] == null || LsPara[1] == null)
                return;

            ProjectControl pc = LsPara[0] as ProjectControl;
            GridControl gc = LsPara[1] as GridControl;

            if (pc == null || pc.Project == null)
                return;

            int[] selectedRowHandles = gc.GetSelectedRowHandles();

            List<string> ls = new List<string>();
            foreach (int i in selectedRowHandles)
            {
                object MapFileName = gc.GetCellValue(i, "Item1");
                int count = pc.Project.OpenMaps.Where(s => s.Name.Equals(MapFileName.ToString())).Count();
                if (count > 0)
                {
                    MessageBox.Show(string.Format("您删除的图件名:{0} 正在运行，请关闭后删除！", MapFileName.ToString()));
                    break;
                }
                ls.Add(MapFileName.ToString());
            }

            foreach (string mapName in ls)
            {
                pc.Project.DeleteMap(mapName);
            }

            e.Handled = true;
        }
    }
}
