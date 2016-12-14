using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gEngine.Graph.Interface.Enums;

namespace gEngine.Graph.Interface
{
    public interface IWellColumn : IObject
    {
        IWell Owner { get; set; }
        ObsDoubles Values { get; set; }
        MathType MathType { get; set; }
    }

    public class IWellColumns : ObservedCollection<IWellColumn> { }
}
