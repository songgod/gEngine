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
    /// RecentProjectControl.xaml 的交互逻辑
    /// </summary>
    public partial class RecentProjectControl : UserControl
    {
        public RecentProjectControl()
        {
            InitializeComponent();
        }

        public RecentProject Project
        {
            get { return (RecentProject) GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Project.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project", typeof(RecentProject), typeof(RecentProjectControl));
    }
}
