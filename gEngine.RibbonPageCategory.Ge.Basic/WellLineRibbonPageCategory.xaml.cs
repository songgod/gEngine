using gEngine.Graph.Ge.Basic;
using gEngine.RibbonPageCategory;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gEngine.RibbonPageCategory.Ge.Basic
{
    /// <summary>
    /// WellLineRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class WellLineRibbonPageCategory : GeRibbonPageCategory
    {
        public WellLineRibbonPageCategory()
        {
            InitializeComponent();
        }

        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.Line);
            }
        }
    }
}
