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
                    FillStyle cplstyle = new FillStyle();
                    cplstyle.Symbol = symbolName;
                    cplstyle.SymbolLib = key;
                    //cplstyle.Width = key == "Normal" ? 1 : 10;
                    //cplstyle.Stroke = Colors.Black;
                    //Brush setting = FillStyle2BrushConverter.ConverterFromFillStyle(cplstyle);

                    Brush bru = FillStyle2BrushConverter.ConverterFromFillStyle(cplstyle);
                    if (bru != null)
                    {
                        Canvas c = new Canvas() { Width = 40.0, Height = 40.0 };
                        //Border border = new Border();
                        //border.BorderBrush = new SolidColorBrush(Colors.Black);
                        //border.BorderThickness = new Thickness(1);
                       
                        //border.Child = c;
                        //path.SetValue(Canvas.TopProperty, 20.0);
                        Rectangle rect = new Rectangle();
                        //LinearGradientBrush lin = new LinearGradientBrush() { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };
                        //lin.GradientStops.Add(new GradientStop() { Offset = 0, Color = Colors.Red });
                        //lin.GradientStops.Add(new GradientStop() { Offset = 0.25, Color = Colors.Blue });
                        //lin.GradientStops.Add(new GradientStop() { Offset = 0.6, Color = Colors.DarkRed });
                        //lin.GradientStops.Add(new GradientStop() { Offset = 1, Color = Colors.Aqua });
                        rect.Fill = bru;
                        rect.Stroke = Brushes.Black;
                        rect.Width = c.Width;
                        rect.Height = c.Height;
                        c.Children.Add(rect);
                        GalleryItem item = new GalleryItem();
                        item.Caption = c;
                        item.Command = null;
                        item.CommandParameter = new string[] { key, symbolName };
                        group.Items.Add(item);
                    }
                }
                gallery.Groups.Add(group);
            }

            Gallery = gallery;
        }

        private static void OnFillSymbolCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FillSymbolGalleryDropDownPopupMenu sgdp = (FillSymbolGalleryDropDownPopupMenu)d;
            ICommand cmd = (ICommand)e.NewValue;
            foreach (GalleryItemGroup group in sgdp.Gallery.Groups)
            {
                foreach (GalleryItem item in group.Items)
                {
                    item.Command = cmd;
                }
            }
        }

        public ICommand FillSymbolCommand
        {
            get { return (ICommand)GetValue(FillSymbolCommandProperty); }
            set { SetValue(FillSymbolCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillSymbolCommandProperty =
            DependencyProperty.Register("FillSymbolCommand", typeof(ICommand), typeof(FillSymbolGalleryDropDownPopupMenu), new PropertyMetadata(null, new PropertyChangedCallback(OnFillSymbolCommandChanged)));
    }
}
