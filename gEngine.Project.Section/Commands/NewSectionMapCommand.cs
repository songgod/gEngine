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

        public ProjectControl ProjectCtrl { get; set; }

        private void NewSectionMapCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            e.CanExecute = pc != null &&
                pc.Project.GetActiveMap() != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer.Type == "WellLocation";
            e.Handled = true;
        }

        private void NewSectionMapCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            ILayer layer = pc.Project.GetActiveMap().Layers.CurrentLayer;
            if (layer.Manipulator == "WellLocationsConnectManipulator")
                layer.Manipulator = "";
            else
                layer.Manipulator = "WellLocationsConnectManipulator";

            LayerControl lc = FindLayer.Find(pc, layer);
            if(lc!=null)
            {
                IManipulators mps = ManipulatorSetter.GetManipulators(lc);
                foreach (IManipulatorBase mp in mps)
                {
                    if(mp is WellLocationsConnectManipulator)
                    {
                        ProjectCtrl = pc;
                        ((WellLocationsConnectManipulator)mp).OnFinishSelect += Mp_OnFinishSelect;
                    }
                }
            }
            
            e.Handled = true;
        }

        private void Mp_OnFinishSelect(Stack<WellLocation> wellLocs)
        {
            if (ProjectCtrl == null)
                return;

            string horizonName = string.Empty;
            string discreteName = string.Empty;
            IDBSource db = ProjectCtrl.Project.DBSource;

            List<string> horizonsNames = ProjectCtrl.Project.DBSource.HorizonsNames;
            List<string> discreteNames = ProjectCtrl.Project.DBSource.DiscreteDataNames;
            if (horizonsNames.Count != 0)
            {
                horizonName = horizonsNames[0];
            }
            if (discreteNames.Count != 0)
            {
                discreteName = discreteNames[0];
            }

            DXSectionSet SectionSet = new DXSectionSet();
            SectionSet.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SectionSetEntity sse = SectionSet.DataContext as SectionSetEntity;
            sse.BindTopAndBottomCw(db, horizonName, wellLocs);
            if (SectionSet.ShowDialog() == true)
            {
                SectionLayerCreator sc = new SectionLayerCreator();
                Layer layer = sc.CreateSectionLayer(db, wellLocs, horizonName, discreteName, sse);
                layer.Name = "剖面图";
                ILayers layers = new ILayers();
                layers.Add(layer);
                IMap map = ProjectCtrl.Project.NewMap("Ge", sse.MapName, layers);
                ProjectCtrl.Project.ActiveMap(map);
                map.Layers.CurrentIndex = 0;
            }
        }
    }
}
