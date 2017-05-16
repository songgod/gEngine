using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge
{
    class RWWell : RWObjectBase
    {
        public override string SupportType { get { return "Well"; }}
        public override IObject Read(XmlNode node)
        {
            Well well = new Well();



            return well;
        }
        public override void Write(XmlNode node, IObject obj)
        {

        }
    }
}
