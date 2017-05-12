using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.Manipulator;
using gEngine.Util.Ge.Plane;
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

namespace gEngineTest.Compass
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Comprass cp = new Comprass() { RotateAngle = 0, ComprassData = "M79.66667,11.333333 L42.33301,144.33367 L79.99967,85.000516 L116.99933,144.66733 z" };
            Comprass cp = new Comprass();
            cp.Top = 0;
            cp.Left = 0;
            cp.Width = 100;
            cp.Height = 180;
            cp.Stroke = new SolidColorBrush(Colors.Black);
            cp.Fill = new SolidColorBrush(Colors.Black);
            cp.StrokeThickness = 1.0;
            cp.RotateAngle = 0;
            cont.Content = cp;

        }
    }
}
