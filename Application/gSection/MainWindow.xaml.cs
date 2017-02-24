using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Core;
using DevExpress.Utils;
using GPTDxWPFRibbonApplication1.Controls;
using GPTDxWPFRibbonApplication1.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using gEngine.Util;

namespace GPTDxWPFRibbonApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {

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
        }


        private void btnShowJWT_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            DXTabItem tabItem = new DXTabItem();
            tabItem.Header = e.Item.Content;
            tabItem.AllowHide = DefaultBoolean.True;
            WellLocationControl uc = new WellLocationControl();
            tabItem.Content = uc;

            ItemCollection items = tabControl.Items;
            foreach (DXTabItem item in items)
            {
                if (item.Header == tabItem.Header)
                    return;
            }
            tabControl.Items.Add(tabItem);
        }

    }
}
