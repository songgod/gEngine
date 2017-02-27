using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.View
{
    public static class FindChild
    {
        public static childitem FindVisualChild<childitem>(DependencyObject obj, string name)
            where childitem : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childitem && ((childitem)child).Name == name)
                    return (childitem)child;
                else
                {
                    childitem childOfChild = FindVisualChild<childitem>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
