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

namespace gEngine.Project.Section.Commands.SectionEdit
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
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;

            ILayer layer = lc.LayerContext;
            if (layer == null)
                return;

            e.CanExecute = layer.Type == "Section";
            e.Handled = true;
        }

        private void SectionCommandBase_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;
            SetManipulator(lc);
            e.Handled = true;
        }

        public abstract void SetManipulator(LayerControl lc);
    }
}
