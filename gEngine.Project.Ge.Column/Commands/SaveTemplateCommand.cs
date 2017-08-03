﻿using gEngine.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using gEngine.View;
using System.Windows;
using gEngine.Graph.Ge.Column;

namespace gEngine.Project.Ge.Column.Commands
{
    class SaveTemplateCommand : CommandBinding
    {
        public SaveTemplateCommand()
        {
            Command =ColumnCommands.SaveTemplateCommand;
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

            string tplName = Microsoft.VisualBasic.Interaction.InputBox("请输入文件名", "数据模板保存");

            if (!string.IsNullOrEmpty(tplName))
            {
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