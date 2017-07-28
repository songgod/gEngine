using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Symbol.normal
{
    public class SolidFillSymbol : FillSymbol
    {
        public SolidFillSymbol()
        {
        }

        public string SolidSymbolName { get; set; }

        public override string Name
        {
            get
            {
                return SolidSymbolName;
            }
        }

        public Color SolidColor { get; set; }

        public override Brush Create(OptionSetting param)
        {
            return new SolidColorBrush(SolidColor);
        }
    }
}