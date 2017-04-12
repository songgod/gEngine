using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    public interface IDBWellLayerData : IDBBase
    {
        List<string> WellNames { get; set; }

        List<Tuple<string, List<string>>> WellLayerDatas { get; set; }
    }

    public interface IDBWellLayerDatas : IList<IDBWellLayerData>, IDBBase
    {

    }
}
