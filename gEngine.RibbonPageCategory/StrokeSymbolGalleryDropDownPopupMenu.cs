using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using gEngine.Symbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            gallery.ItemAutoHeight = true;
            gallery.ItemDescriptionHorizontalAlignment = HorizontalAlignment.Left;
            gallery.IsItemCaptionVisible = true;
            gallery.IsItemDescriptionVisible = true;

            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure() { StartPoint = new Point(0, 0) };
            LineSegment ls = new LineSegment { Point = new Point(80, 0) };
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
                    LineOptionSetting setting = new LineOptionSetting();
                    setting.Symbol = symbolName;
                    setting.Factory = key;
                    setting.Width = key == "Normal" ? 1 : 10;
                    setting.Stroke = Colors.Black;
                    setting.Path = pg;

                    Path path = gEngine.Symbol.Registry.CreateStroke(setting) as Path;
                    if (path != null)
                    {
                        Canvas c = new Canvas() { Width = 80.0, Height = 40.0 };
                        path.SetValue(Canvas.TopProperty, 20.0);
                        c.Children.Add(path);
                        GalleryItem item = new GalleryItem();
                        item.Caption = c;
                        item.Command = SelectCommand;
                        item.CommandParameter = new string[] { key, symbolName };
                        group.Items.Add(item);
                    }
                }
                gallery.Groups.Add(group);
            }

            Gallery = gallery;
        }



        public string SymbolLib
        {
            get { return (string)GetValue(SymbolLibProperty); }
            set { SetValue(SymbolLibProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SymbolLib.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolLibProperty =
            DependencyProperty.Register("SymbolLib", typeof(string), typeof(StrokeSymbolGalleryDropDownPopupMenu), new PropertyMetadata(""));



        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(StrokeSymbolGalleryDropDownPopupMenu), new PropertyMetadata(""));





        public ICommand SelectCommand
        {
            get
            {
                return new DelegateCommand<string[]>((parameter) =>
                {
                    SymbolLib = parameter[0] as string;
                    Symbol = parameter[1] as string;
                });
            }
        }
    }
}
