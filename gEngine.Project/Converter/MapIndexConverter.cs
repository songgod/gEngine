﻿using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gEngine.Project.Converter
{
    public class MapIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            if(index<0)
                return null;
            IMaps maps = ((MapsControl)parameter).MapsSource;
            if (index < 0 || index >= maps.Count)
                return null;
            return maps[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
