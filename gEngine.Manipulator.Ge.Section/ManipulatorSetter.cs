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
        private static Canvas FindCanvas(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                Canvas canvas = child as Canvas;
                if(canvas!= null && canvas.Name == "canvas")
                {
                    ContentPresenter p = VisualTreeHelper.GetParent(canvas) as ContentPresenter;
                    if (p != null)
                    {
                        SectionObject so = p.DataContext as SectionObject;
                        if (so != null)
                        {
                            return canvas;
                        }
                    }
                }

                Canvas childOfChild = FindCanvas(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }

        public static bool SetManipulator(ManipulatorBase mp, LayerControl SectionLayer)
        {
            if (mp == null || SectionLayer == null)
                return false;

            Canvas canvas = FindCanvas(SectionLayer);
            if (canvas==null)
                return false;

            BehaviorCollection bc = Interaction.GetBehaviors(canvas);
            bc.Clear();
            bc.Add(mp);
            return true;
        }
    }
}
