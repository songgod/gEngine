using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Symbol.normal
{
    public class GradientFillSymbol: FillSymbol
    {
        public GradientFillSymbol()
        {
        }

        public string GradientSymbolName { get; set; }

        public override string Name
        {
            get
            {
                return GradientSymbolName;
            }
        }

        public GradientStopCollection GradientColor { get; set; }

        public override Brush Create(OptionSetting param)
        {
            //GradientBrush gb = new GradientBrush();
            //SolidColorBrush sc = new SolidColorBrush();
            LinearGradientBrush lg = new LinearGradientBrush(GradientColor);
            return lg;// SolidColorBrush(GradientColor);
        }
    }
}
