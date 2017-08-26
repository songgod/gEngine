using gEngine.Util;
using gEngine.Utility;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace gEngine.Graph.Interface
{
    public interface IMap
    {
        string Name { get; set; }

        string Type { get; }

        Matrix ViewMatrix { get; set; }

        UndoRedoCommandManager UndoRedoCmdMgr { get; set; }

        string Manipulator { get; set; }

        ILayers Layers { get; set; }
    }

    public class IMaps : ObservedCollection<IMap>
    {
        private int currentindex = -1;
        private IMap currentmap = null;
        public IMaps()
        {
            this.CollectionChanged += IMaps_CollectionChanged;
        }

        private void IMaps_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.CurrentIndex = e.NewStartingIndex;
        }

        public int CurrentIndex
        {
            get { return currentindex; }
            set
            {
                currentindex = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentIndex"));
                if (currentindex >= 0 && currentindex < Count)
                {
                    currentmap = this[currentindex];
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentMap"));
                }
            }
        }

        public IMap CurrentMap
        {
            get
            {
                return currentmap;
            }
            set
            {
                currentmap = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentMap"));
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
