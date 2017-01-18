using gEngine.Graph.Ge;
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
using static gEngine.Graph.Ge.Enums;

namespace gEngine.View.Datatemplate.Converter
{
    public class LogsToPathConverter : IMultiValueConverter
    {
        public LogsToPathConverter()
        {
            InvalidValue = -9999;
        }

        public double InvalidValue { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles vls = values[0] as ObsDoubles;
            Well owner = values[1] as Well;
            if (vls == null || owner == null)
                return null;


            if (owner.Depths.Count != vls.Count)
                return null;

            if (vls.Count <= 1)
                return null;

            double mindpeth = owner.Depths[0];
            double[] xMinList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();
            double[] xMaxList = vls.Select(s => s).Where(s => (s != InvalidValue)).ToArray();
            double xMin = xMinList.Min();
            double xMax = xMaxList.Max();

            MathType mathType = (MathType)values[2];
            if (mathType == MathType.DEFAULT)
            {
                if (xMax-xMin>100)
                    mathType = MathType.ARITHM;
                else
                    mathType = MathType.LINER;
            }
            
            PathGeometry geom = new PathGeometry();
            PathFigure fg = null;
            PolyLineSegment pls = null;
            for (int i = 0; i < vls.Count; ++i)
            {
                if (vls[i] == InvalidValue)
                {
                    fg = null;
                    pls = null;
                }
                else
                {
                    double x = vls[i] - xMin;
                    if (mathType.Equals(Enums.MathType.ARITHM))
                    {
                        x = Math.Log10(vls[i])-Math.Log10(xMin);
                    }

                    if (fg == null)
                    {
                        fg = new PathFigure();
                        fg.StartPoint = new Point() { X = x, Y = owner.Depths[i] - mindpeth };
                        pls = new PolyLineSegment();
                        fg.Segments.Add(pls);
                        geom.Figures.Add(fg);
                    }
                    pls.Points.Add(new Point() { X = x, Y = owner.Depths[i] - mindpeth });
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
