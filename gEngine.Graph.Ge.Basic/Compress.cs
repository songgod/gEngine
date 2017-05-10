using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Basic
{
    /// <summary>
    /// 指北针
    /// </summary>
    public class Compress : Object
    {
        public string CompressData
        {
            get { return (string)GetValue(CompressDataProperty); }
            set { SetValue(CompressDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompressDataProperty =
            DependencyProperty.Register("CompressData", typeof(string), typeof(Compress));


        public double RotateAngle
        {
            get { return (double)GetValue(RotateAngleProperty); }
            set { SetValue(RotateAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(double), typeof(Compress));
    }
}
