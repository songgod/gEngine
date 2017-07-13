using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View.Ge.Basic
{
    public class RuleToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int ScaleNumber = (Int32) values[0];
            int ScaleSpace = (Int32) values[1];
            double ScaleHeight = (double) values[2];
            double Left = (double)values[3];
            PathGeometry geom = new PathGeometry();
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 0+ Left, Y = ScaleHeight / 10 * 38 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = ScaleNumber * ScaleSpace / 10 * 38+ Left, Y = ScaleHeight / 10 * 38 } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }

            for (int i = 0; i <= ScaleNumber; i++)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = i * ScaleSpace / 10 * 38+ Left, Y = 0 };
                LineSegment ls = new LineSegment() { Point = new Point() { X = i * ScaleSpace / 10 * 38+ Left, Y = ScaleHeight / 10 * 38 } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }

            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RuleTickToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int ScaleNumber = (Int32) values[0];
            int ScaleSpace = (Int32) values[1];
            double ScaleHeight = (double) values[2];
            string Unit = (string) values[3];
            double Top = (double)values[4];
            double Left = (double)values[5];
        
            PathGeometry geom = new PathGeometry();
            for (int i = 0; i <= ScaleNumber; i++)
            {
                string tick = string.Empty;
                if (i.Equals(ScaleNumber))
                {
                    tick = (ScaleNumber * ScaleSpace * 100).ToString() + Unit;
                }
                else
                {
                    tick = (i * ScaleSpace * 100).ToString();
                }
                Point p = new Point(i * ScaleSpace / 10 * 38 +Left, ScaleHeight / 10 * 38 + Top);
                PathGeometry path = GetTextPath(tick, "微软雅黑", 12, p);
                geom.AddGeometry(path);
            }
            return geom;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public PathGeometry GetTextPath(string word, string fontFamily, int fontSize, Point p)
        {
            Typeface typeface = new Typeface(new FontFamily(fontFamily), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            FormattedText text = new FormattedText(word,
                new System.Globalization.CultureInfo("zh-cn"),
                FlowDirection.LeftToRight, typeface, fontSize,
                Brushes.Black);
            text.TextAlignment = TextAlignment.Center;
            Geometry geo = text.BuildGeometry(p);
            PathGeometry path = geo.GetFlattenedPathGeometry();
            return path;
        }
    }
}
