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

namespace gEngineTest.Log
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void LogError_Click(object sender, RoutedEventArgs e)
        {
            gEngine.Util.Log.LogError("this is a error string");
        }

        private void LogWarning_Click(object sender, RoutedEventArgs e)
        {
            gEngine.Util.Log.LogError("this is a waring string");
        }

        private void LogInfo_Click(object sender, RoutedEventArgs e)
        {
            gEngine.Util.Log.LogError("this is a info string");
        }

        private void Crush_Click(object sender, RoutedEventArgs e)
        {
            String s = null;
            s.GetType();
        }
    }
}
