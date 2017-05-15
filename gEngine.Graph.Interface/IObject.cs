using gEngine.Utility;
using System.ComponentModel;

namespace gEngine.Graph.Interface
{
    public interface IObject
    {
        string Name { get; set; }

        bool Visible { get; set; }

        bool Editable { get; set; }

        string DataTemplate { get; set; }
    }
    public class IObjects : ObservedCollection<IObject> { }
}
