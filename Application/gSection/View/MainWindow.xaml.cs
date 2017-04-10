using DevExpress.Xpf.Ribbon;

namespace gSection.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {
        public TabControl TabControl { get { return tc; } }

        public string[] ClipartImages { get; set; }

        public MainWindow()
        {
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

        }
    }
}
