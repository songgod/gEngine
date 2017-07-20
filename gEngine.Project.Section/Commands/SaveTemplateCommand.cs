using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Graph.Rw.Ge;
using gEngine.Graph.Rw.Ge.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Section.Commands
{
    class SaveTemplateCommand : CommandBinding
    {
        public SaveTemplateCommand()
        {
            Command = SectionCommands.SaveTemplateCommand;
            CanExecute += SaveTemplateCommand_CanExecute;
            Executed += SaveTemplateCommand_Executed;
        }

        private void SaveTemplateCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SaveTemplateCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            IObject Well = e.Parameter as IObject;

            

            ColumnReadWriterInstaller s = new ColumnReadWriterInstaller();
            s.InstallObjectReadWriter();




            //List<object> LsPara = e.Parameter as List<object>;
            //if (LsPara == null)
            //    return;

            //if (LsPara[0] == null || LsPara[1] == null)
            //    return;

            //ProjectControl pc = LsPara[0] as ProjectControl;
            //GridControl gc = LsPara[1] as GridControl;

            //if (pc == null || pc.Project == null)
            //    return;

            //int[] selectedRowHandles = gc.GetSelectedRowHandles();

            //List<string> ls = new List<string>();
            //foreach (int i in selectedRowHandles)
            //{
            //    object MapFileName = gc.GetCellValue(i, "Item1");
            //    int count = pc.Project.OpenMaps.Where(s => s.Name.Equals(MapFileName.ToString())).Count();
            //    if (count > 0)
            //    {
            //        MessageBox.Show(string.Format("您删除的图件名:{0} 正在运行，请关闭后删除！", MapFileName.ToString()));
            //        break;
            //    }
            //    ls.Add(MapFileName.ToString());
            //}

            //foreach (string mapName in ls)
            //{
            //    pc.Project.DeleteMap(mapName);
            //}

            //e.Handled = true;
        }
    }
}
