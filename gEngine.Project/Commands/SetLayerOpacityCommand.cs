using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class SetLayerOpacityCommand : CommandBinding
    {
        public SetLayerOpacityCommand()
        {
            Command = OperateViewCommands.SetLayerOpacityCommand;
            Executed += SetLayerOpacityCommand_Executed;
            CanExecute += SetLayerOpacityCommand_CanExecute;
        }
        private void SetLayerOpacityCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            FrameworkElement dep = e.Parameter as FrameworkElement;
            if (dep == null)
                return;
            LayerCtrlObject lco = dep.DataContext as LayerCtrlObject;
            if (lco == null)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SetLayerOpacityCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FrameworkElement dep = e.Parameter as FrameworkElement;
            LayerCtrlObject lco = dep.DataContext as LayerCtrlObject;
            LayerMgrControl layerMgr = FindParent.FindVisualParent<LayerMgrControl>(dep);
            
            IMap map = layerMgr.MapSource;
            ILayers layers = map.Layers;
            foreach (ILayer layer in layers)
            {
                if (lco.Name == layer.Name)
                {
                    layer.Opacity = lco.LayerOpacity;
                }
                foreach (IObject obj in layer.Objects)
                {
                    if (lco.Name == obj.Name)
                    {
                        obj.Opacity = lco.LayerOpacity;
                    }
                }

            }

            e.Handled = true;
        }
    }
}
