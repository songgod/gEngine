using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using gEngine.Application;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.Symbol;
using gEngine.View.Ge;
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
    /// WellLineRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class LineRibbonPageCategory : GeRibbonPageCategory
    {
        public LineRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Basic.Line();
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
