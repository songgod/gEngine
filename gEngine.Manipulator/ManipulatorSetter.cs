using gEngine.Manipulator;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public static class ManipulatorSetter
    {
        public static IManipulators GetManipulators(DependencyObject obj)
        {
            IManipulators mps =  (IManipulators)obj.GetValue(ManipulatorsProperty);
            if (mps == null)
            {
                mps = new IManipulators();
                obj.SetValue(ManipulatorsProperty, mps);
            }
            return mps;
        }

        public static void SetManipulators(DependencyObject obj, IManipulators value)
        {
            obj.SetValue(ManipulatorsProperty, value);
        }

        // Using a DependencyProperty as the backing store for MapManipulator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManipulatorsProperty =
            DependencyProperty.RegisterAttached("Manipulators", typeof(IManipulators), typeof(ManipulatorSetter), new PropertyMetadata(null));

        public static bool AddManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (elm == null || mp==null)
                return false;
            
            if (mp.CanAttach(elm))
            {
                BehaviorCollection bc = Interaction.GetBehaviors(elm);
                bc.Add(mp.AsBehavior());
                IManipulators ms = GetManipulators(elm);
                ms.Add(mp);
                return true;
            }

            return false;
        }

        public static bool RemoveManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (elm == null || mp == null)
                return false;

            IManipulators ms = GetManipulators(elm);
            BehaviorCollection bs = Interaction.GetBehaviors(elm);
            foreach (IManipulatorBase m in ms)
            {
                if (m == mp)
                {
                    if(bs.Contains(m.AsBehavior()))
                        bs.Remove(m.AsBehavior());
                    ms.Remove(m);
                    return true;
                }
            }

            return false;
        }

        public static bool RemoveManipulator(Type mptype, UIElement elm)
        {
            if (elm == null)
                return false;


            IManipulators ms = GetManipulators(elm);
            BehaviorCollection bs = Interaction.GetBehaviors(elm);
            foreach (IManipulatorBase m in ms)
            {
                if (m.GetType() == mptype)
                {
                    if (bs.Contains(m.AsBehavior()))
                        bs.Remove(m.AsBehavior());
                    ms.Remove(m);
                    return true;
                }
            }

            return false;
        }

        public static bool RemoveManipulator(string mptype, UIElement elm)
        {
            if (elm == null)
                return false;


            IManipulators ms = GetManipulators(elm);
            BehaviorCollection bs = Interaction.GetBehaviors(elm);
            foreach (IManipulatorBase m in ms)
            {
                if (m.GetType().Name == mptype)
                {
                    if (bs.Contains(m.AsBehavior()))
                        bs.Remove(m.AsBehavior());
                    ms.Remove(m);
                    return true;
                }
            }

            return false;
        }

        public static bool SetManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (elm == null)
                return false;

            if (mp == null)
            {
                ClearManipulator(elm);
                return true;
            }
            else if (mp.CanAttach(elm))
            {
                BehaviorCollection bc = Interaction.GetBehaviors(elm);
                bc.Clear();
                bc.Add(mp.AsBehavior());
                IManipulators ms = GetManipulators(elm);
                ms.Add(mp);
                return true;
            }

            return false;
        }

        public static IManipulators GetManipulators(UIElement elm)
        {
            if (elm == null)
                return null;

            IManipulators mps = elm.GetValue(ManipulatorsProperty) as IManipulators;
            if(mps==null)
            {
                mps = new IManipulators();
                elm.SetValue(ManipulatorsProperty, mps);
            }
            return mps;
        }

        public static bool ClearManipulator(UIElement elm)
        {
            if (elm == null)
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(elm);
            bc.Clear();
            IManipulators ms = GetManipulators(elm);
            ms.Clear();
            return true;
        }

        public static bool IsContainManipulator(string type, UIElement elm)
        {
            if (elm == null)
                return false;

            IManipulators ms = GetManipulators(elm);
            foreach (IManipulatorBase m in ms)
            {
                if (m.GetType().Name == type)
                    return true;
            }

            return false;
        }

        public static bool IsContainManipulator(Type type, UIElement elm)
        {
            if (elm == null)
                return false;

            IManipulators ms = GetManipulators(elm);
            foreach (IManipulatorBase m in ms)
            {
                if (m.GetType() == type)
                    return true;
            }

            return false;
        }

        public static bool IsContainManipulator(UIElement elm)
        {
            if (elm == null)
                return false;
            IManipulators ms = GetManipulators(elm);
            if(ms.Count>0)
                return true;
            return false;
        }

        public static bool CanAttachManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (mp == null || mp == null)
                return false;

            return mp.CanAttach(elm);
        }
    }
}
