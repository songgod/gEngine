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
using gEngine.Utility;
using gEngine.Graph.Ge.Column;

namespace gEngine.Project.Section.Controls
{
    /// <summary>
    /// Interaction logic for DXSectionSet.xaml
    /// </summary>
    public partial class DXSectionSet : DXWindow
    {
        public DXSectionSet()
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
            DependencyProperty.Register("TplNames", typeof(List<string>), typeof(DXSectionSet));

        public string SelTplName
        {
            get { return (string) GetValue(SelTplNameProperty); }
            set { SetValue(SelTplNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelTplNameProperty =
            DependencyProperty.Register("SelTplName", typeof(string), typeof(DXSectionSet));

        public string MapName
        {
            get { return (string) GetValue(MapNameProperty); }
            set { SetValue(MapNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapNameProperty =
            DependencyProperty.Register("MapName", typeof(string), typeof(DXSectionSet));

        #endregion

        private void BindTemplate()
        {
            TplNames = gEngine.Graph.Tpl.Ge.Registry.GetTemplateNames(typeof(Well));
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.SetBinding(SelTplNameProperty, new Binding("SelectedItemValue") { ElementName = "cbTemplete", Mode = BindingMode.OneWay });
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
