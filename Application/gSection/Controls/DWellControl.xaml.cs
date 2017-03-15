using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using gEngine.View;
using System.Windows.Interactivity;
using GPTDxWPFRibbonApplication1.ViewModels;
using System.IO;
using System.Text;
using gEngine.Graph.Interface;
using gEngine.Graph.Ge.Column;
using System;
using System.Windows.Media;

namespace GPTDxWPFRibbonApplication1.Controls
{
    /// <summary>
    /// DWellControl.xaml 的交互逻辑
    /// </summary>
    public partial class DWellControl : UserControl, IView
    {
        #region IView接口实现

        FrameworkElement IView.FullScreenObject
        {
            get { return mc; }
            set { mc = (MapControl)value; }
        }

        Behavior<UIElement> IView.ManipulatorBehavior
        {
            get; set;
        }

        #endregion

        public DWellControl()
        {
            InitializeComponent();
            // 将View中控件传到ViewModel，目前有2种方法，第一种方法是通过属性赋值来实现，如下注释
            // 第二种方式是在View的控件中（mc）增加命令，由命令来将mc控件，传到ViewModel中，目前采用第二种方式
            //WellViewModel _vm = new WellViewModel() { mc = this.mc };
        }
    }
}
