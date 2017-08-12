using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Interface
{
    public interface ILayerCreator
    {
        string MapType { get; }
        List<string> LayerTypes { get; }
        ILayer CreateLayer(string type);
    }
}
