using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge
{
    public class Layer : Base, ILayer
    {
        public Layer()
        {
            Objects = new IObjects();
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaiseProertyChanged("Name");
            }
        }

        private IObjects objects;
        public IObjects Objects
        {
            get
            {
                return objects;
            }

            set
            {
                objects = value;
                RaiseProertyChanged("Objects");
            }
        }
    }
}
