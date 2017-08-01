using DevExpress.Xpf.Ribbon;
using gEngine.Application;
using gEngine.Graph.Interface;
using gEngine.View;
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

namespace gEngine.Project.Controls
{
    /// <summary>
    /// ProjectControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectControl : UserControl
    {
        public ProjectControl()
        {
            InitializeComponent();
        }



        public Project Project
        {
            get { return (Project)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Project.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project", typeof(Project), typeof(ProjectControl));


        public LayerMgrControl LayerMgrControl
        {
            get
            {
                return layermgr;
            }
        }

        //public ToolsControl ToolsControl
        //{
        //    get
        //    {
        //        return toolcontrol;
        //    }
        //}

        public MapsControl MapsControl
        {
            get
            {
                return tc;
            }
        }

        public void Mpl_OnSelectObject(ObjectControl oc)
        {
            IObject iobject = oc.DataContext as IObject;
            GeRibbonPageCategory grpc = gEngine.Application.Registry.GetRibbonPageCategory(iobject.GetType());
            if (grpc != null)
            {
                BindingExpression exp = grpc.GetBindingExpression(IsVisibleProperty);
                if (exp == null)
                {
                    //绑定1：将iobject的IsSelected属性绑定到ribbon的IsVisibleProperty属性上
                    Binding bd = new Binding("IsSelected");
                    bd.Source = iobject;
                    bd.Mode = BindingMode.OneWay;
                    grpc.SetBinding(RibbonPageCategory.IsVisibleProperty, bd);

                    //绑定2：将iobject绑定到ribbon的datacontext上
                    grpc.DataContext = iobject;
                }
            }
        }
    }
}
