using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Column
{
    public class ColumnLayer : Layer
    {
        public ColumnLayer()
        {

        }

        public override string Type
        {
            get
            {
                return "Column";
            }
        }
    }
}
