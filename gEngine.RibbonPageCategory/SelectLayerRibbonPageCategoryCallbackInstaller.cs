using DevExpress.Xpf.Ribbon;
using gEngine.Graph.Interface;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.Application
{
    class SelectLayerRibbonPageCategoryCallbackInstaller
    {
        public SelectLayerRibbonPageCategoryCallbackInstaller()
        {

            MapControl.OnSelectLayerChanged += MapControl_OnSelectLayerChanged;
        }

        private void MapControl_OnSelectLayerChanged(ILayer layer)
        {
            Registry.HideAllLayerRibbonPageCategory();
            if (layer == null)
                return;

            LayerRibbonPageCategory grpc = Registry.GetLayerRibbonPageCategory(layer.GetType());

            if (grpc != null)
            {
                grpc.IsVisible = true;
                grpc.DataContext = layer;
            }
        }
    }
}
