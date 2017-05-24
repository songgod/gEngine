﻿using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge
{
    public abstract class RWObjectBase
    {
        public abstract string SupportType { get; }

        public abstract void Read(IObject Object, XmlNode node);

        public abstract void Write(XmlNode node, IObject obj);

        public abstract IObject CreateObject();
    }
}
