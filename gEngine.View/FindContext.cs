using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.View
{
    public static class FindContext
    {
        public static T Find<T>(FrameworkElement elm) where T : class
        {
            if (elm != null && elm.DataContext != null && elm.DataContext is T)
                return elm.DataContext as T;

            DependencyObject parentObject = VisualTreeHelper.GetParent(elm);
            return Find<T>(parentObject as FrameworkElement);
        }
    }
}
