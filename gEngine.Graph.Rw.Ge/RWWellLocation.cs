using gEngine.Graph.Interface;
using gEngine.Graph.Ge.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge
{
    class RWWellLocation : RWObjectBase
    {
        public override string SupportType { get { return "WellLocation"; } }
        public override IObject Read(XmlNode node)
        {
            WellLocation WellLocation = new WellLocation();
            
            return WellLocation;

            
        }
        public override void Write(XmlNode node, IObject obj)
        {

        }
    }
}
