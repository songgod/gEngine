using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using gEngine.Graph.Ge;
using gEngine.Symbol;
using gEngine.View.Ge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.RibbonPageCategory
{
    public class PointSymbolGalleryDropDownPopupMenu : GalleryDropDownPopupMenu
    {
        public PointSymbolGalleryDropDownPopupMenu()
        {
            Gallery gallery = new Gallery();
            gallery.ColCount = 4;
            gallery.ItemDescriptionHorizontalAlignment = HorizontalAlignment.Left;
            gallery.IsItemCaptionVisible = true;
            gallery.IsItemDescriptionVisible = true;

            foreach (KeyValuePair<string, ISymbolFactory> kv in gEngine.Symbol.Registry.SymbolFactorys)
            {
                string key = kv.Key;

                GalleryItemGroup group = new GalleryItemGroup();
                group.Caption = key;
                group.IsCaptionVisible = DevExpress.Utils.DefaultBoolean.True;


                foreach (string symbolName in kv.Value.PointSymbolNames)
                {
                    PointStyle pstyle = new PointStyle();
                    pstyle.Height = 20;
                    pstyle.Width = 20;
                    pstyle.Symbol = symbolName;
                    pstyle.SymbolLib = key;
                    pstyle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEECE1"));
                    pstyle.Stroke = Colors.Black;
                    OptionSetting setting = PointStyle2OptionSettingConverter.CreateFromPointStyle(pstyle);

                    object obj = gEngine.Symbol.Registry.CreatePoint(setting);

                    if (obj != null)
                    {
                        GalleryItem item = new GalleryItem();
                        item.Caption = obj;
                        item.Command = null;
                        item.CommandParameter = new string[] { key, symbolName };
                        group.Items.Add(item);
                    }
                }

                gallery.Groups.Add(group);
            }


            Gallery = gallery;
        }

        private static void OnPointSymbolCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PointSymbolGalleryDropDownPopupMenu pgdp = (PointSymbolGalleryDropDownPopupMenu)d;
            ICommand cmd = (ICommand)e.NewValue;
            foreach (GalleryItemGroup group in pgdp.Gallery.Groups)
            {
                foreach (GalleryItem item in group.Items)
                {
                    item.Command = cmd;
                }
            }
        }

        public ICommand PointSymbolCommand
        {
            get { return (ICommand)GetValue(PointSymbolCommandProperty); }
            set { SetValue(PointSymbolCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointSymbolCommandProperty =
            DependencyProperty.Register("PointSymbolCommand", typeof(ICommand), typeof(PointSymbolGalleryDropDownPopupMenu), new PropertyMetadata(null, new PropertyChangedCallback(OnPointSymbolCommandChanged)));
    }
}
