using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Ribbon;
using gEngine.Graph.Interface;
using gEngine.Project;
using gEngine.Project.Commands;
using gEngine.Project.Controls;
using gEngine.RibbonPageCategory;
using gEngine.View;
using gSection.CommandBindings;
using gSection.Commands;
using gSection.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace gSection.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {
        public string[] ClipartImages { get; set; }

        public Project Projects { get; set; }

        public ProjectControl ProjectControl
        {
            get
            {
                return prjctrl;
            }
        }

        public MainWindow()
        {
            gEngine.Project.Registry.InstallCommands(this);
            RibbonCommandInstaller install = new RibbonCommandInstaller();
            install.Install(this);

            Projects = new Project();

            InitializeComponent();

            ClipartImages = new string[] {
                 "/RibbonDemo;component/Images/Clipart/caCompClient.png",
                 "/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caDatabaseBlue.png",
                 "/RibbonDemo;component/Images/Clipart/caDataBaseDisabled.png",
                 "/RibbonDemo;component/Images/Clipart/caDataBaseGreen.png",
                 "/RibbonDemo;component/Images/Clipart/caDataBaseViolet.png",
                 "/RibbonDemo;component/Images/Clipart/caInet.png",
                 "/RibbonDemo;component/Images/Clipart/caInetSearch.png",
                 "/RibbonDemo;component/Images/Clipart/caModem.png",
                 "/RibbonDemo;component/Images/Clipart/caModemEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caNetCard.png",
                 "/RibbonDemo;component/Images/Clipart/caNetwork.png",
                 "/RibbonDemo;component/Images/Clipart/caNetworkEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caServer.png",
                 "/RibbonDemo;component/Images/Clipart/caServerEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caWebCam.png"
             };
            this.DataContext = this;


            gEngine.RibbonPageCategory.Registry.AddRibbonPageCategory(ribbonControl);
        }

        public void Mpl_OnSelectObject(ObjectControl oc)
        {
            IObject iobject = oc.DataContext as IObject; 
            GeRibbonPageCategory grpc = gEngine.RibbonPageCategory.Registry.GetRibbonPageCategory(iobject.GetType());
            if (grpc != null)
            {
                BindingExpression exp = grpc.GetBindingExpression(IsVisibleProperty);
                if (exp == null)
                {
                    //绑定1：将iobject的IsSelected属性绑定到ribbon的IsVisibleProperty属性上
                    Binding bd = new Binding("IsSelected");
                    bd.Source = iobject;
                    bd.Mode = BindingMode.OneWay;
                    grpc.SetBinding(RibbonPageCategory.IsVisibleProperty, bd);

                    //绑定2：将iobject绑定到ribbon的datacontext上
                    Binding bd2 = new Binding();
                    bd2.Source = iobject;
                    bd2.Mode = BindingMode.OneWay;
                    bd2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    grpc.SetBinding(RibbonPageCategory.DataContextProperty, bd2);
                }
            }
        }

        #region Command

        public ICommand CloseApplicationMenuCommand
        {
            get
            {
                return new DelegateCommand<GridControl>((gcMap) =>
                {
                    int[] selectedRowHandles = gcMap.GetSelectedRowHandles();
                    if (selectedRowHandles.Length == 0)
                    {
                        if (Projects.Maps.Count > 0)
                        {
                            gcMap.SelectionMode = MultiSelectMode.None;
                            this.BackstageViewControl_1.Close();
                            return;
                        }
                    }
                    foreach (int i in selectedRowHandles)
                    {
                        string MapName = gcMap.GetCellValue(i, "Item1").ToString();
                        var query = from q in Projects.Maps where q.Item1 == MapName select q;
                        if (query.ToList().Count >= 0)
                        {
                            if (query.ElementAt(0).Item2 != null)
                            {
                                this.BackstageViewControl_1.Close();
                                break;
                            }
                        }
                    }
                });
            }
        }

        public ICommand SetOpenMapTabIndexCommand
        {
            get
            {
                return new DelegateCommand<string>((ProjectUrl) =>
                {
                    if (Projects.Url != null)
                    {
                        if (Projects.Url.Equals(ProjectUrl))
                            BackstageViewControl_1.SelectedTabIndex = 1;
                    }
                });
            }
        }

        public ICommand SetMultiSelectMapCommand
        {
            get
            {
                return new DelegateCommand<GridControl>((gcMap) =>
                {
                    if (gcMap != null)
                    {
                        gcMap.SelectionMode = MultiSelectMode.MultipleRow;
                    }
                });
            }
        }

        #endregion
    }
}
