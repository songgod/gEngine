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
    /// FaultLineProxyObjectRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class FaultLineProxyObjectRibbonPageCategory : GeRibbonPageCategory
    {
        public FaultLineProxyObjectRibbonPageCategory()
        {
            InitializeComponent();
        }

        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Section.FaultLineProxyObject);
            }
        }

        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    Graph.Ge.Section.FaultLineProxyObject line = this.DataContext as Graph.Ge.Section.FaultLineProxyObject;
                    line.LineStyle.SymbolLib = parameter[0] as string;
                    line.LineStyle.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
