using gEngine.Graph.Interface;
using gEngine.View;
using gSection.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands.Section
{
    public class SectionCommandBase : Command
    {
        public override bool CanExecute(object parameter)
        {
            MainWindow mw = parameter as MainWindow;
            if (mw == null)
                return false;

            TabControl tc = mw.TabControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return false;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return false;

            ILayer layer = lc.LayerContext;
            if (layer == null)
                return false;

            return layer.Type == "Section";
        }

        public override void Execute(object parameter) { }
    }
}
