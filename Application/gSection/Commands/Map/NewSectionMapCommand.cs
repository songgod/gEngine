using gEngine.Data.Ge;
using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Plane;
using gEngine.Util.Ge.Section;
using gEngine.View;
using gSection.View;
using gSection.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gSection.Commands.Map
{
    public class NewSectionMapCommand : Command
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

            return layer.Type == "WellPlane";
        }

        public override void Execute(object parameter)
        {
            MainWindow mw = parameter as MainWindow;
            if (mw == null)
                return;

            TabControl tc = mw.TabControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;
            WellLocationsConnectManipulator mp = gEngine.Manipulator.Registry.CreateManipulator("WellLocationsConnectManipulator", mc) as WellLocationsConnectManipulator;
            if (mp == null)
                return;
            mp.OnFinishSelect += Mp_OnFinishSelect;
            ManipulatorSetter.SetManipulator(mp, lc);
        }

        private void Mp_OnFinishSelect(Stack<WellLocation> wellLocs)
        {
            string horizonName = string.Empty;
            string discreteName = string.Empty;
            IDBSource db = Project.Single.DBSource;

            List<string> horizonsNames = Project.Single.DBSource.HorizonsNames;
            List<string> discreteNames = Project.Single.DBSource.DiscreteDataNames;
            if (horizonsNames.Count != 0)
            {
                horizonName = horizonsNames[0];
            }
            if (discreteNames.Count != 0)
            {
                discreteName = discreteNames[0];
            }
            SectionLayerCreator sc = new SectionLayerCreator();
            Layer layer = sc.CreateSectionLayer(db, wellLocs, horizonName, discreteName);
            IMap map = Project.Single.NewMap("Ge", "Column");
            map.Layers.Add(layer);
        }
    }
}
