using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    using DoubleValues = List<double>;
    public interface IDBDiscreteData : IDBBase
    {
        string LayerNumber { get; set; }

        string SerialNumber { get; set; }

        double Top_MeasuredDepth { get; set; }

        double MeasuredThickness { get; set; }

        List<string> SDiscreteDatas { get; set; }

        List<double> DDiscreteDatas { get; set; }
    }

    public interface IDBDiscreteDatas : IDBBase
    {
        List<string> ColNames { get; set; }

        List<IDBDiscreteData> DiscreteDatas { get; set; }
    }
}
