using DevExpress.Xpf.Ribbon;
using gEngine.Project.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Application
{
    public class RibbonPageBase : RibbonPage
    {
        public RibbonPageBase()
        {
            this.DataContext = this;
        }

        public ProjectControl ProjectControl
        {
            get { return (ProjectControl)GetValue(ProjectControlProperty); }
            set { SetValue(ProjectControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectControlProperty =
            DependencyProperty.Register("ProjectControl", typeof(ProjectControl), typeof(RibbonPageBase), new PropertyMetadata(null));
    }
}
