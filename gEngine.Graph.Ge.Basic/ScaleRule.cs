using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Basic
{
    public class ScaleRule : Object
    {
        public string Unit
        {
            get { return (string) GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(ScaleRule));

        public int ScaleNumber
        {
            get { return (int) GetValue(ScaleNumberProperty); }
            set { SetValue(ScaleNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleNumberProperty =
            DependencyProperty.Register("ScaleNumber", typeof(int), typeof(ScaleRule));

        public int ScaleSpace
        {
            get { return (int) GetValue(ScaleSpaceProperty); }
            set { SetValue(ScaleSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleSpaceProperty =
            DependencyProperty.Register("ScaleSpace", typeof(int), typeof(ScaleRule));

        public double ScaleHeight
        {
            get { return (double) GetValue(ScaleHeightProperty); }
            set { SetValue(ScaleHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleHeightProperty =
            DependencyProperty.Register("ScaleHeight", typeof(double), typeof(ScaleRule));

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(ScaleRule));



        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(ScaleRule));
    }
}
