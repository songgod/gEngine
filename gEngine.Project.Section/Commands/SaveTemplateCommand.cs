using gEngine.Commands;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Graph.Rw.Ge;
using gEngine.Graph.Rw.Ge.Column;
using gEngine.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            ObjectControl ObjectControl = e.Parameter as ObjectControl;
            Well Well = ObjectControl.Content as Well;

            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "数据模板文件|*.tpl";
            if (SaveFileDialog.ShowDialog() == true)
            {
                string tplName = SaveFileDialog.FileName;
                bool isSave = gEngine.Graph.Tpl.Ge.Registry.SaveTemplate(Well, tplName);

                if (isSave)
                    MessageBox.Show("数据模板保存成功！");
                else
                    MessageBox.Show("数据模板保存失败！");
            }

            e.Handled = true;
        }
    }
}
