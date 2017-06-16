using DevExpress.Xpf.Ribbon;
using gEngine.Graph.Interface;
using gEngine.Project;
using gEngine.Project.Commands;
using gEngine.Project.Controls;
using gEngine.RibbonPageCategory;
using gSection.CommandBindings;
using gSection.Commands;
using gSection.Converters;
using System;
using System.Windows;
using System.Windows.Data;

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
            Projects.OpenDBSource(@"D:\gSectionData.Txt");



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

        public void Mpl_OnSelectObject(IObject iobject)
        {
            GeRibbonPageCategory grpc = gEngine.RibbonPageCategory.Registry.GetRibbonPageCategory(iobject.GetType());
            RibbonPageCategory rpc = GetRibPageCategory(grpc);
            if(rpc!=null)
            {
                BindingExpression exp = rpc.GetBindingExpression(IsVisibleProperty);
                if(exp==null)
                {
                    Binding bd = new Binding("IsSelected");
                    bd.Source = iobject;
                    bd.Mode = BindingMode.OneWay;
                    rpc.SetBinding(RibbonPageCategory.IsVisibleProperty, bd);
                }
            }
        }

        private RibbonPageCategory GetRibPageCategory(GeRibbonPageCategory grpc)
        {
            foreach (var rpc in this.ribbonControl.Categories)
            {
                if (rpc is RibbonPageCategory)
                {
                    if (rpc == grpc)
                    {
                        return rpc as RibbonPageCategory;
                    }
                }
            }
            return null;
        }
    }
}
