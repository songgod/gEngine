using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Plane;
using gEngine.RibbonPageCategory;
using gEngine.RibbonPageCategory.Ge;
using gEngine.Symbol;
using gEngine.Symbol.gesym;
using gEngine.View;
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
using System.Windows.Interactivity;
using DevExpress.Xpf.Ribbon;

namespace gEngine.RibbonPageCategory.Ge.Plane
{
    /// <summary>
    /// WellLocationRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationRibbonPageCategory : GeRibbonPageCategory
    {
        public WellLocationRibbonPageCategory()
        {
            InitializeComponent();

            InitPointStyle();
        }

        private void InitPointStyle()
        {
            GalleryDropDownPopupMenu menu = new GalleryDropDownPopupMenu();
            Gallery gallery = new Gallery();
            gallery.ColCount = 4;
            gallery.ItemDescriptionHorizontalAlignment = HorizontalAlignment.Left;
            gallery.IsItemCaptionVisible = true;
            gallery.IsItemDescriptionVisible = true;

            GalleryItemGroup group = new GalleryItemGroup();
            group.Caption = "点符号设置";
            group.IsCaptionVisible = DevExpress.Utils.DefaultBoolean.True;

            PointStyle pstyle = new PointStyle();
            pstyle.Height = 20;
            pstyle.Width = 20;
            pstyle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEECE1"));
            pstyle.Stroke = new SolidColorBrush(Colors.Black);
            OptionSetting setting = PointStyle2OptionSettingConverter.CreateFromPointStyle(pstyle);

            foreach (KeyValuePair<string, ISymbolFactory> kv in gEngine.Symbol.Registry.SymbolFactorys)
            {
                string key = kv.Key;
                PointSymbols pss = kv.Value.PointSymbols;

                foreach (PointSymbol symbol in pss.Values)
                {
                    Path path = symbol.Create(setting) as Path;

                    if (path != null)
                    {
                        GalleryItem item = new GalleryItem();
                        item.Caption = path;
                        item.Command = SelectBarCommand;
                        item.CommandParameter = new string[] { key, symbol.Name };
                        group.Items.Add(item);
                    }
                }
            }

            gallery.Groups.Add(group);
            menu.Gallery = gallery;
            bsbPointStyle.PopupControl = menu;
        }

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path path = e.OriginalSource as Path;
            PointSymbol symbol = path.Tag as PointSymbol;
            WellLocation wl = this.DataContext as WellLocation;
            wl.PointStyle.Symbol = symbol.Name;

        }

        public override Type SupportType
        {
            get
            {
                return typeof(WellLocation);
            }
        }

        public ICommand SelectBarCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    WellLocation wl = this.DataContext as WellLocation;
                    wl.PointStyle.SymbolLib = parameter[0] as string;
                    wl.PointStyle.Symbol = parameter[1] as string;
                });
            }
        }
    }
}
