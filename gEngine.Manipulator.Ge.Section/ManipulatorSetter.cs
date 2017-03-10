using gEngine.Graph.Ge.Section;
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

namespace gEngine.Manipulator.Ge.Section
{
    public static class ManipulatorSetter
    {
        public static bool SetManipulator(ManipulatorBase mp, LayerControl SectionLayer)
        {
            if (mp == null || SectionLayer == null)
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(SectionLayer);
            bc.Clear();
            bc.Add(mp);
            return true;
        }

        public static bool ClearManipulator(LayerControl SectionLayer)
        {
            if (SectionLayer == null)
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(SectionLayer);
            bc.Clear();
            return true;
        }

        public static bool IsContainManipulator(this LayerControl SectionLayer)
        {
            if (SectionLayer == null)
                return false;
            BehaviorCollection bc = Interaction.GetBehaviors(SectionLayer);
            if (bc.Count > 0)
                return true;
            return false;
        }
    }
}
