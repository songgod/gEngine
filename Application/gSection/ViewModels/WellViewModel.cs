using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Util;
using gEngine.Util.Ge.Column;
using gEngine.Util.Ge.Section;
using gEngine.View;
using GPTDxWPFRibbonApplication1.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace GPTDxWPFRibbonApplication1.ViewModels
{
    public class WellViewModel
    {
        public WellViewModel()
        {
            _instance = this;
        }

        #region CreateInstance

        private volatile static WellViewModel _instance = null;
        private static readonly object lockHelper = new object();
        public static WellViewModel CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new WellViewModel();
                }
            }
            return _instance;
        }

        NewSectionSetViewModel vm = NewSectionSetViewModel.CreateInstance();

        #endregion

        #region Property

        private FrameworkElement _mc;
        public FrameworkElement mc
        {
            get { return _mc; }
            set
            {
                _mc = value;
                InitWell(_mc);
            }
        }

        #endregion

        #region Method

        private void InitWell(FrameworkElement mc)
        {
            Random rdm = new Random();
            Map map = new Map();
            Layer layer = SectionLayerCreator.CreateSectionLayer();

            string wellNums = DXNewSectionSet.WellNums;
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
                            well.LongitudinalProportion = vm.SLongitudinalProportion==0 ? 1500 : vm.SLongitudinalProportion;//纵向比例
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

        #endregion

        #region ICommand

        public System.Windows.Input.ICommand LoadCommand
        {
            get { return new RelayCommand<FrameworkElement>(InitWell); }
        }

        #endregion
    }
}
