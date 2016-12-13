using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Interface
{
    public interface IWell : IObject
    {
        ObsDoubles Depths { get; set; }
        IWellColumns Columns { get; set; }
    }
}
