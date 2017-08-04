using gEngine.View;
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
        public static double GetTolerance(MapControl mc, int pixelsize=5)
        {
            double x = mc.Dp2LP(pixelsize);
            double t = Math.Max(2.0, x);
            return t;
        }
    }
}
