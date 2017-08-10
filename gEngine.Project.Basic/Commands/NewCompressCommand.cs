using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Basic;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Basic.Commands
{
    public class NewCompressCommand : CommandBinding
    {
        public NewCompressCommand()
        {
            Command = BasicCommands.NewCompressCommand;
            Executed += NewCompressCommand_Executed;
            CanExecute += NewCompressCommand_CanExecute;
        }

        private void NewCompressCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            //if (pc.MapsControl.ActiveMapControl == null)
            //    return;

            //MapControl mc = pc.MapsControl.ActiveMapControl;
            //if (mc == null)
            //    return;

            //LayerControl layer = mc.ActiveLayerControl;
            //if (layer == null)
            //    return;

            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewCompressCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            //if (pc.MapsControl.ActiveMapControl == null)
            //    return;

            //MapControl mc = pc.MapsControl.ActiveMapControl;
            //if (mc == null)
            //    return;

            //LayerControl lc = mc.ActiveLayerControl;
            //if (lc == null)
            //    return;

            //DrawCompressObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawCompressObjectManipulator") as DrawCompressObjectManipulator;
            //if (dm == null)
            //    return;
            //ManipulatorSetter.SetManipulator(dm, lc);

            Graph.Interface.IMap map = pc.Project.NewMap("Ge", "Commpress");


            //Graph.Ge.FillStyle fl = new Graph.Ge.FillStyle();





           Graph.Ge.Layer layer = new Graph.Ge.Layer() { Type = "Commpress" };


           gEngine.Graph.Ge.Basic.Comprass com = new Graph.Ge.Basic.Comprass()
            {
                Width = 40,
                Height = 50,
                Top = 70,
                Left = 80,
                Fill = System.Windows.Media.Brushes.Red,
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 0,
                RotateAngle = 0
            };


            layer.Objects.Add(com);




          map.Layers.Add(layer);
        }
    }
}
