using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Basic
{
    public class BasicLayer : Layer
    {
        public BasicLayer()
        {
        }

        public override string Type
        {
            get
            {
                return "Basic";
            }
        }
    }
}
