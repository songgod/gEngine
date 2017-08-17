using DevExpress.Xpf.Bars;
using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Project.Ge.Section.Commands.SectionEdit;
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
    public class NewTrendLineCommand : SectionCommandBase
    {
        public NewTrendLineCommand()
        {
            Command = SectionEditCommands.NewTrendLineCommand;
        }

        public override void SetManipulator(LayerControl lc, object param)
        {
            if (ManipulatorSetter.IsContainManipulator("DrawTrendLineManipulator", lc))
                ManipulatorSetter.ClearManipulator(lc);
            else
            {
                IManipulatorBase dm = gEngine.Manipulator.Registry.CreateManipulator("DrawTrendLineManipulator", param);
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
                bd.ConverterParameter = "DrawTrendLineManipulator";
                bd.Mode = BindingMode.TwoWay;
                bar.SetBinding(BarCheckItem.IsCheckedProperty, bd);
            }
        }
    }
}
