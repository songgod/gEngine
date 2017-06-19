﻿using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveStratumManipulator : DrawCurveManipulator
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            SectionLayer sectionlayer = this.AssociatedObject.LayerContext as SectionLayer;
            if (sectionlayer != null)
            {
                Graph = sectionlayer.StratumObject.TopGraph;
                LineType = (int)SectionLayerEdit.SectionLineType.Stratum;
            }
        }
    }

    public class DrawCurveStratumManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawCurveStratumManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawCurveStratumManipulator();
        }
    }
}
