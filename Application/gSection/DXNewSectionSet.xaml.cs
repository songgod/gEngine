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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;


namespace GPTDxWPFRibbonApplication1
{
    /// <summary>
    /// Interaction logic for DXNewSectionSet.xaml
    /// </summary>
    public partial class DXNewSectionSet : DXWindow
    {
        public DXNewSectionSet()
        {
            InitializeComponent();
        }

        public DXNewSectionSet(string wellNums)
        {
            InitializeComponent();
            WellNums = wellNums;
        }

        #region Property

        public static string WellNums;

        #endregion
    }
}
