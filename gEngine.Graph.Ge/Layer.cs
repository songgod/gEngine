using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge
{
    public class Layer : Base, ILayer
    {
        public Layer()
        {
            Objects = new IObjects();
        }

        virtual public string Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public IObjects Objects
        {
            get { return (IObjects)GetValue(ObjectsProperty); }
            set { SetValue(ObjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Objects.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectsProperty =
            DependencyProperty.Register("Objects", typeof(IObjects), typeof(Layer));

        public bool Visible
        {
            get { return (bool)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(bool), typeof(Layer), new PropertyMetadata(true));



        public bool Editable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }

        
        // Using a DependencyProperty as the backing store for Editable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditableProperty =
            DependencyProperty.Register("Editable", typeof(bool), typeof(Layer), new PropertyMetadata(true));

        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof(double), typeof(Layer), new PropertyMetadata(1.0));
    }
}
