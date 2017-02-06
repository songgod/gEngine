using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.View.Symbol
{
    public interface ISymbol
    {
        string Type { get; }
        string Name { get; }
    }
}
