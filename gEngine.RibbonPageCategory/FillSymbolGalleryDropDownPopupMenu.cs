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
    public class FillSymbolGalleryDropDownPopupMenu: GalleryDropDownPopupMenu
    {
        public FillSymbolGalleryDropDownPopupMenu()
        {
            Gallery gallery = new Gallery();
            gallery.ColCount = 4;
            gallery.ItemAutoHeight = true;
            gallery.ItemDescriptionHorizontalAlignment = HorizontalAlignment.Left;
            gallery.IsItemCaptionVisible = true;
            gallery.IsItemDescriptionVisible = true;

          
            foreach (KeyValuePair<string, ISymbolFactory> kv in gEngine.Symbol.Registry.SymbolFactorys)
            {
                string key = kv.Key;
                GalleryItemGroup group = new GalleryItemGroup();
                group.Caption = key;
                group.IsCaptionVisible = DevExpress.Utils.DefaultBoolean.True;

                foreach (string symbolName in kv.Value.FillSymbolNames)
                {
                    OptionSetting setting = new OptionSetting();
                    setting.Factory = key;
                    setting.Symbol = symbolName;
                    Brush bru = gEngine.Symbol.Registry.CreateFillBrush(setting);
                    
                    if (bru != null)
                    {
                        Canvas c = new Canvas() { Width = 40.0, Height = 40.0 };
                        Rectangle rect = new Rectangle();
                        rect.Fill = bru;
                        rect.Stroke = Brushes.Black;
                        rect.Width = c.Width;
                        rect.Height = c.Height;
                        c.Children.Add(rect);
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



        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(FillSymbolGalleryDropDownPopupMenu), new PropertyMetadata(""));



        public string SymbolLib
        {
            get { return (string)GetValue(SymbolLibProperty); }
            set { SetValue(SymbolLibProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SymbolLib.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolLibProperty =
            DependencyProperty.Register("SymbolLib", typeof(string), typeof(FillSymbolGalleryDropDownPopupMenu), new PropertyMetadata(""));




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
