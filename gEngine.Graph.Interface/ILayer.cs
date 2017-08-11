

using System;
using gEngine.Utility;
using System.ComponentModel;

namespace gEngine.Graph.Interface
{
    public interface ILayer
    {
        string Name { get; set; }

        string Type { get; set; }
        /// <summary>
        /// 添加新图层
        /// </summary>
        bool NewLayer { get; set; }

        /// <summary>
        /// 添加通用图层
        /// </summary>
        bool NewUniversallyLayer { get; set; }
        /// <summary>
        /// 添加井位图层
        /// </summary>
        bool NewWellLocationLayer { get; set; }

        /// <summary>
        /// 添加剖面图层
        /// </summary>
        bool NewSectionMapLayer { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// 编辑
        /// </summary>
        bool Editable { get; set; }

        /// <summary>
        /// 删除图层
        /// </summary>
        bool Delete { get; set; }

        /// <summary>
        /// 清空图层
        /// </summary>
        bool EmptyLayer { get; set; }

     

        double Opacity { get; set; }

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
