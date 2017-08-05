using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Section
{
    public class LineProxyObject : Object
    {


        public gTopology.Line Line
        {
            get { return (gTopology.Line)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(gTopology.Line), typeof(LineProxyObject), new PropertyMetadata(null));



        public SectionInfo SectionInfo { get; set; }
        public virtual LineStyle LineStyle { get; set; }
    }

    public class FaultLineProxyObject : LineProxyObject
    {
        public override LineStyle LineStyle
        {
            get
            {
                return SectionInfo.GetFaultLineStyle(Line.Id);
            }
            set
            {
                SectionInfo.SetLineStyle(Line.Id, value);
            }
        }
    }

    public class StratumLineProxyObject : LineProxyObject
    {
        public override LineStyle LineStyle
        {
            get
            {
                return SectionInfo.GetStratumLineStyle(Line.Id);
            }
            set
            {
                SectionInfo.SetLineStyle(Line.Id, value);
            }
        }
    }

    public class SandLineProxyObject : LineProxyObject
    {
        public override LineStyle LineStyle
        {
            get
            {
                return SectionInfo.GetSandLineStyle(Line.Id);
            }
            set
            {
                SectionInfo.SetLineStyle(Line.Id, value);
            }
        }
    }
}
