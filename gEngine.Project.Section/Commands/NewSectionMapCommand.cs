using gEngine.Commands;
using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Plane;
using gEngine.Project.Commands;
using gEngine.Project.Controls;
using gEngine.Project.Section.Controls;
using gEngine.Util.Ge.Section;
using gEngine.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Project.Ge.Section.Commands
{
    public class NewSectionMapCommand : CommandBinding
    {
        public NewSectionMapCommand()
        {
            Command = SectionCommands.NewSectionMapCommand;
            Executed += NewSectionMapCommand_Executed;
            CanExecute += NewSectionMapCommand_CanExecute;
        }

        public Project Project { get; set; }

        private void NewSectionMapCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

            e.CanExecute = layer.Type == "WellPlane";
            e.Handled = true;
        }

        private void NewSectionMapCommand_Executed(object sender, ExecutedRoutedEventArgs e)
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

            WellLocationsConnectManipulator mp = gEngine.Manipulator.Registry.CreateManipulator("WellLocationsConnectManipulator", mc) as WellLocationsConnectManipulator;
            if (mp == null)
                return;
            Project = pc.Project;
            mp.OnFinishSelect += Mp_OnFinishSelect;
            ManipulatorSetter.SetManipulator(mp, lc);

            e.Handled = true;
        }

        //private void Mp_OnFinishSelect(Stack<WellLocation> wellLocs)
        //{
        //    if (Project == null)
        //        return;

        //    string horizonName = string.Empty;
        //    string discreteName = string.Empty;
        //    IDBSource db = Project.DBSource;

        //    List<string> horizonsNames = Project.DBSource.HorizonsNames;
        //    List<string> discreteNames = Project.DBSource.DiscreteDataNames;
        //    if (horizonsNames.Count != 0)
        //    {
        //        horizonName = horizonsNames[0];
        //    }
        //    if (discreteNames.Count != 0)
        //    {
        //        discreteName = discreteNames[0];
        //    }

        //    SectionLayerCreator sc = new SectionLayerCreator();
        //    Layer layer = sc.CreateSectionLayer(db, wellLocs, horizonName, discreteName);
        //    layer.Name = "柱状图Layer图层";
        //    ILayers layers = new ILayers();
        //    layers.Add(layer);
        //    IMap map = Project.NewMap("Ge", "Column", layers);
        //    //map.Layers.Add(layer);
        //}

        private void Mp_OnFinishSelect(Stack<WellLocation> wellLocs)
        {
            if (Project == null)
                return;

            string horizonName = string.Empty;
            string discreteName = string.Empty;
            IDBSource db = Project.DBSource;

            List<string> horizonsNames = Project.DBSource.HorizonsNames;
            List<string> discreteNames = Project.DBSource.DiscreteDataNames;
            if (horizonsNames.Count != 0)
            {
                horizonName = horizonsNames[0];
            }
            if (discreteNames.Count != 0)
            {
                discreteName = discreteNames[0];
            }

            DXSectionSet SectionSet = new DXSectionSet();
            if (SectionSet.ShowDialog() == true)
            {
                Well WellTpl = gEngine.Graph.Tpl.Ge.Registry.GetTemplate(typeof(Well), SectionSet.SelTplName) as Well;

                SectionLayerCreator sc = new SectionLayerCreator();
                Layer layer = sc.CreateSectionLayer(db, wellLocs, horizonName, discreteName, WellTpl);
                layer.Name = "柱状图Layer图层";
                ILayers layers = new ILayers();
                layers.Add(layer);
                IMap map = Project.NewMap("Ge", "Column", layers);
            }
        }
    }
}
