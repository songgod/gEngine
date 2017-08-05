using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Section
{
    public class FaceProxyObject : Object
    {


        public gTopology.Face Face
        {
            get { return (gTopology.Face)GetValue(FaceProperty); }
            set { SetValue(FaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Face.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FaceProperty =
            DependencyProperty.Register("Face", typeof(gTopology.Face), typeof(FaceProxyObject));



        public SectionInfo SectionInfo { get; set; }
        public virtual FillStyle FillStyle { get; set; }
    }

    public class StratumFaceProxyObject : FaceProxyObject
    {
        public override FillStyle FillStyle
        {
            get
            {
                return SectionInfo.GetStratumFillStyle(Face.InsideID);
            }
            set
            {
                SectionInfo.SetFillStyle(Face.InsideID, value);
            }
        }
    }

    public class SandFaceProxyObject : FaceProxyObject
    {
        public override FillStyle FillStyle
        {
            get
            {
                return SectionInfo.GetSandFillStyle(Face.InsideID);
            }
            set
            {
                SectionInfo.SetFillStyle(Face.InsideID, value);
            }
        }
    }
}
