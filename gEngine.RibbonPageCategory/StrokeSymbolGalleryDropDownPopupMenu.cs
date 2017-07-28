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

namespace gEngine.Application
{
    public class StrokeSymbolGalleryDropDownPopupMenu : GalleryDropDownPopupMenu
    {
        public StrokeSymbolGalleryDropDownPopupMenu()
        {
            Gallery gallery = new Gallery();
            gallery.ColCount = 4;
            gallery.ItemDescriptionHorizontalAlignment = HorizontalAlignment.Left;
            gallery.IsItemCaptionVisible = true;
            gallery.IsItemDescriptionVisible = true;

            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure() { StartPoint = new Point(0, 0) };
            LineSegment ls = new LineSegment { Point = new Point(40, 0) };
            pf.Segments.Add(ls);
            pg.Figures.Add(pf);

            foreach (KeyValuePair<string, ISymbolFactory> kv in gEngine.Symbol.Registry.SymbolFactorys)
            {
                string key = kv.Key;
                GalleryItemGroup group = new GalleryItemGroup();
                group.Caption = key;
                group.IsCaptionVisible = DevExpress.Utils.DefaultBoolean.True;

                foreach (string symbolName in kv.Value.StrokeSymbolNames)
                {
                    LineStyle cplstyle = new LineStyle();
                    cplstyle.Symbol = symbolName;
                    cplstyle.SymbolLib = key;
                    cplstyle.Width = 1;
                    cplstyle.Stroke = Colors.Black;
                    LineOptionSetting setting = LineStyle2LineOptionSettingConverter.ConvertFromLineStyle(cplstyle, pg);

                    object obj = gEngine.Symbol.Registry.CreateStroke(setting);
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

        private static void OnLineSymbolCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StrokeSymbolGalleryDropDownPopupMenu sgdp = (StrokeSymbolGalleryDropDownPopupMenu)d;
            ICommand cmd = (ICommand)e.NewValue;
            foreach (GalleryItemGroup group in sgdp.Gallery.Groups)
            {
                foreach (GalleryItem item in group.Items)
                {
                    item.Command = cmd;
                }
            }
        }

        public ICommand LineSymbolCommand
        {
            get { return (ICommand)GetValue(LineSymbolCommandProperty); }
            set { SetValue(LineSymbolCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineSymbolCommandProperty =
            DependencyProperty.Register("LineSymbolCommand", typeof(ICommand), typeof(StrokeSymbolGalleryDropDownPopupMenu), new PropertyMetadata(null,new PropertyChangedCallback(OnLineSymbolCommandChanged)));
    }
}
