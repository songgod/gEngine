using gEngine.Graph.Interface;
using System;
using System.Windows;
using System.Windows.Controls;

namespace gEngine.View
{
    public class DataTemplateChooser : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ContentPresenter c = container as ContentPresenter;
            IObject o = item as IObject;
            if (c == null || o == null || String.IsNullOrEmpty(o.DataTemplate))
                return null;

            DataTemplate dt = c.TryFindResource(o.DataTemplate) as DataTemplate;
            return dt;
        }
    }
}
