using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class SetLayerEditableCommand: CommandBinding
    {
        public SetLayerEditableCommand()
        {
            Command = OperateViewCommands.SetLayerEditableCommand;
            Executed += SetLayerEditableCommand_Executed;
            CanExecute += SetLayerEditableCommand_CanExecute;
        }

        private void SetLayerEditableCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SetLayerEditableCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Image img = e.Parameter as Image;
            LayerCtrlObject lco = img.DataContext as LayerCtrlObject;
            LayerMgrControl layerMgr = FindParent.FindVisualParent<LayerMgrControl>(img);

            IMap map = layerMgr.MapSource;
            ILayers layers = map.Layers;
            foreach (ILayer layer in layers)
            {
                if (lco.Name == layer.Name)
                {
                    layer.Editable = !layer.Editable;
                    lco.EditImageOpacity = layer.Editable ? 1.0 : 0.2;
                }
                if (layer.Visible)
                {
                    foreach (IObject obj in layer.Objects)
                    {
                        if (lco.Name == obj.Name)
                        {
                            obj.Editable = !obj.Editable;
                            lco.EditImageOpacity = obj.Editable ? 1.0 : 0.2;
                        }
                    }
                }
            }
            e.Handled = true;
        }
    }
}
