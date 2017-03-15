using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using gEngine.View;
using System.Windows.Interactivity;
using GPTDxWPFRibbonApplication1.ViewModels;
using System.IO;
using System.Text;
using gEngine.Graph.Interface;
using gEngine.Graph.Ge.Column;
using System;
using System.Windows.Media;

namespace GPTDxWPFRibbonApplication1.Controls
{
    /// <summary>
    /// DWellControl.xaml 的交互逻辑
    /// </summary>
    public partial class DWellControl : UserControl, IView
    {
        #region IView接口实现
        FrameworkElement IView.FullScreenObject
        {
            get { return mc; }
            set { mc = (MapControl)value; }
        }

        Behavior<UIElement> IView.ManipulatorBehavior
        {
            get; set;
        }
        #endregion

        //private string _wellNums;
        //public string WellNums
        //{
        //    get { return _wellNums; }
        //    set { _wellNums = value;
        //        MessageBox.Show(_wellNums);
        //    }
        //}

        public DWellControl()
        {
            InitializeComponent();
            InitWell();
        }

        private void InitWell()
        {
            Random rdm = new Random();

            Map map = new Map();
            Layer layer = new Layer();

            string wellNums = New_section_set.WellNums;
            int wellCount = 0;
            int WellWidthSum = 0;

            if (!string.IsNullOrEmpty(wellNums))
            {
                string[] split = wellNums.Split(',');
                foreach (string wellNum in split)
                {
                    string txtfilePath = "D:\\Data\\" + wellNum + ".txt";
                    if (File.Exists(txtfilePath))
                    {
                        TxtWell tw = new TxtWell() { TxtFile = txtfilePath };
                        WellCreator wc = new WellCreator();
                        IObjects res = new IObjects();
                        res = wc.Create(tw);
                        foreach (Well well in res)
                        {
                            well.Name = wellNum.ToString();
                            well.LongitudinalProportion = 1500;//纵向比例
                            int depthsWidth = 60;// 深度道宽度
                            int colsOffset = 0; // 曲线偏移
                            well.Location = wellCount == 0 ? 0 : WellWidthSum;
                            WellWidthSum += (well.Columns.Count * 60 + depthsWidth) + 50;
                            for (int i = 0; i < well.Columns.Count; i++)
                            {
                                // 初步定义深度道显示在第一条曲线右侧
                                if (i.Equals(1))
                                {
                                    colsOffset = depthsWidth;
                                    well.DepthsOffset = i * 60;
                                }
                                well.Columns[i].Color = Color.FromRgb((byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255));
                                well.Columns[i].Offset = colsOffset + i * 60;
                            }

                            wellCount++;
                            layer.Objects.Add(well);
                        }
                    }
                }
            }

            map.Layers.Add(layer);
            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }
    }
}
