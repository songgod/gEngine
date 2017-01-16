using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace gEngineTest
{
    public class ItemsControlStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            Style st = new Style();

            

            

            return base.SelectStyle(item, container);
        }
    }
}
