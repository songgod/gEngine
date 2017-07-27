using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.RibbonPageCategory;
using gEngine.Symbol;
using gEngine.View.Ge;
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

namespace gEngine.RibbonPageCategory.Ge.Basic
{
    /// <summary>
    /// WellLineRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class WellLineRibbonPageCategory : GeRibbonPageCategory
    {
        public WellLineRibbonPageCategory()
        {
            InitializeComponent();
            InitLineStyle();
        }
        private void InitLineStyle()
        {
            GalleryDropDownPopupMenu menu = new GalleryDropDownPopupMenu();
            Gallery gallery = new Gallery();
            gallery.ColCount = 1;
            gallery.ItemDescriptionHorizontalAlignment = HorizontalAlignment.Left;
            gallery.IsItemCaptionVisible = true;
            gallery.IsItemDescriptionVisible = true;

            GalleryItemGroup group = new GalleryItemGroup();
            group.Caption = "简单线";
            group.IsCaptionVisible = DevExpress.Utils.DefaultBoolean.True;

            ComplexLineStyle pstyle = new ComplexLineStyle();
            
            pstyle.Width = 20;
            
            pstyle.Stroke = new SolidColorBrush(Colors.Black);
            OptionSetting setting = LineStyle2OptionSettingConverter.CreateFromLineStyle(pstyle);

            foreach (KeyValuePair<string, ISymbolFactory> kv in gEngine.Symbol.Registry.SymbolFactorys)
            {
                string key = kv.Key;
                

                foreach (string symbolName in kv.Value.LineSymbolNames)
                {
                    LineSymbol symbol = kv.Value.GetLineSymbol(symbolName);

                    Path path = symbol.Create(setting) as Path;

                    if (path != null)
                    {
                        GalleryItem item = new GalleryItem();
                        item.Caption = path;
                        item.Command = SelectBarCommand;
                        item.CommandParameter = new string[] { key, symbolName };
                        group.Items.Add(item);
                    }
                }
            }

            gallery.Groups.Add(group);
            menu.Gallery = gallery;
            bsbLineStyle.PopupControl = menu;
        }

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Path path = e.OriginalSource as Path;
            //PointSymbol symbol = path.Tag as PointSymbol;
            //WellLocation wl = this.DataContext as WellLocation;
            //wl.PointStyle.Symbol = symbol.Name;

        }
        public override Type SupportType
        {
            get
            {
                return typeof(Graph.Ge.Basic.Line);
            }
        }
        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    ComplexLineStyle wl = this.DataContext as ComplexLineStyle;
                    wl.SymbolLib = parameter[0] as string;
                    wl.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
