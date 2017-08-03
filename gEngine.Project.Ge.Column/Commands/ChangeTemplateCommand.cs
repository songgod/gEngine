using gEngine.Commands;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Project.Section.Controls;
using gEngine.Util.Ge.Section;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Project.Ge.Column.Commands
{
    public class ChangeTemplateCommand : CommandBinding
    {
        public ChangeTemplateCommand()
        {
            Command = ColumnCommands.ChangeTemplateCommand;
            CanExecute += ChangeTemplateCommand_CanExecute;
            Executed += ChangeTemplateCommand_Executed;
        }

        private void ChangeTemplateCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ChangeTemplateCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;
            ObjectControl ObjectControl = e.Parameter as ObjectControl;
            Well sourceWell = ObjectControl.Content as Well;

            DXChangeTemplate dxChangeTpl = new DXChangeTemplate();

            if (dxChangeTpl.ShowDialog() == true)
            {
                string tplName = dxChangeTpl.SelTplName;
                IObject WellTpl = gEngine.Graph.Tpl.Ge.Registry.GetTemplate(typeof(Well), tplName);
                SectionLayerCreator sc = new SectionLayerCreator();
                Well destWell = sc.CreateWellByTpl(WellTpl, sourceWell) as Well;
                ObjectControl.Content = destWell;
                MessageBox.Show("数据模板更换成功！");
            }
            e.Handled = true;
        }
    }
}
