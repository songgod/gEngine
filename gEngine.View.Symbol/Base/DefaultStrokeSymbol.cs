using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Symbol
{
    public class DefaultStrokeSymbol : StrokeSymbol
    {
        public DefaultStrokeSymbol()
        {

        }

        private static readonly string name = "DefaultPointSymbol";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override PathGeometry SymbolGeometry
        {
            get
            {
                return null;
            }
        }
    }
}