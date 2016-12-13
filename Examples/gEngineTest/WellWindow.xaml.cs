using gEngine.Graph.Ge;
using System;
using System.Collections.Generic;
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
using System.Globalization;
using gEngine.Utility;
using gEngine.Graph.Interface;
using System.IO;

namespace gEngineTest
{
    public class DepthToPathConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObsDoubles dbs = value as ObsDoubles;
            if (dbs == null || dbs.Count<=1)
                return null;

            PathGeometry geom = new PathGeometry();

            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = dbs[0] };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 1, Y = dbs[dbs.Count-1] } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }


            for (int i=0;i<dbs.Count;++i)
            {
                PathFigure fg = new PathFigure();
                fg.StartPoint = new Point() { X = 1, Y = dbs[i] };
                LineSegment ls = new LineSegment() { Point = new Point() { X = 15, Y = dbs[i] } };
                fg.Segments.Add(ls);
                geom.Figures.Add(fg);
            }
            return geom;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LogsToPathConveter : IMultiValueConverter
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

            PathGeometry geom = new PathGeometry();
            PathFigure fg = null;
            PolyLineSegment pls = null;
            for (int i = 0; i < vls.Count; ++i)
            {
                double x = vls[i];
                if(x==-9999)
                {
                    fg = null;
                    pls = null;
                }
                else
                {
                    if(fg==null)
                    {
                        fg = new PathFigure();
                        fg.StartPoint = new Point() { X = vls[i], Y = owner.Depths[i] };
                        pls = new PolyLineSegment();
                        fg.Segments.Add(pls);
                        geom.Figures.Add(fg);
                    }
                    pls.Points.Add(new Point() { X = vls[i], Y = owner.Depths[i] });
                }
            }

            return geom;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// WellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WellWindow : Window
    {
        public Well well { get; set; }

        private void InitWell(String file)
        {
            well = new Well();
            int curveCount = 0;//定义曲线的条数
            bool colFlag = true;//曲线标题的标识

            using (var stream = new StreamReader(file, Encoding.GetEncoding("gb2312")))
            {
                while (!stream.EndOfStream)
                {
                    string strLine = System.Text.RegularExpressions.Regex.Replace(stream.ReadLine().Trim().ToString(), @"\s+", " ");
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] strColumns = strLine.Split(' ');
                        curveCount = strColumns.Length;
                        if (colFlag)
                        {
                            colFlag = false;
                            for (int i = 0; i < curveCount; i++)
                            {
                                if (i.Equals(0))
                                {
                                    well.Name = strColumns[i];
                                }
                                else
                                {
                                    WellColumn wellColumn = new WellColumn();
                                    wellColumn.Name = strColumns[i];
                                    wellColumn.Owner = well;
                                    well.Columns.Add(wellColumn);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < curveCount; i++)
                            {
                                Dictionary<double, double> dict = new Dictionary<double, double>();
                                if (i.Equals(0))
                                {
                                    well.Depths.Add(double.Parse(strColumns[i]));
                                }
                                else
                                {
                                    double xValue = double.Parse(strColumns[i]);
                                    well.Columns[i - 1].Values.Add(xValue);
                                }
                            }
                        }
                    }
                }
            }
        }

        public WellWindow()
        {
            InitWell("c:\\MulWellColumnData.txt");

            this.DataContext = this;
            InitializeComponent();
        }
    }
}
