using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Utility;
using gEngine.View;
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

namespace gEngineTest
{
    /// <summary>
    /// WellColumnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WellColumnWindow : Window
    {
        public Well well { get; set; }

        public WellColumnWindow()
        {
            InitializeComponent();
            InitWell();
        }

        private void InitWell()
        {
            Layer layer = new Layer();
            well = new Well();
            int curveCount = 0;//定义曲线的条数
            bool colFlag = true;//曲线标题的标识
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //string filePath = path + "Data\\MulWellColumnData.txt";
            //string filePath = path + "Data\\AC.txt";
            string filePath = "D:\\Data\\MulWellColumnData.txt";
            var file = File.Open(filePath, FileMode.Open);

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
                                    wellColumn.MathTyp = Enums.MathType.LINER;
                                    wellColumn.Owner = well;
                                    well.Columns.Add(wellColumn);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < curveCount; i++)
                            {
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
            layer.Objects.Add(well);
            CoordTrans();
            this.lyControl.SetBinding(LayerControl.ItemsSourceProperty, new Binding("Objects") { Source = layer });
        }

        public double wellColumnWidth = 100;
        private void CoordTrans()
        {
            int i = 0;
            foreach (WellColumn wc in well.Columns)
            {
                double[] xMinList = wc.Values.Select(s => s).Where(s => (s != -9999)).ToArray();
                double[] xMaxList = wc.Values.Select(s => s).Where(s => (s != -9999)).ToArray();
                double xMin = xMinList.Min();
                double xMax = xMaxList.Max();
                well.Columns[i].XOffset = Math.Floor(xMin) * -1;
                well.Columns[i].YOffset = well.Depths[0] * -1;
                well.Columns[i].ScaleX = 100 / (xMax - xMin);
                i++;
            }
        }
    }
}
