using gEngine.Utility;

namespace gEngine.Graph.Interface
{
    public interface IMap
    {
        string Name { get; set; }

        ILayers Layers { get; set; }
    }

    public class IMaps : ObservedCollection<IMap> { }
}
