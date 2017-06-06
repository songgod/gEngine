using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Section
{
    public class SectionLayer : Layer
    {
        public SectionLayer()
        {
            Type = "Section";
            SectionObject = new SectionObject();
        }
        

        public SectionObject SectionObject { get; private set; }
    }
}
