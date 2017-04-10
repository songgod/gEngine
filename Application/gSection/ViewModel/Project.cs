using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.ViewModel
{
    public class Project
    {
        public Project() 
        {

        }

        public IMaps Maps { get; set; }

        public IMaps OpenMaps { get; set; }

        public IDBFactory DBFactory { get; set; }
    }
}
