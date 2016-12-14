using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    public interface IDBWellLocation : IDBBase
    {
        string WellType { get; set; }
        int WellCategory { get; set; }
        double x { get; set; }
        double y { get; set; }
    }

    public interface IDBWellLocations : IList<IDBWellLocation>, IDBBase
    {

    }
}
