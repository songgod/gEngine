using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Section
{
    public class SectionLayer : Layer
    {
        public SectionLayer()
        {
            Type = "Section";
            SandObject = new SandObject();
            StratumObject = new StratumObject();
            Objects.Add(StratumObject);
            Objects.Add(SandObject);
        }
        

        public SandObject SandObject { get; private set; }
        public StratumObject StratumObject { get; private set; }
    }
}
