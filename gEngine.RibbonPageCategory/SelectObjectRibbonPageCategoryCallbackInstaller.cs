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
    static class SelectObjectRibbonPageCategoryCallbackInstaller
    {
        static SelectObjectRibbonPageCategoryCallbackInstaller()
        {

            ObjectControl.OnObjectControlSelected += ObjectControl_OnObjectControlSelected;
        }

        private static void ObjectControl_OnObjectControlSelected(ObjectControl oc)
        {
            IObject iobject = oc.DataContext as IObject;
            GeRibbonPageCategory grpc = Registry.GetRibbonPageCategory(iobject.GetType());
            if (grpc != null)
            {
                BindingExpression exp = grpc.GetBindingExpression(GeRibbonPageCategory.IsVisibleProperty);
                if (exp == null)
                {
                    //绑定1：将iobject的IsSelected属性绑定到ribbon的IsVisibleProperty属性上
                    Binding bd = new Binding("IsSelected");
                    bd.Source = iobject;
                    bd.Mode = BindingMode.OneWay;
                    grpc.SetBinding(RibbonPageCategory.IsVisibleProperty, bd);

                    //绑定2：将iobject绑定到ribbon的datacontext上
                    grpc.DataContext = iobject;
                }
            }
        }
    }
}
