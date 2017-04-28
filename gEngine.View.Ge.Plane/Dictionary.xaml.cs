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

namespace gEngine.View.Ge.Plane
{
    /// <summary>
    /// Dictionary.xaml 的交互逻辑
    /// </summary>
    public partial class Dictionary : ResourceDictionary
    {
        public Dictionary()
        {
            InitializeComponent();

            //WellTypeToColorConverter wtlConverter = new WellTypeToColorConverter();
            //Binding binding = new Binding("WellType");
            //binding.Converter = wtlConverter;
            //FrameworkElementFactory fef = new FrameworkElementFactory(typeof(Path));
            //fef.SetBinding(Path.DataProperty, binding);
            

            //FrameworkElementFactory fef = new FrameworkElementFactory(typeof(TextBlock));

            //Binding placeBinding = new Binding();

            //fef.SetBinding(TextBlock.TextProperty, placeBinding);

            //placeBinding.Path = new PropertyPath("Name");

            //DataTemplate dataTemplate = new DataTemplate();

            //dataTemplate.VisualTree = fef;
        }
    }
}
