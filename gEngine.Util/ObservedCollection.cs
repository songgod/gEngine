using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace gEngine.Utility
{
    public class ObservedCollection<T> : ObservableCollection<T>
    {
        public ObservedCollection()
        {
            this.CollectionChanged += ObservedCollection_CollectionChanged;
        }

        public ObservedCollection(List<T> list) : base(list)
        {
            this.CollectionChanged += ObservedCollection_CollectionChanged;
        }

        public ObservedCollection(IEnumerable<T> collection) : base(collection)
        {
            this.CollectionChanged += ObservedCollection_CollectionChanged;
        }

        public delegate void ObservedCollectionAddDelegate(int aIndex, T aItem);
        public event ObservedCollectionAddDelegate OnItemAdded;

        public delegate void ObservedCollectionMoveDelegate(int aOldIndex, int aNewIndex, T aItem);
        public event ObservedCollectionMoveDelegate OnItemMoved;

        public delegate void ObservedCollectionRemoveDelegate(int aIndex, T aItem);
        public event ObservedCollectionRemoveDelegate OnItemRemoved;

        public delegate void ObservedCollectionReplaceDelegate(int aIndex, T aOldItem, T aNewItem);
        public event ObservedCollectionReplaceDelegate OnItemReplaced;

        public delegate void ObservedCollectionResetDelegate();
        public event ObservedCollectionResetDelegate OnCleared;

        private void ObservedCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (OnItemAdded != null) OnItemAdded(e.NewStartingIndex, (T)e.NewItems[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (OnItemRemoved != null) OnItemRemoved(e.OldStartingIndex, (T)e.OldItems[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    if (OnItemReplaced != null) OnItemReplaced(e.OldStartingIndex, (T)e.OldItems[0], (T)e.NewItems[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    if (OnItemMoved != null) OnItemMoved(e.OldStartingIndex, e.NewStartingIndex, (T)e.NewItems[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    if (OnCleared != null) OnCleared();
                    break;
                default:
                    break;
            }
        }
    }
    public class ObsDoubles :  ObservedCollection<double>
    {
        public ObsDoubles() { }
        public ObsDoubles(List<double> list) : base(list) { }

        public ObsDoubles(IEnumerable<double> collection) : base(collection) { }
    }
}
