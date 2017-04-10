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


        public IObjects Objects
        {
            get { return (IObjects)GetValue(ObjectsProperty); }
            set { SetValue(ObjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Objects.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectsProperty =
            DependencyProperty.Register("Objects", typeof(IObjects), typeof(Layer));



        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(string), typeof(Layer));


    }
}
