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
using System.Windows.Data;
using System.Windows.Media;

namespace GPTDxWPFRibbonApplication1.ViewModels
{
    public class NewSectionSetViewModel : DependencyObject
    {
        public NewSectionSetViewModel()
        {
            _instance = this;
        }

        #region CreateInstance

        private volatile static NewSectionSetViewModel _instance = null;
        private static readonly object lockHelper = new object();
        public static NewSectionSetViewModel CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new NewSectionSetViewModel();
                }
            }
            return _instance;
        }

        #endregion

        #region Property

        // 纵向比例
        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(List<int>), typeof(NewSectionSetViewModel), new PropertyMetadata(InitLongitudinalProportion()));

        private List<int> LongitudinalProportion
        {
            get { return (List<int>)GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        /// <summary>
        /// 选择的纵向比例
        /// </summary>
        public int SLongitudinalProportion
        {
            get;
            private set;
        }

        // 图名
        public static readonly DependencyProperty MapNameProperty =
            DependencyProperty.Register("MapName", typeof(string), typeof(NewSectionSetViewModel), new PropertyMetadata("剖面图"));

        private string MapName
        {
            get { return (string)GetValue(MapNameProperty); }
            set { SetValue(MapNameProperty, value); }
        }

        #endregion

        #region Delegate

        public static event Action<string, string> RibbonViewModelOpenPageToTab;

        #endregion

        #region Method

        /// <summary>
        /// 初始化纵向比例
        /// </summary>
        static List<int> InitLongitudinalProportion()
        {
            List<int> yScale = new List<int>();
            yScale.Add(100);
            yScale.Add(200);
            yScale.Add(500);
            yScale.Add(1000);
            yScale.Add(1500);
            yScale.Add(2000);
            return yScale;
        }

        /// <summary>
        /// 确定执行函数
        /// </summary>
        /// <param name="el"></param>
        private void Confirm(FrameworkElement el)
        {
            try
            {
                string url = (string)el.Tag;
                string title = this.MapName;
                if (RibbonViewModelOpenPageToTab != null)
                {
                    Window w = Window.GetWindow(el);
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
        /// <param name="el"></param>
        private void Cancel(FrameworkElement el)
        {
            if (el != null)
            {
                Window w = Window.GetWindow(el);
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
            get { return new RelayCommand<FrameworkElement>(Confirm); }
        }

        public System.Windows.Input.ICommand CancelCommand
        {
            get { return new RelayCommand<FrameworkElement>(Cancel); }
        }

        public System.Windows.Input.ICommand CbVScaleCommand
        {
            get
            {
                return new RelayCommand<int>((selectItem) =>
                {
                    SLongitudinalProportion =selectItem;
                });
            }
        }

        #endregion
    }
}