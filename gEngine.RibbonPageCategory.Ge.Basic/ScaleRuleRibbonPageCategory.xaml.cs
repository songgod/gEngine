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

namespace gEngine.Application.Ge.Basic
{
    /// <summary>
    /// ScaleRuleRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class ScaleRuleRibbonPageCategory : GeRibbonPageCategory
    {
        public ScaleRuleRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Basic.ScaleRule();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.ScaleRule);
            }
        }
        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    //Graph.Ge.Basic.ScaleRule boun = this.DataContext as Graph.Ge.Basic.ScaleRule;
                    //boun.FillStyle.SymbolLib = parameter[0] as string;
                    //boun.FillStyle.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
