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
            MapControl.OnLayerControlSelected += MapControl_OnLayerControlSelected;
            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private void MapControl_OnLayerControlSelected(LayerControl oc)
        {
            Registry.HideAllPageCategory();
            if (oc == null)
                return;
            
            GeRibbonPageCategory grpc = Registry.GetRibbonPageCategory(oc.LayerContext.GetType());

            if (grpc != null)
            {
                grpc.IsVisible = true;
                grpc.DataContext = oc.LayerContext;
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
