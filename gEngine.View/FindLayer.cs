using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.View
{
    public static class FindLayer
    {
        public static LayerControl Find(DependencyObject obj, ILayer layer)
        {
            if (obj == null)
                return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is LayerControl && ((LayerControl)child).LayerContext == layer)
                    return (LayerControl)child;
                else
                {
                    LayerControl childOfChild = Find(child, layer);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
