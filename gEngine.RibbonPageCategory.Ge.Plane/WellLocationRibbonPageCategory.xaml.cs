using gEngine.Graph.Ge.Plane;
using gEngine.RibbonPageCategory;
using gEngine.RibbonPageCategory.Ge;
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

namespace gEngine.RibbonPageCategory.Ge.Plane
{
    /// <summary>
    /// WellLocationRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationRibbonPageCategory : GeRibbonPageCategory
    {
        public WellLocationRibbonPageCategory()
        {
            InitializeComponent();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(WellLocation);
            }
        }
    }
}
