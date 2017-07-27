using gEngine.Utility;
using System.ComponentModel;

namespace gEngine.Graph.Interface
{
    public interface IObject
    {
        string Name { get; set; }

        bool Visible { get; set; }

        bool Editable { get; set; }
        double Opacity { get; set; }

        string DataTemplate { get; set; }
        bool IsSelected { get; set; }

        IObject DeepClone();
    }
    public class IObjects : ObservedCollection<IObject> { }
}
