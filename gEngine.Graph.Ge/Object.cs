using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge
{
    public abstract class Object : Base, IObject
    {
        public string DataTemplate
        {
            get { return (string)GetValue(DataTemplateProperty); }
            set { SetValue(DataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTemplateProperty =
            DependencyProperty.Register("DataTemplate", typeof(string), typeof(Base));

        public bool Visible
        {
            get { return (bool)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(bool), typeof(Object), new PropertyMetadata(true));



        public bool Editable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Editable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditableProperty =
            DependencyProperty.Register("Editable", typeof(bool), typeof(Object), new PropertyMetadata(true));

        public double Opacity
        {
            get
            {
                return (double)GetValue(OpacityProperty);
            }

            set
            {
                SetValue(OpacityProperty, value);
            }
        }

        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof(double), typeof(Object), new PropertyMetadata(1.0));
    }
}
