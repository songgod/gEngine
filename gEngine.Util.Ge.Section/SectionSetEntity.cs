using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using gEngine.Graph.Ge.Column;
using System;
using System.Collections.Generic;
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

        private bool CheckUI()
        {
            if (string.IsNullOrEmpty(MapName))
            {
                MessageBox.Show("图件名不允许为空！");
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
        }

        /// <summary>
        /// 确定执行函数
        /// </summary>
        /// <param name="el"></param>
        private void Confirm(FrameworkElement el)
        {
            try
            {
                if (CheckUI())
                {
                    Window w = Window.GetWindow(el);
                    if (w == null)
                        return;
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
