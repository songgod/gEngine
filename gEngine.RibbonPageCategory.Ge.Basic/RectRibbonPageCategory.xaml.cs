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
    /// RectRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class RectRibbonPageCategory : GeRibbonPageCategory
    {
        public RectRibbonPageCategory()
        {
            InitializeComponent();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.Rect);
            }
        }
        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    Graph.Ge.Basic.Rect boun = this.DataContext as Graph.Ge.Basic.Rect;
                    boun.FillStyle.SymbolLib = parameter[0] as string;
                    boun.FillStyle.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
