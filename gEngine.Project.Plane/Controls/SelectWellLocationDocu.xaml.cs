using DevExpress.Xpf.Core;
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

namespace gEngine.Project.Ge.Plane.Controls
{
    /// <summary>
    /// SelectWellLocationDocu.xaml 的交互逻辑
    /// </summary>
    public partial class SelectWellLocationDocu : DXWindow
    {
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(SelectWellLocationDocu));



        public string MapName
        {
            get { return (string)GetValue(MapNameProperty); }
            set { SetValue(MapNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapNameProperty =
            DependencyProperty.Register("MapName", typeof(string), typeof(SelectWellLocationDocu));


        public SelectWellLocationDocu(List<string> listTxt)
        {
            InitializeComponent();
            this.lbTxts.ItemsSource = listTxt;
            this.SetBinding(FileNameProperty, new Binding("SelectedValue") { ElementName = "lbTxts", Mode = BindingMode.OneWay });
            this.SetBinding(MapNameProperty, new Binding("Text") { ElementName = "tbxName", Mode = BindingMode.OneWay });
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MapName))
            {
                MessageBox.Show("请输入图件名！");
                return;
            }

            if (string.IsNullOrEmpty(FileName))
            {
                MessageBox.Show("请选择井数据！");
                return;
            }
            
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
