using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Symbol
{
    public abstract class LineSymbol : ISymbol
    {
        public static readonly string type = "Line";
        public string Type
        {
            get
            {
                return type;
            }
        }

        public abstract string Name { get; }
        public abstract object Create(OptionSetting param);
    }
}
