using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    using DoubleValues = List<double>;
    public interface IDBHorizon : IDBBase
    {
        string LayerNumber { get; set; }

        double Top_MeasuredDepth { get; set; }

        double MeasuredThickness { get; set; }

        List<string> SHorizonDatas { get; set; }

        List<double> DHorizonDatas { get; set; }
    }

    public interface IDBHorizons : IDBBase
    {
        List<string> ColNames { get; set; }

        List<IDBHorizon> Horizons { get; set; }
    }
}
