using gEngine.Graph.Interface;
using gEngine.Project.Commands;
using gEngine.Project.Controls;
using gEngine.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    public abstract class SectionCommandBase : CommandBinding
    {
        public SectionCommandBase()
        {
            Executed += SectionCommandBase_Executed;
            CanExecute += SectionCommandBase_CanExecute;
        }

        private void SectionCommandBase_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;

            e.CanExecute = pc!=null &&
                pc.Project.GetActiveMap()!=null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer!=null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer.Type=="Section";
            e.Handled = true;
        }

        private void SectionCommandBase_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            ILayer layer = pc.Project.GetActiveMap().Layers.CurrentLayer;
            SetManipulator(layer, e.Parameter);
            e.Handled = true;
        }

        public abstract void SetManipulator(ILayer lyr, object param);
    }
}
