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
    /// SandLineProxyObjectRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class SandLineProxyObjectRibbonPageCategory : GeRibbonPageCategory
    {
        public SandLineProxyObjectRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Section.LineProxyObject();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Section.SandLineProxyObject);
            }
        }
    }
}
