using gEngine.Utility;

namespace gEngine.Graph.Interface
{
    public interface IObject
    {
        string Name { get; set; }

        string DataTemplate { get; set; }
    }
    public class IObjects : ObservedCollection<IObject> { }
}
