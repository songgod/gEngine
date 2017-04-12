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

        private TxtWellLayer _txtWellLayer;
        public TxtWellLayer txtWellLayer
        {
            get { return _txtWellLayer; }
            set { _txtWellLayer = value; }
        }

        private TxtWellLayerData _txtWellLayerData;
        public TxtWellLayerData txtWellLayerData
        {
            get { return _txtWellLayerData; }
            set { _txtWellLayerData = value; }
        }

        #endregion

        #region Method

        private void InitWellLayer()
        {
            string txtfilePath = "D:\\Data\\分层界限" + ".txt";
            if (File.Exists(txtfilePath))
            {
                txtWellLayer = new TxtWellLayer() { TxtFile = txtfilePath };
            }
        }

        private void InitWellLayerData()
        {
            string txtfilePath = "D:\\Data\\分层数据" + ".txt";
            if (File.Exists(txtfilePath))
            {
                txtWellLayerData = new TxtWellLayerData() { TxtFile = txtfilePath };
            }
        }

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
                InitWellLayer();
                InitWellLayerData();
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
                            // 分层数据
                            int pointer = txtWellLayerData.WellNames.IndexOf(wellNum);
                            int lpointer = txtWellLayerData.WellNames.LastIndexOf(wellNum);
                            for (int i = 0; i < txtWellLayerData.WellLayerDatas.Count; i++)
                            {
                                WellLayerData wellLayerData = new WellLayerData() { Owner = well };
                                List<string> item2List = txtWellLayerData.WellLayerDatas[i].Item2.GetRange(pointer, lpointer - pointer + 1);
                                wellLayerData.Name = txtWellLayerData.WellLayerDatas[i].Item1;
                                wellLayerData.WellLayerDatas = item2List.ToList();
                                well.WellLayerDatas.Add(wellLayerData);
                            }

                            //IEnumerable<gEngine.Graph.Ge.Column.WellLayerData> wellLayerDataUI = well.WellLayerDatas.Select(s => s).Where(s => s.Name == "二类砂岩" || s.Name == "电测解释");
                            IEnumerable<gEngine.Graph.Ge.Column.WellLayerData> wellLayerDataUI = well.WellLayerDatas.Select(s => s).Where(s => s.Name == "二类砂岩");
                            System.Collections.IEnumerator etor1 = wellLayerDataUI.GetEnumerator();
                            while (etor1.MoveNext())
                            {
                                WellLayerData wellLayerData = new WellLayerData();
                                wellLayerData = (gEngine.Graph.Ge.Column.WellLayerData)etor1.Current;
                                well.WellLayerDatasUI.Add(wellLayerData);
                            }

                            int wellLayerDatasCount = well.WellLayerDatasUI.Count;//分层数据道数量

                            well.Name = wellNum.ToString();
                            well.LongitudinalProportion = vm.SLongitudinalProportion == 0 ? 1500 : vm.SLongitudinalProportion;//纵向比例
                            int depthsWidth = 60;// 深度道宽度
                            int colsOffset = 0; // 曲线偏移
                            int wellLayerWidth = 60;//层号道宽度
                            int wellLayerOffset = 0;//层号道偏移
                            int wellLayerDataWidth = 60;//分层数据道宽度
                            int wellLayerDataOffset = 0;//分层数据道偏移

                            well.Location = wellCount == 0 ? 0 : WellWidthSum;
                            WellWidthSum += (well.Columns.Count * 60 + depthsWidth + wellLayerWidth + wellLayerDatasCount * wellLayerDataWidth) + 50;
                            for (int i = 0; i < well.Columns.Count; i++)
                            {
                                // 初步定义深度道显示在第一条曲线右侧
                                if (i.Equals(1))
                                {
                                    colsOffset = depthsWidth + wellLayerWidth + wellLayerDatasCount * wellLayerDataWidth;
                                    well.DepthsOffset = i * 60;
                                    wellLayerOffset = i * 60 + depthsWidth;
                                    wellLayerDataOffset = i * 60 + depthsWidth + wellLayerWidth;
                                }
                                well.Columns[i].Color = Color.FromRgb((byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255));
                                well.Columns[i].Offset = colsOffset + i * 60;
                            }

                            // 层号
                            IEnumerable<gEngine.Data.Interface.IDBWellLayer> wellLayerCollections = txtWellLayer.Select(s => s).Where(s => s.Name == wellNum);
                            System.Collections.IEnumerator etor = wellLayerCollections.GetEnumerator();
                            WellLayer wellLayer = new WellLayer() { Owner = well, Offset = wellLayerOffset };
                            while (etor.MoveNext())
                            {
                                gEngine.Data.Ge.DBWellLayer dbwellLayer = (gEngine.Data.Ge.DBWellLayer)etor.Current;
                                wellLayer.BoundaryNames.Add(dbwellLayer.BoundaryName);
                                wellLayer.TopDepths.Add(dbwellLayer.TopDepth);
                                wellLayer.Thickness.Add(dbwellLayer.Thickness);
                            }

                            well.WellLayers.Add(wellLayer);

                            for (int i = 0; i < wellLayerDatasCount; i++)
                            {
                                well.WellLayerDatasUI[i].Offset = wellLayerDataOffset + i * wellLayerDataWidth;
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
