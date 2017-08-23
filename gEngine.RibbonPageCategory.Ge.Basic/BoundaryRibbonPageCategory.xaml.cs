using DevExpress.Mvvm;
using gEngine.Graph.Ge;
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

namespace gEngine.Application.Ge.Basic
{
    /// <summary>
    /// FillRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class BoundaryRibbonPageCategory : GeRibbonPageCategory
    {
        public BoundaryRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Basic.Boundary();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.Boundary);
            }
        }
    }
}
