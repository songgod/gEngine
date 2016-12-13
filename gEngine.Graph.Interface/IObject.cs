using gEngine.Utility;

namespace gEngine.Graph.Interface
{
    public interface IObject
    {
        string Name { get; set; }
    }
    public class IObjects : ObservedCollection<IObject> { }
}
