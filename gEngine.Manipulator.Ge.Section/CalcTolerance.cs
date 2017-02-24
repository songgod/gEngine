using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Section
{
    public static class CalcTolerance
    {
        public static double GetTolerance(UIElement elm, int pixelsize=5)
        {
            Vector v = new Vector(pixelsize, pixelsize);
            Transform tf = elm.RenderTransform;
            Matrix m = Matrix.Multiply(tf.Value, Matrix.Identity);
            m.Invert();
            Vector tv = m.Transform(v);
            double t = Math.Max(2.0, Math.Max(tv.X, tv.Y));
            return t;
        }
    }
}
