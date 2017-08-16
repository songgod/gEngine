using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using gEngine.Data.Interface;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Plane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Util.Ge.Section
{
    public class SectionSetEntity : DependencyObject
    {
        public SectionSetEntity()
        {
            BindTemplate();
            BindLongitudinalProportion();
            BindHorizontalProportion();
        }

        #region Property

        public List<int> LongitudinalProportion
        {
            get { return (List<int>) GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(List<int>), typeof(SectionSetEntity));

        /// <summary>
        /// 选择的纵向比例
        /// </summary>
        public int SLongitudinalProportion
        {
            get;
            private set;
        }

        public List<int> HorizontalProportion
        {
            get { return (List<int>) GetValue(HorizontalProportionProperty); }
            set { SetValue(HorizontalProportionProperty, value); }
        }

        public static readonly DependencyProperty HorizontalProportionProperty =
            DependencyProperty.Register("HorizontalProportion", typeof(List<int>), typeof(SectionSetEntity));

        /// <summary>
        /// 选择的横向比例
        /// </summary>
        public int SHorizontalProportion
        {
            get;
            private set;
        }

        public List<string> TplNames
        {
            get { return (List<string>) GetValue(TplNamesProperty); }
            set { SetValue(TplNamesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TplNamesProperty =
            DependencyProperty.Register("TplNames", typeof(List<string>), typeof(SectionSetEntity));

        public string SelTplName
        {
            get;
            private set;
        }

        public string MapName
        {
            get { return (string) GetValue(MapNameProperty); }
            set { SetValue(MapNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapNameProperty =
            DependencyProperty.Register("MapName", typeof(string), typeof(SectionSetEntity));

        public double TopYs
        {
            get { return (double) GetValue(TopYsProperty); }
            set { SetValue(TopYsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopYsProperty =
            DependencyProperty.Register("TopYs", typeof(double), typeof(SectionSetEntity));

        public double BottomYs
        {
            get { return (double) GetValue(BottomYsProperty); }
            set { SetValue(BottomYsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomYsProperty =
            DependencyProperty.Register("BottomYs", typeof(double), typeof(SectionSetEntity));

        public ObservableCollection<string> TopCw
        {
            get { return (ObservableCollection<string>) GetValue(TopCwProperty); }
            set { SetValue(TopCwProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopCwProperty =
            DependencyProperty.Register("TopCw", typeof(ObservableCollection<string>), typeof(SectionSetEntity));

        public string SelTopCw
        {
            get;
            private set;
        }

        public ObservableCollection<string> BottomCw
        {
            get { return (ObservableCollection<string>) GetValue(BottomCwProperty); }
            set { SetValue(BottomCwProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomCwProperty =
            DependencyProperty.Register("BottomCw", typeof(ObservableCollection<string>), typeof(SectionSetEntity));

        public string SelBottomCw
        {
            get;
            private set;
        }

        #endregion

        #region Method

        private void BindTemplate()
        {
            TplNames = gEngine.Graph.Tpl.Ge.Registry.GetTemplateNames(typeof(Well));
        }

        private void BindLongitudinalProportion()
        {
            LongitudinalProportion = new List<int>();
            LongitudinalProportion.Add(100);
            LongitudinalProportion.Add(200);
            LongitudinalProportion.Add(500);
            LongitudinalProportion.Add(1000);
            LongitudinalProportion.Add(1500);
            LongitudinalProportion.Add(2000);
        }

        private void BindHorizontalProportion()
        {
            HorizontalProportion = new List<int>();
            HorizontalProportion.Add(10);
            HorizontalProportion.Add(50);
            HorizontalProportion.Add(100);
            HorizontalProportion.Add(200);
            HorizontalProportion.Add(500);
            HorizontalProportion.Add(1000);
            HorizontalProportion.Add(2000);
            HorizontalProportion.Add(3000);
        }

        public void BindTopAndBottomCw(IDBSource db, string horizonName, Stack<WellLocation> wellLocs)
        {
            List<string> lsLayerNums = new List<string>();
            foreach (WellLocation wl in wellLocs)
            {
                IDBHorizons horizons = db.GetHorizonsByWell(wl.Name, horizonName);
                for (int i = 0; i < horizons.Horizons.Count; i++)
                {
                    if (string.IsNullOrEmpty(horizons.Horizons[i].LayerNumber))
                        continue;
                    lsLayerNums.Add(horizons.Horizons[i].LayerNumber);
                }
            }

            lsLayerNums = lsLayerNums.GroupBy(t => t).Select(t => t.First()).ToList();

            TopCw = new ObservableCollection<string>(lsLayerNums);
            BottomCw = new ObservableCollection<string>(lsLayerNums);
        }

        private bool CheckUI(FrameworkElement el)
        {

            if (string.IsNullOrEmpty(MapName))
            {
                MessageBox.Show("图件名不允许为空！");
                return false;
            }

            if (string.IsNullOrEmpty(TopYs.ToString()))
            {
                MessageBox.Show("顶部延伸不允许为空！");
                return false;
            }

            if (string.IsNullOrEmpty(BottomYs.ToString()))
            {
                MessageBox.Show("底部延伸不允许为空！");
                return false;
            }

            ComboBoxEdit cbTopCw = el.FindName("cbTopCw") as ComboBoxEdit;
            if (cbTopCw == null)
            {
                MessageBox.Show("未找到顶部层位！");
                return false;
            }

            ComboBoxEdit cbBottomCw = el.FindName("cbBottomCw") as ComboBoxEdit;
            if (cbBottomCw == null)
            {
                MessageBox.Show("未找到底部层位！");
                return false;
            }

            if (cbTopCw.SelectedItemValue == null)
            {
                MessageBox.Show("顶部层位不允许为空！");
                return false;
            }

            if (cbBottomCw.SelectedItemValue == null)
            {
                MessageBox.Show("底部层位不允许为空！");
                return false;
            }

            int idxTopCw = cbTopCw.SelectedIndex;
            int idxBottomCw = cbBottomCw.SelectedIndex;

            if (idxTopCw > idxBottomCw)
            {
                MessageBox.Show("顶部层位大于底部层位，请重新选择！");
                return false;
            }

            return true;
        }

        private void GetSelectedData(FrameworkElement el)
        {
            ComboBoxEdit cbTemplate = el.FindName("cbTemplate") as ComboBoxEdit;
            if (cbTemplate != null)
            {
                SelTplName = cbTemplate.SelectedItemValue == null ? string.Empty : cbTemplate.SelectedItemValue.ToString();
            }

            ComboBoxEdit cbVScale = el.FindName("cbVScale") as ComboBoxEdit;
            if (cbVScale != null)
            {
                SLongitudinalProportion = cbVScale.SelectedItemValue == null ? 1500 : Int32.Parse(cbVScale.SelectedItemValue.ToString());
            }

            ComboBoxEdit cbHScale = el.FindName("cbHScale") as ComboBoxEdit;
            if (cbHScale != null)
            {
                SHorizontalProportion = cbHScale.SelectedItemValue == null ? 100 : Int32.Parse(cbHScale.SelectedItemValue.ToString());
            }

            ComboBoxEdit cbTopCw = el.FindName("cbTopCw") as ComboBoxEdit;
            ComboBoxEdit cbBottomCw = el.FindName("cbBottomCw") as ComboBoxEdit;

            SelTopCw = cbTopCw.SelectedItemValue.ToString();
            SelBottomCw = cbBottomCw.SelectedItemValue.ToString();
        }

        /// <summary>
        /// 确定执行函数
        /// </summary>
        /// <param name="el"></param>
        private void Confirm(FrameworkElement el)
        {
            try
            {
                Window w = Window.GetWindow(el);
                if (w == null)
                    return;
                if (CheckUI(w))
                {
                    GetSelectedData(w);
                    w.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 取消执行按钮
        /// </summary>
        /// <param name="el"></param>
        private void Cancel(FrameworkElement el)
        {
            if (el != null)
            {
                Window w = Window.GetWindow(el);
                if (w == null)
                    return;
                w.DialogResult = false;
            }
        }

        #endregion

        #region ICommand

        /// <summary>
        /// 确定命令
        /// </summary>
        public System.Windows.Input.ICommand ConfirmCommand
        {
            get { return new DelegateCommand<FrameworkElement>(Confirm); }
        }

        public System.Windows.Input.ICommand CancelCommand
        {
            get { return new DelegateCommand<FrameworkElement>(Cancel); }
        }

        #endregion
    }
}
