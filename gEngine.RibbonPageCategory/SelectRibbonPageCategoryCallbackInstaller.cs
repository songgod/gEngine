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
    class SelectRibbonPageCategoryCallbackInstaller
    {
        public SelectRibbonPageCategoryCallbackInstaller()
        {
            MapControl.OnSelectLayerChanged += MapControl_OnSelectLayerChanged; ;
            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private void MapControl_OnSelectLayerChanged(MapControl mc, int index)
        {
            Registry.HideAllPageCategory();
            if (mc == null)
                return;

            IMap map = mc.MapContext;
            if (map == null)
                return;

            if (index < 0 || index >= map.Layers.Count)
                return;

            ILayer layer = map.Layers[index];

            GeRibbonPageCategory grpc = Registry.GetRibbonPageCategory(layer.GetType());

            if (grpc != null)
            {
                grpc.IsVisible = true;
                grpc.DataContext = layer;
            }
        }

        private void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
            Registry.HideAllPageCategory();
            if (oc == null)
                return;

            IObject iobject = oc.DataContext as IObject;
            GeRibbonPageCategory grpc = Registry.GetRibbonPageCategory(iobject.GetType());

            if (grpc != null)
            {
                grpc.IsVisible = iobject.IsSelected;
                grpc.DataContext = iobject;
            }

        }
    }
}
