using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Symbol
{
    public class DefaultFillSymbol : FillSymbol
    {
        private static readonly string name = "DefaultPointSymbol";
        public DefaultFillSymbol()
        {
        }

        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override Brush Create(OptionSetting param)
        {
            return new SolidColorBrush(Colors.LightGray);
        }
    }
}