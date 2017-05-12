﻿

using System;
using gEngine.Utility;
using System.ComponentModel;

namespace gEngine.Graph.Interface
{
    public interface ILayer
    {
        string Name { get; set; }

        string Type { get; set; }

        IObjects Objects { get; set; }
    }

    public class ILayers : ObservedCollection<ILayer>
    {
        private int currentindex=-1;

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
