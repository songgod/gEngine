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
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Project.Commands
{
    public class SetLayerVisibleCommand : CommandBinding
    {
        public SetLayerVisibleCommand()
        {
            Command = OperateViewCommands.SetLayerVisibleCommand;
            Executed += SetLayerVisibleCommand_Executed;
            CanExecute += SetLayerVisibleCommand_CanExecute;
        }

        private void SetLayerVisibleCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Image vImage = e.Parameter as Image;
            LayerCtrlObject lco = vImage.DataContext as LayerCtrlObject;
            LayerMgrControl layerMgr = FindParent.FindVisualParent<LayerMgrControl>(vImage);

            IMap map = layerMgr.MapSource;
            if (map == null)
                return;
            ILayers layers = map.Layers;
            if (layers == null)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SetLayerVisibleCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Image vImage = e.Parameter as Image;
            LayerCtrlObject lco= vImage.DataContext as LayerCtrlObject;
            LayerMgrControl layerMgr = FindParent.FindVisualParent<LayerMgrControl>(vImage);

            IMap map = layerMgr.MapSource;
            ILayers layers = map.Layers;
            foreach (ILayer layer in layers)
            {
                if (lco.Name == layer.Name)
                {
                    layer.Visible = !layer.Visible;
                    lco.VisibalityImageOpacity = layer.Visible ? 1.0 : 0.2;
                }
                if (layer.Visible)
                {
                    foreach (IObject obj in layer.Objects)
                    {
                        if (lco.Name == obj.Name)
                        {
                            obj.Visible = !obj.Visible;
                            lco.VisibalityImageOpacity = obj.Visible ? 1.0 : 0.2;
                        }
                    }
                }
            }
            e.Handled = true;
        }
    }
}
