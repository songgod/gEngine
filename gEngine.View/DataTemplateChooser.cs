using System;
using System.Windows;
using System.Windows.Controls;

namespace gEngine.View
{
    public class DataTemplateChooser : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) throw new ArgumentException();
            if (container == null) throw new ArgumentException();
            DataTemplate itemDataTemplate = null;
            itemDataTemplate = base.SelectTemplate(item, container);
            if (itemDataTemplate != null) return itemDataTemplate;

            FrameworkElement itemContainer = container as FrameworkElement;
            if (itemContainer == null) return null;

            foreach (Type itemInterface in item.GetType().GetInterfaces())
            {
                itemDataTemplate = itemContainer.TryFindResource(new DataTemplateKey(itemInterface)) as DataTemplate;
                if (itemDataTemplate != null) break;
            }
            return itemDataTemplate;
        }
    }
}
