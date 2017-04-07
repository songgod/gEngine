using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Plane;
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
            NewSectionSetViewModel.RibbonViewModelOpenPageToTab += new Action<string, string>(OpenPageToTab);
            DicUndoCommands = new Dictionary<DXTabItem, Stack<IUndoRedoCommand>>();
            DicRedoCommands = new Dictionary<DXTabItem, Stack<IUndoRedoCommand>>();
        }

        #region Property

        public FrameworkElement FullViewObject { get; set; }
        //各类画线操作等
        private Behavior<UIElement> manipulatorBehavior;
        public Behavior<UIElement> ManipulatorBehavior
        {
            get
            {
                return manipulatorBehavior;
            }
            set
            {
                manipulatorBehavior = value;
                if (value is WellLocationsConnectManipulator)
                {
                    ((WellLocationsConnectManipulator)ManipulatorBehavior).OnDrawWellLine += RibbonViewModel_OnDrawWellLine;
                }
            }
        }
        public ObservableCollection<DXTabItem> TabItems { get; set; }
        public DXTabItem CurrentTabItem { get; set; }

        //鼠标位置
        public static readonly DependencyProperty MousePositionProperty =
            DependencyProperty.Register("MousePosition", typeof(Point), typeof(RibbonViewModel), new PropertyMetadata(new Point(-1, -1)));

        public Point MousePosition
        {
            get { return (Point)GetValue(MousePositionProperty); }
            set { SetValue(MousePositionProperty, value); }
        }
        //撤销
        //public Stack<IUndoRedoCommand> UndoCommands { get; protected set; }
        public Dictionary<DXTabItem, Stack<IUndoRedoCommand>> DicUndoCommands { get; protected set; }
        //重做
        //public Stack<IUndoRedoCommand> RedoCommands { get; protected set; }
        public Dictionary<DXTabItem, Stack<IUndoRedoCommand>> DicRedoCommands { get; protected set; }
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
                    if (mc != null)
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
                    TabAddItem(barBtnItem);
                });
            }
        }

        /// <summary>
        /// 打开文件，然后添加到页签命令
        /// </summary>
        public System.Windows.Input.ICommand OpenFileToTabCommand
        {
            get
            {
                return new RelayCommand<BarButtonItem>((barBtnItem) =>
                {
                    const string filterString = "文本文件 (*.txt)|*.txt";
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog() { Filter = filterString, Title = "Open file..." };
                    if (dialog.ShowDialog().Value == true)
                    {
                        TabAddItem(barBtnItem, dialog.FileName);
                    }

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
                        this.ClearURCommand();
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
                        FullViewObject = ((IView)newItem.Content).FullScreenObject;
                        ManipulatorBehavior = ((IView)newItem.Content).ManipulatorBehavior;
                        CurrentTabItem = newItem;
                    }

                });
            }
        }

        /// <summary>
        /// 设置各类操作命令
        /// </summary>
        public System.Windows.Input.ICommand SetManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    ManipulatorBase mb = (ManipulatorBase)ManipulatorBehavior;
                    if (mc != null && mb != null)
                    {
                        ManipulatorSetter.SetManipulator(mb, mc.GetLayerControl(0));
                        WellLocationsConnectManipulator wcm = mb as WellLocationsConnectManipulator;//连井操作
                        if (wcm != null)
                            wcm.InitPara();
                    }
                    ClearURCommand();
                });
            }
        }

        public System.Windows.Input.ICommand SetFaultManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    DrawLineManipulator dm = new DrawLineManipulator();
                    ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));
                });
            }
        }

        public System.Windows.Input.ICommand SetCurveManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    DrawCurveManipulator dm = new DrawCurveManipulator();
                    ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));
                });
            }
        }

        public System.Windows.Input.ICommand SetEditManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    EditCurveManipulator dm = new EditCurveManipulator();
                    ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));
                });
            }
        }

        public System.Windows.Input.ICommand SetReplaceManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    ReplaceLineManipulator dm = new ReplaceLineManipulator();
                    ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));
                });
            }
        }

        public System.Windows.Input.ICommand SetEraseManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    EraseLineManipulator dm = new EraseLineManipulator();
                    ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));
                });
            }
        }

        public System.Windows.Input.ICommand SetRemoveFaceManipulatorCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MapControl mc = FullViewObject as MapControl;
                    SetFaceTypeManipulator dm = new SetFaceTypeManipulator() { FaceType = -1 };
                    ManipulatorSetter.SetManipulator(dm, mc.GetLayerControl(0));
                });
            }
        }

        #region 撤销重做命令

        private void RibbonViewModel_OnDrawWellLine(Behavior<UIElement> behavior)//IUndoRedoCommand idcm
        {
            //WellLocationsConnectManipulator command = idcm as WellLocationsConnectManipulator;
            ConnectWellCommand command = new ConnectWellCommand(behavior);
            if (command == null) return;
            if (command is IUndoRedoCommand)
            {
                if (CurrentTabItem == null)
                    return;
                //新增Undo，清空Redo
                if (!DicUndoCommands.ContainsKey(CurrentTabItem))
                {
                    DicUndoCommands.Add(CurrentTabItem, new Stack<IUndoRedoCommand>());
                    DicRedoCommands.Add(CurrentTabItem, new Stack<IUndoRedoCommand>());
                }
                DicUndoCommands[CurrentTabItem].Push(command);
                DicRedoCommands[CurrentTabItem].Clear();
            }
        }
        public System.Windows.Input.ICommand UndoCommand
        {
            get
            {
                return new RelayCommand(UndoCommandExecute, CanUndoCommandExecute);
            }
        }
        protected bool CanUndoCommandExecute()
        {
            if (CurrentTabItem == null|| !DicUndoCommands.Keys.Contains(CurrentTabItem))
                return false;
            return DicUndoCommands[CurrentTabItem].Count > 0;
        }
        protected void UndoCommandExecute()
        {
            if (CurrentTabItem == null|| !DicUndoCommands.Keys.Contains(CurrentTabItem))
                return;
            if (DicUndoCommands[CurrentTabItem].Count > 0)
            {
                IUndoRedoCommand command = DicUndoCommands[CurrentTabItem].Pop();
                command.Undo();
                DicRedoCommands[CurrentTabItem].Push(command);
            }
        }
        public System.Windows.Input.ICommand RedoCommand
        {
            get
            {
                return new RelayCommand(RedoCommandExecute, CanRedoCommandExecute);
            }
        }
        protected bool CanRedoCommandExecute()
        {
            if (CurrentTabItem == null|| !DicRedoCommands.Keys.Contains(CurrentTabItem))
                return false;
            return DicRedoCommands[CurrentTabItem].Count > 0;
        }
        protected void RedoCommandExecute()
        {
            if (CurrentTabItem == null|| !DicRedoCommands.Keys.Contains(CurrentTabItem))
                return;
            if (DicRedoCommands[CurrentTabItem].Count > 0)
            {
                IUndoRedoCommand command = DicRedoCommands[CurrentTabItem].Pop();
                if (command != null)
                {
                    command.Redo();
                    DicUndoCommands[CurrentTabItem].Push(command);
                }
            }
        }

        public void ClearURCommand()
        {
            if (CurrentTabItem == null)
                return;
            if (!DicUndoCommands.Keys.Contains(CurrentTabItem))
                return;
            if (!DicRedoCommands.Keys.Contains(CurrentTabItem))
                return;
            DicUndoCommands[CurrentTabItem].Clear();
            DicRedoCommands[CurrentTabItem].Clear();
        }
        #endregion



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
            CurrentTabItem = tabItem;
        }

        /// <summary>
        /// 打开新页签
        /// </summary>
        /// <param name="barBtnItem"></param>
        private void TabAddItem(BarButtonItem barBtnItem)
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
            CurrentTabItem = tabItem;
        }

        /// <summary>
        /// 打开新页签（带有参数）
        /// </summary>
        /// <param name="barBtnItem"></param>
        private void TabAddItem(BarButtonItem barBtnItem, string parm)
        {
            DXTabItem tabItem = new DXTabItem();
            tabItem.Header = barBtnItem.Content;
            tabItem.AllowHide = DefaultBoolean.True;
            tabItem.IsSelected = true;
            string ucName = barBtnItem.Tag.ToString();

            Assembly curAssembly = Assembly.GetExecutingAssembly();
            object[] parameters = new object[] { parm };
            UserControl uc = (UserControl)curAssembly.CreateInstance(ucName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);
            tabItem.Content = uc;

            foreach (DXTabItem item in TabItems)
            {
                if (item.Header == tabItem.Header)
                    return;
            }
            TabItems.Add(tabItem);
            CurrentTabItem = tabItem;
        }

        #endregion
    }
}

