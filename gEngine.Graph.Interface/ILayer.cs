

using gEngine.Utility;

namespace gEngine.Graph.Interface
{
    public interface ILayer
    {
        string Name { get; set; }

        IObjects Objects { get; set; }
    }

    public class ILayers : ObservedCollection<ILayer> { }
}
