using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace gEngine.Symbol.gesym
{
    public class GeFillSymbol : FillSymbol
    {
        private static readonly string name = "GeFillSymbol";

        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override Brush Create(OptionSetting param)
        {
            if (param==null)
                return null;


            Color clr = param.GetValue<Color>("Color");
            if (clr == null)
                clr = Colors.Transparent;

            return new SolidColorBrush() { Color = clr };
        }
    }
}
