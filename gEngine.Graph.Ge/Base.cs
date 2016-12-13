using System.ComponentModel;

namespace gEngine.Graph.Ge
{
    public abstract class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseProertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
