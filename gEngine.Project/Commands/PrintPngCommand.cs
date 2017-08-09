using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace gEngine.Project.Commands
{
    class PrintPngCommand : CommandBinding
    {
        public PrintPngCommand()
        {
            Command = BasicCommands.PrintPngCommand;
            Executed += PrintPngCommand_Executed;
            CanExecute += PrintPngCommand_CanExecute;
        }

        private void PrintPngCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            if (pc.MapsControl.ActiveMapControl == null)
                return;

            MapControl mc = pc.MapsControl.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl layer = mc.ActiveLayerControl;
            if (layer == null)
                return;

            // 这里需要判断一下是不是装饰图层

            e.CanExecute = true;
            e.Handled = true;
        }

        private void PrintPngCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            if (pc.MapsControl.ActiveMapControl == null)
                return;

            MapControl mc = pc.MapsControl.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;
            ExportToPng(mc);
            //DrawRectObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawRectObjectManipulator") as DrawRectObjectManipulator;
            //if (dm == null)
            //    return;
            //ManipulatorSetter.SetManipulator(dm, lc);
        }
        public void ExportToPng(MapControl surface)
        {
            //if (path == null) return;

            Transform transform = surface.LayoutTransform;
            surface.LayoutTransform = null;

            Size size = new Size(surface.ActualWidth, surface.ActualHeight);
            surface.Measure(size);
            surface.Arrange(new System.Windows.Rect(size));

            RenderTargetBitmap renderBitmap =
            new RenderTargetBitmap(
            (int)size.Width,
            (int)size.Height,
            96d,
            96d,
            PixelFormats.Pbgra32);
            renderBitmap.Render(surface);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = DateTime.Now.ToString("yyyyMMdd") + ".png"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Text documents (.png)|*.png"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Save document
                using (FileStream outStream = new FileStream(dlg.FileName, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(outStream);
                }
            }

            surface.LayoutTransform = transform;
        }
    }
}
