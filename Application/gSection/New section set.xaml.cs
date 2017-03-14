
using GPTDxWPFRibbonApplication1.Controls;
using GPTDxWPFRibbonApplication1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GPTDxWPFRibbonApplication1
{
    /// <summary>
    /// New_section_set.xaml 的交互逻辑
    /// </summary>
    public partial class New_section_set : Window
    {
        public New_section_set()
        {
            InitializeComponent();
        }

        public New_section_set(string wellNums)
        {
            InitializeComponent();
            //Vm = new DWellControl();
            //Vm.WellNums = wellNums;

            WellNums = wellNums;
        }

        #region Property

        public static string WellNums;

        //public DWellControl Vm
        //{
        //    get;
        //    set;
        //}

        #endregion

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
