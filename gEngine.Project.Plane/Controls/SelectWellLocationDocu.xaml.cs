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
    public partial class SelectWellLocationDocu : UserControl
    {
        public string TxtName
        {
            get; set;
        }
        public SelectWellLocationDocu(List<string> listTxt)
        {
            InitializeComponent();
            this.lbTxts.ItemsSource = listTxt;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (lbTxts.SelectedIndex < 0)
            {
                MessageBox.Show("请选择井数据！");
                return;
            }
            else
            {
                TxtName = lbTxts.SelectedValue.ToString();
            }
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            
            if (parentWindow != null)
            {
                parentWindow.DialogResult = true;
                parentWindow.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            if (parentWindow != null)
            {
                parentWindow.DialogResult = false;
                parentWindow.Close();
            }
        }
    }
}
