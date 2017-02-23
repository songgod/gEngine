using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using gEngine.Util;
using gEngine.View;
using GPTDxWPFRibbonApplication1.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GPTDxWPFRibbonApplication1.ViewModels
{
    public class RibbonViewModel
    {
        public RibbonViewModel()
        {
            TabItems = new ObservableCollection<DXTabItem>();
        }

        #region Property
        public FrameworkElement FullViewObject { get; set; }
        public ObservableCollection<DXTabItem> TabItems { get; set; }
        #endregion

        #region Commands

        /// <summary>
        /// 全屏显示命令
        /// </summary>
        public System.Windows.Input.ICommand FullViewCommand
        {
            get
            {
                return new RelayCommand(() => ViewUtil.FullView(FullViewObject));
            }
        }

        /// <summary>
        /// 打开页签页命令
        /// </summary>
        public System.Windows.Input.ICommand TabOpenCommand
        {
            get
            {
                return new RelayCommand<BarButtonItem>((barBtnItem) =>
                {
                    DXTabItem tabItem = new DXTabItem();
                    tabItem.Header = barBtnItem.Content;
                    tabItem.AllowHide = DefaultBoolean.True;
                    string ucName = barBtnItem.Tag.ToString();
                    UserControl uc = (UserControl)Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, ucName).Unwrap();
                    tabItem.Content = uc;

                    foreach (DXTabItem item in TabItems)
                    {
                        if (item.Header == tabItem.Header)
                            return;
                    }
                    TabItems.Add(tabItem);
                });
            }
        }

        /// <summary>
        /// 加载页签命令
        /// </summary>
        public System.Windows.Input.ICommand TabLoadedCommand
        {
            get
            {
                return new RelayCommand<DXTabControl>(tabControl => {
                    tabControl.TabHiding += (sender, e) => {
                        TabItems.Remove((DXTabItem)e.Item);
                    };
                });
            }
        }
        /// <summary>
        /// 切换页签命令
        /// </summary>
        public System.Windows.Input.ICommand TabChangedCommand
        {
            get
            {
                return new RelayCommand<DXTabControl>(tabControl =>
                {
                    DXTabItem newItem = tabControl.GetTabItem(tabControl.SelectedIndex);
                    if (newItem != null)
                    {
                        FullViewObject = ((IView)(newItem).Content).FullScreenObject;
                    }

                });
            }
        }

        #endregion  Commands

    }
}

