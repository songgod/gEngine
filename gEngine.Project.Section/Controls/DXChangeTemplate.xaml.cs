using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using gEngine.Graph.Ge.Column;

namespace gEngine.Project.Section.Controls
{
    /// <summary>
    /// Interaction logic for DXChangeTemplate.xaml
    /// </summary>
    public partial class DXChangeTemplate : DXWindow
    {
        public DXChangeTemplate()
        {
            InitializeComponent();
            BindTemplate();
            this.DataContext = this;
        }

        #region Property

        public List<string> TplNames
        {
            get { return (List<string>) GetValue(TplNamesProperty); }
            set { SetValue(TplNamesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TplNamesProperty =
            DependencyProperty.Register("TplNames", typeof(List<string>), typeof(DXChangeTemplate));

        public string SelTplName
        {
            get;
            private set;
        }

        #endregion

        #region Method

        private void BindTemplate()
        {
            TplNames = gEngine.Graph.Tpl.Ge.Registry.GetTemplateNames(typeof(Well));
        }

        #endregion

        #region Event

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.SelTplName = this.cbTemplate.SelectedItemValue.ToString();
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion
    }
}
