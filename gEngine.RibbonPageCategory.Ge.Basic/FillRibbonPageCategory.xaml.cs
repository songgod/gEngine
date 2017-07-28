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
    public partial class FillRibbonPageCategory : GeRibbonPageCategory
    {
        public FillRibbonPageCategory()
        {
            InitializeComponent();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.Boundary);
            }
        }
        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    //Graph.Ge.Basic.Line line = this.DataContext as Graph.Ge.Basic.Line;
                    //if (parameter[0] == "Solid")
                    //{
                    //    NormalLineStyle nls = new NormalLineStyle() { Stroke = line.LinStyle.Stroke, Width = line.LinStyle.Width };
                    //    line.LinStyle = nls;
                    //}
                    //else if (parameter[0] == "Dot")
                    //{
                    //    NormalLineStyle nls = new NormalLineStyle() { Stroke = line.LinStyle.Stroke, Width = line.LinStyle.Width };
                    //    nls.StrokeDashArray = DoubleCollection.Parse(parameter[1]);
                    //    line.LinStyle = nls;
                    //}
                    //else
                    //{
                    //    ComplexLineStyle cpls = new ComplexLineStyle() { Stroke = line.LinStyle.Stroke, Width = line.LinStyle.Width };
                    //    cpls.SymbolLib = parameter[0] as string;
                    //    cpls.Symbol = parameter[1] as string;
                    //    line.LinStyle = cpls;
                    //}

                });
            }
        }
    }
}
