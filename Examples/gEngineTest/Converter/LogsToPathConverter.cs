using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gEngineTest.Converter
{
    public class LogsToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles vls = values[0] as ObsDoubles;
            IWell owner = values[1] as IWell;
            if (vls == null || owner == null)
                return null;


            if (owner.Depths.Count != vls.Count)
                return null;

            if (vls.Count <= 1)
                return null;

            string mathType = values[2].ToString();
            
            PathGeometry geom = new PathGeometry();
            PathFigure fg = null;
            PolyLineSegment pls = null;
            for (int i = 0; i < vls.Count; ++i)
            {
                double x = vls[i];
                if (x == -9999)
                {
                    fg = null;
                    pls = null;
                }
                else
                {
                    if (mathType.Equals(Enums.MathType.ARITHM.ToString()))
                    {
                        x = Math.Log10(x);
                    }
                    if (fg == null)
                    {
                        fg = new PathFigure();
                        fg.StartPoint = new Point() { X = x, Y = owner.Depths[i] };
                        pls = new PolyLineSegment();
                        fg.Segments.Add(pls);
                        geom.Figures.Add(fg);
                    }
                    pls.Points.Add(new Point() { X = x, Y = owner.Depths[i] });
                }
            }

            return geom;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
