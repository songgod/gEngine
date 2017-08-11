using DevExpress.Mvvm;
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

namespace gEngine.Application.Ge.Section
{
    /// <summary>
    /// TrendLineRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class TrendLineRibbonPageCategory : GeRibbonPageCategory
    {
        public TrendLineRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Section.TrendLine();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Section.TrendLine);
            }
        }

        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    Graph.Ge.Section.TrendLine line = this.DataContext as Graph.Ge.Section.TrendLine;
                    line.LineStyle.SymbolLib = parameter[0] as string;
                    line.LineStyle.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
