using System;
using System.Collections.Generic;
using System.Globalization;
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
using static gEngine.Graph.Ge.Enums;

namespace gEngine.View.Datatemplate
{
    /// <summary>
    /// WellLocationDataTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationDataTemplate : UserControl
    {
        public WellLocationDataTemplate()
        {
            InitializeComponent();
        }
    }

    public class WellTypeToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WellType wt = (WellType)value;
            switch (wt)
            {
                case WellType.W:
                    return Colors.Blue.ToString();
                case WellType.Y:
                    return Colors.Red.ToString();
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
