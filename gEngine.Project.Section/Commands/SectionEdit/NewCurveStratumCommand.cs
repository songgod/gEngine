using DevExpress.Xpf.Bars;
using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Section;
using gEngine.Project.Section.Converters;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace gEngine.Project.Ge.Section.Commands.SectionEdit
{
    class NewCurveStratumCommand : SectionCommandBase
    {
        public NewCurveStratumCommand()
        {
            Command = SectionEditCommands.NewCurveStratumCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            if (ManipulatorSetter.IsContainManipulator("DrawCurveStratumManipulator", lc))
                ManipulatorSetter.ClearManipulator(lc);
            else
            {
                DrawCurveStratumManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawCurveStratumManipulator", param) as DrawCurveStratumManipulator;
                if (dm == null)
                    return;
                ManipulatorSetter.SetManipulator(dm, lc);
            }

            BarCheckItem bar = param as BarCheckItem;
            if (bar != null && lc != null)
            {
                Binding bd = new Binding();
                bd.Source = lc;
                bd.Path = new PropertyPath("(0)", ManipulatorSetter.ManipulatorsProperty);
                bd.Converter = new IsCheckedConverter();
                bd.ConverterParameter = "DrawCurveStratumManipulator";
                bd.Mode = BindingMode.TwoWay;
                bar.SetBinding(BarCheckItem.IsCheckedProperty, bd);
            }
        }
    }
}
