

using System;
using gEngine.Utility;
using System.ComponentModel;

namespace gEngine.Graph.Interface
{
    public interface ILayer
    {
        string Name { get; set; }

        string Type { get; }

        bool Visible { get; set; }

        bool Editable { get; set; }

        double Opacity { get; set; }

        string Manipulator { get; set; }

        IObjects Objects { get; set; }
    }

    public class ILayers : ObservedCollection<ILayer>
    {
        private int currentindex=-1;
        private ILayer currentlayer = null;

        public int CurrentIndex
        {
            get { return currentindex; }
            set
            {
                currentindex = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentIndex"));
                if (currentindex >= 0 && currentindex < Count)
                {
                    currentlayer = this[currentindex];
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentLayer"));
                }
            }
        }

        public ILayer CurrentLayer
        {
            get
            {
                return currentlayer;
            }
            set
            {
                currentlayer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentLayer"));
                for (int i = 0; i < Count; i++)
                {
                    if (this[i] == value)
                    {
                        currentindex = i;
                        OnPropertyChanged(new PropertyChangedEventArgs("CurrentIndex"));
                        break;
                    }
                }
            }
        }
    }
}
