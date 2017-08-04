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
    class SelectObjectRibbonPageCategoryCallbackInstaller
    {
        public SelectObjectRibbonPageCategoryCallbackInstaller()
        {

            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
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
