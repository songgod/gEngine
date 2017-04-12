using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    public interface IDBWellLayer : IDBBase
    {
        string BoundaryName
        {
            get;
            set;
        }

        double TopDepth
        {
            get;
            set;
        }

        double Thickness
        {
            get;
            set;
        }
    }

    public interface IDBWellLayers : IList<IDBWellLayer>, IDBBase
    {

    }
}
