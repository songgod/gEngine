using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gSection.Converters
{
    public class RibPageCategoryVisibleConvert: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //IMaps maps = value as IMaps;
            int count = (int)value;


            return count > -1;
            //if (maps == null)
            //    return false;
            //if(maps.CurrentIndex<0)
            //    return false; 
            //IMap map = maps[maps.CurrentIndex];

            //foreach (ILayer layer in map.Layers)
            //{
            //    foreach (IObject n in layer.Objects)
            //    {
            //        if (n.IsSelected)
            //            return true;
            //    }
            //}
            //return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
