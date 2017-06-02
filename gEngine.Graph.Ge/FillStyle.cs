using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    public class FillStyle
    {
        public enum FillType
        {
            SolidBrush = 0,
            GradientBrush,
            ImageBrush,
            SymbolBrush,
            UnKownBrush
        }

        public virtual FillType BrushType
        {
            get
            {
                return FillType.UnKownBrush;
            }
        }
    }

    public class SymbolFillStyle : FillStyle
    {
        public override FillType BrushType
        {
            get
            {
                return FillType.SymbolBrush;
            }
        }

        public string Symbol { get; set; }
        public string SymbolLib { get; set; }
        public Color Color { get; set; }
    }

    public class SolidFillStyle : FillStyle
    {
        public override FillType BrushType
        {
            get
            {
                return FillType.SolidBrush;
            }
        }
        public Color Color { get; set; }
    }

    public class GradientFillStyle : FillStyle
    {
        public override FillType BrushType
        {
            get
            {
                return FillType.GradientBrush;
            }
        }
        public Point Start { get; set; }
        public Point End { get; set; }
        public GradientStopCollection Stops { get; set; }
    }

    public class ImageFillStyle : FillStyle
    {
        public override FillType BrushType
        {
            get
            {
                return FillType.ImageBrush;
            }
        }
        public string Image { get; set; }
    }
}
