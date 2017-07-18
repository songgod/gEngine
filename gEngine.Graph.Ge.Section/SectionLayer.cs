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
            SectionInfo = new SectionInfo();
        }

        public SectionInfo SectionInfo { get; private set; }
    }
}
