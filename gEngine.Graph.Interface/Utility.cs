using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Interface
{
    static public class Utility
    {
        static public void ClearSelect(IMap map)
        {
            ILayers layers = map.Layers;
            foreach (ILayer layer in layers)
            {
                ClearSelect(layer);
            }
        }

        static public void ClearSelect(ILayer layer)
        {
            IObjects objs = layer.Objects;
            foreach (IObject obj in objs)
            {
                obj.IsSelected = false;
            }
        }

        static public void SelectAll(IMap map)
        {
            ILayers layers = map.Layers;
            foreach (ILayer layer in layers)
            {
                SelectAll(layer);
            }
        }

        static public void SelectAll(ILayer layer)
        {
            IObjects objs = layer.Objects;
            foreach (IObject obj in objs)
            {
                obj.IsSelected = true;
            }
        }
    }
}
