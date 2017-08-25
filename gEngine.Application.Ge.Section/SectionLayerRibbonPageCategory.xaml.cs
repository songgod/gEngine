using DevExpress.Mvvm;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using gEngine.Util.Ge.Section;
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
    /// SandFaceProxyObjectRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class SectionLayerRibbonPageCategory : GeRibbonPageCategory
    {
        public SectionLayerRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Section.SectionLayer();
        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Section.SectionLayer);
            }
        }

        public int VerticalScale
        {
            get
            {
                SectionLayer layer = this.DataContext as SectionLayer;
                SecionLayerSetting set = new SecionLayerSetting(layer);
                return set.getSectionLayerWellVerticalScale();
            }
            set
            {
                SectionLayer layer = this.DataContext as SectionLayer;
                SecionLayerSetting set = new SecionLayerSetting(layer);
                set.setSectionLayerWellVerticalScale(value);
            }
        }
    }
}
