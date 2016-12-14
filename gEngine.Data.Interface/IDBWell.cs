using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    using DoubleValues = List<double>;
    public interface IDBWell : IDBBase
    {
        DoubleValues Depths { get; set; }
        List<Tuple<string, DoubleValues>> Columns { get; set; }
    }

    public interface IDBWells : IList<IDBWell>, IDBBase
    {

    }
}
