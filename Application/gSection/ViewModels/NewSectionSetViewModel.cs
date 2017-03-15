using DevExpress.Utils;
using DevExpress.Xpf.Core;
using gEngine.Util;
using gEngine.View;
using GPTDxWPFRibbonApplication1.Controls;
using GPTDxWPFRibbonApplication1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GPTDxWPFRibbonApplication1.ViewModels
{
    public class NewSectionSetViewModel : DependencyObject
    {
        public NewSectionSetViewModel()
        {
            LongitudinalProportion = new List<double>();
            LongitudinalProportion.Add(100);
            LongitudinalProportion.Add(200);
        }

        #region Property

        // 纵向比例
        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(List<double>), typeof(NewSectionSetViewModel));

        public List<double> LongitudinalProportion
        {
            get { return (List<double>)GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        #endregion

        #region Delegate

        public static event Action<string, string> RibbonViewModelOpenPageToTab;

        #endregion

        #region Method

        /// <summary>
        /// 确定执行函数
        /// </summary>
        /// <param name="w"></param>
        private void Confirm(Window w)
        {
            try
            {
                Button ok = w.FindName("button") as Button;
                string url = (string)ok.Tag;
                string title = (string)ok.ToolTip;
                if (RibbonViewModelOpenPageToTab != null)
                {
                    RibbonViewModelOpenPageToTab(url, title);
                    w.Close();
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
        /// <param name="w"></param>
        private void Cancel(Window w)
        {
            if (w != null)
            {
                w.Close();
            }
        }

        #endregion

        #region ICommand

        /// <summary>
        /// 确定命令
        /// </summary>
        public System.Windows.Input.ICommand ConfirmCommand
        {
            get { return new RelayCommand<Window>(Confirm); }
        }

        public System.Windows.Input.ICommand CancelCommand
        {
            get { return new RelayCommand<Window>(Cancel); }
        }

        #endregion
    }
}