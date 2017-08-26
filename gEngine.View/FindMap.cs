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
    public static class FindMap
    {
        public static MapControl Find(DependencyObject obj, IMap map)
        {
            if (obj == null)
                return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is MapControl && ((MapControl)child).MapContext == map)
                    return (MapControl)child;
                else
                {
                    MapControl childOfChild = Find(child, map);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
