using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Symbol
{
    public abstract class PointSymbol : ISymbol
    {
        public static readonly string type = "Point";
        public string Type
        {
            get
            {
                return type;
            }
        }

        public abstract string Name { get; }
        public abstract object Create(PointOptionSetting param);
    }
}
