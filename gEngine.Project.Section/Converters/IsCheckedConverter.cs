using gEngine.Manipulator;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.Project.Section.Converters
{

    public class IsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            IManipulators manipulators = (IManipulators)value;
            foreach (IManipulatorBase m in manipulators)
            {
                if (m.GetType().Name == parameter.ToString()&&m.Type== "LayerManipulator")
                {
                    return true;
                }
            }
            return false;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            IManipulators manipulators = new IManipulators();
            if (!b)
            {
                IManipulatorBase dm = gEngine.Manipulator.Registry.CreateManipulator(parameter.ToString());
                if (dm != null)
                {
                    manipulators.Add(dm);
                }
            }
            return manipulators;
        }
    }
}
