using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngineTest.MapDxTab
{
    public class Project
    {
        public Project()
        {
            Maps = new IMaps();
        }
        public IMaps Maps { get; set; }
    }
}
