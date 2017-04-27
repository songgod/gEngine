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
        public static bool SetManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (mp == null || elm == null || !mp.CanAttach(elm))
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(elm);
            bc.Clear();
            bc.Add(mp.AsBehavior());
            return true;
        }

        public static bool AddManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (mp == null || elm == null)
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(elm);
            bc.Add(mp.AsBehavior());
            return true;
        }

        public static bool RemoveManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (mp == null || elm == null)
                return false;
            BehaviorCollection bc = Interaction.GetBehaviors(elm);
            bc.Remove(mp.AsBehavior());
            return true;
        }

        public static bool ClearManipulator(UIElement elm)
        {
            if (elm == null)
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(elm);
            bc.Clear();
            return true;
        }

        public static bool IsContainManipulator(UIElement elm)
        {
            if (elm == null)
                return false;
            BehaviorCollection bc = Interaction.GetBehaviors(elm);
            if (bc.Count > 0)
                return true;
            return false;
        }

        public static bool CanAttachManipulator(IManipulatorBase mp, UIElement elm)
        {
            if (mp == null || elm == null)
                return false;

            return mp.CanAttach(elm);
        }
    }
}
