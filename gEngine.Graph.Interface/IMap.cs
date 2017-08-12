using gEngine.Utility;
using System.Collections.Generic;
using System.ComponentModel;

namespace gEngine.Graph.Interface
{
    public interface IMap
    {
        string Name { get; set; }

        string Type { get; }

        ILayers Layers { get; set; }
    }

    public class IMaps : ObservedCollection<IMap>
    {
        private int currentindex = -1;

        public int CurrentIndex
        {
            get { return currentindex; }
            set
            {
                currentindex = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentIndex"));
            }
        }
    }
}
