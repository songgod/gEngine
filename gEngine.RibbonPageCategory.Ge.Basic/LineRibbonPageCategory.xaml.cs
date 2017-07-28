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
        }

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Path path = e.OriginalSource as Path;
            //PointSymbol symbol = path.Tag as PointSymbol;
            //WellLocation wl = this.DataContext as WellLocation;
            //wl.PointStyle.Symbol = symbol.Name;

        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.Line);
            }
        }
        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    Graph.Ge.Basic.Line line = this.DataContext as Graph.Ge.Basic.Line;
                    line.LinStyle.SymbolLib = parameter[0] as string;
                    line.LinStyle.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
