﻿using gEngine.Data.Ge;
using gEngine.Data.Interface;
using gEngine.Graph.Ge;
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

            WellLocationsConnectManipulator mp = new WellLocationsConnectManipulator(mc);
            mp.OnFinishSelect += Mp_OnFinishSelect;
            ManipulatorSetter.SetManipulator(mp, lc);
        }

        //private void Mp_OnFinishSelect(HashSet<string> names)
        //{
        //    DBWells wells = new DBWells();
        //    foreach (string name in names)
        //    {
        //        IDBWell wl = Project.Single.DBFactory.GetWell(name);
        //        if (wl != null)
        //            wells.Add(wl);
        //    }

        //    SectionLayerCreator sc = new SectionLayerCreator();
        //    Layer layer = sc.CreateSectionLayer(wells);
        //    gEngine.Graph.Ge.Map map = new gEngine.Graph.Ge.Map() { Name = "Column" };
        //    map.Layers.Add(layer);
        //    Project.Single.Maps.Add(new Tuple<string, IMap>(null, map));
        //    Project.Single.OpenMaps.Add(map);
        //}

        private void Mp_OnFinishSelect(HashSet<string> names)
        {
            string horizonName = string.Empty;
            string discreteName = string.Empty;
            IDBFactory db = Project.Single.DBFactory;

            List<string> horizonsNames = Project.Single.DBFactory.HorizonsNames;
            List<string> discreteNames = Project.Single.DBFactory.DiscreteDataNames;
            if (horizonsNames.Count != 0)
            {
                horizonName = horizonsNames[0];
            }
            if (discreteNames.Count != 0)
            {
                discreteName = discreteNames[0];
            }
            SectionLayerCreator sc = new SectionLayerCreator();
            Layer layer = sc.CreateSectionLayer(db, names, horizonName, discreteName);
            gEngine.Graph.Ge.Map map = new gEngine.Graph.Ge.Map() { Name = "Column" };
            map.Layers.Add(layer);
            Project.Single.Maps.Add(new Tuple<string, IMap>(null, map));
            Project.Single.OpenMaps.Add(map);
        }
    }
}
