using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
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
using System.Windows.Interactivity;

namespace GPTDxWPFRibbonApplication1.ViewModels
{
    public class RibbonViewModel : DependencyObject
    {
        public RibbonViewModel()
        {
            TabItems = new ObservableCollection<DXTabItem>();
            NewSectionSetVM.RibbonViewModelOpenPageToTab += new Action<string, string>(OpenPageToTab);
        }

        #region Property

        public FrameworkElement FullViewObject { get; set; }
        Behavior<UIElement> ManipulatorBehavior { get; set; }
        public ObservableCollection<DXTabItem> TabItems { get; set; }

        //鼠标位置
        public static readonly DependencyProperty MousePositionProperty =
            DependencyProperty.Register("MousePosition", typeof(Point), typeof(RibbonViewModel), new PropertyMetadata(new Point(-1, -1)));

        public Point MousePosition
        {
            get { return (Point)GetValue(MousePositionProperty); }
            set { SetValue(MousePositionProperty, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// 全屏显示命令
        /// </summary>
        public System.Windows.Input.ICommand FullViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    if(mc!=null)
                    {
                        mc.FullView();
                    }
                });
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
                    tabItem.IsSelected = true;
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
        /// 弹出页命令
        /// 2017-03-05
        /// </summary>
        public System.Windows.Input.ICommand ShowDialogCommand
        {
            get
            {
                return new RelayCommand<BarButtonItem>((barBtnItem) =>
                {
                    string winName = barBtnItem.Tag.ToString();
                    Assembly curAssembly = Assembly.GetExecutingAssembly();
                    Window win = (Window)curAssembly.CreateInstance(winName);
                    if (win != null)
                    {
                        win.WindowState = WindowState.Normal;
                        win.ShowDialog();
                    }
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
                return new RelayCommand<DXTabControl>(tabControl =>
                {
                    tabControl.TabHiding += (sender, e) =>
                    {
                        TabItems.Remove((DXTabItem)e.Item);
                    };
                    tabControl.MouseMove += (sender, e) =>
                    {
                        MousePosition = e.GetPosition(tabControl);
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
                        ManipulatorBehavior= ((IView)(newItem).Content).ManipulatorBehavior;
                    }

                });
            }
        }

        /// <summary>
        /// 设置操作
        /// </summary>
        public System.Windows.Input.ICommand SetManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    GraphManipulatorBase gm = (GraphManipulatorBase)ManipulatorBehavior;
                    ManipulatorSetter.SetManipulator(gm, mc.GetLayerControl(0));
                });
            }
        }


        #endregion  Commands

        #region Method

        /// <summary>
        /// 打开功能页添加到Tab选项卡
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public void OpenPageToTab(string url, string title)
        {
            DXTabItem tabItem = new DXTabItem();
            tabItem.AllowHide = DefaultBoolean.True;
            tabItem.IsSelected = true;
            tabItem.Header = title;
            UserControl uc = (UserControl)Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, url).Unwrap();
            tabItem.Content = uc;
            foreach (DXTabItem item in TabItems)
            {
                if (item.Header.ToString().Equals(tabItem.Header.ToString()))
                    return;
            }
            TabItems.Add(tabItem);
        }

        #endregion
    }
}

