using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public interface IManipulatorBase
    {
        string Type { get;}
        bool CanAttach(DependencyObject elm);

        Behavior AsBehavior();
    }

    public class ManipulatorBase<T> : Behavior<T> where T : DependencyObject
    {
        public bool CanAttach(DependencyObject elm)
        {
            if (elm == null)
                return false;

            Type typ = elm.GetType();
            while (typ != null)
            {
                if (typ == AssociatedType)
                {
                    return true;
                }
                typ = typ.BaseType;
            }
            return false;
        }

        public Behavior AsBehavior()
        {
            return this;
        }
    }
    public class MapManipulator : ManipulatorBase<MapControl>, IManipulatorBase
    {
        public string Type
        {
            get
            {
                return "MapManipulator";
            }
        }
    }

    public class LayerManipulator : ManipulatorBase<LayerControl>, IManipulatorBase
    {
        public string Type
        {
            get
            {
                return "LayerManipulator";
            }
        }
    }

    public class ObjectManipulator : ManipulatorBase<ObjectControl>, IManipulatorBase
    {
        public string Type
        {
            get
            {
                return "ObjectManipulator";
            }
        }
    }
}
