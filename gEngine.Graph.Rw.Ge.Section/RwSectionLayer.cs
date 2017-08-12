﻿using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using gEngine.Util.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gEngine.Graph.Rw.Ge.Section
{
    class RWSectionLayer : RWLayerBase
    {
        public override string SupportType { get { return "Section"; } }

        public override void ReadLayer(ILayer Ilayer, XmlNode node)
        {
            base.ReadLayer(Ilayer, node);
            RWSectionInfo rwinfo = new RWSectionInfo();
            SectionLayer sectionlayer = Ilayer as SectionLayer;
            rwinfo.Read(sectionlayer.SectionInfo, node);
            SectionLayerEdit sectionLayerEdit = new SectionLayerEdit();
            sectionLayerEdit.SectionLayer = sectionlayer;
            sectionLayerEdit.RebuildGraph();
            
        }

        public override void WriteLayer(XmlNode node, ILayer layer)
        {
            base.WriteLayer(node, layer);
            RWSectionInfo rwinfo = new RWSectionInfo();
            SectionLayer sectionlayer = layer as SectionLayer;
            rwinfo.Write(node, sectionlayer.SectionInfo);
        }

        public override ILayer CreateLayer()
        {
            return new SectionLayer();
        }
    }
}
